using System.Collections.Generic;

namespace Hearthdelve.Shared.Calendar
{
    /// <summary>
    /// The fantasy calendar (5b), pure: every date is derived from the game's day count (day 1 is a new game's arrival day), so
    /// nothing about the calendar is saved and the save version doesn't change. The start date and month lengths are locked once
    /// shipped (they decide every save's dates).
    /// </summary>
    public static class CalendarRules
    {
        public const string Remembrance = "remembrance";

        /// <summary>The string table's key for a month's name (1-based); the names are only ever in the table.</summary>
        public static string MonthKey(int month) => $"calendar.month.{month}";
        public static string WeekdayKey(int weekday) => $"calendar.weekday.{weekday}";
        public static string FestivalKey(string festival) => $"calendar.festival.{festival}";

        /// <summary>Days since the start of the calendar's first year, for day 1 at the start date.</summary>
        static int Offset(in CalendarSettings s) => (Clamp(s.startMonth, 1, Months(s)) - 1) * DaysPerMonth(s) + Clamp(s.startDay, 1, DaysPerMonth(s)) - 1;

        static int Months(in CalendarSettings s) => s.months < 1 ? 1 : s.months;
        static int DaysPerMonth(in CalendarSettings s) => s.daysPerMonth < 1 ? 1 : s.daysPerMonth;
        static int Week(in CalendarSettings s) => s.weekLength < 1 ? 1 : s.weekLength;
        static int Clamp(int v, int lo, int hi) => v < lo ? lo : v > hi ? hi : v;

        /// <summary>The date of a game day (days before 1 count as day 1).</summary>
        public static CalendarDate DateOf(int day, in CalendarSettings s)
        {
            int n = (day < 1 ? 1 : day) - 1 + Offset(s);
            int year = n / s.DaysPerYear + 1, inYear = n % s.DaysPerYear;
            int week = Week(s);
            int weekday = ((day < 1 ? 1 : day) - 1 + Clamp(s.startWeekday, 1, week) - 1) % week + 1;
            return new CalendarDate(year, inYear / DaysPerMonth(s) + 1, inYear % DaysPerMonth(s) + 1, weekday);
        }

        /// <summary>The game day of a date in the keeper's <paramref name="year"/> (may be before day 1 in the first year).</summary>
        public static int DayOf(int year, int month, int dayOfMonth, in CalendarSettings s) =>
            (year - 1) * s.DaysPerYear + (Clamp(month, 1, Months(s)) - 1) * DaysPerMonth(s) + Clamp(dayOfMonth, 1, DaysPerMonth(s)) - 1 - Offset(s) + 1;

        /// <summary>The next game day on or after <paramref name="day"/> that falls on this month and day.</summary>
        public static int NextDayOf(int day, int month, int dayOfMonth, in CalendarSettings s)
        {
            int from = day < 1 ? 1 : day;
            int year = DateOf(from, s).Year;
            int d = DayOf(year, month, dayOfMonth, s);
            return d >= from ? d : DayOf(year + 1, month, dayOfMonth, s);
        }

        public static bool IsFestival(int day, string festival, in CalendarSettings s)
        {
            if (s.festivals == null || string.IsNullOrEmpty(festival)) return false;
            CalendarDate date = DateOf(day, s);
            foreach (CalendarFestival f in s.festivals)
                if (f.id == festival && f.month == date.Month && f.day == date.Day) return true;
            return false;
        }

        /// <summary>Whose birthday it is on this day (usually nobody).</summary>
        public static List<CalendarBirthday> Birthdays(int day, in CalendarSettings s)
        {
            var list = new List<CalendarBirthday>();
            if (s.birthdays == null) return list;
            CalendarDate date = DateOf(day, s);
            foreach (CalendarBirthday b in s.birthdays)
                if (b.month == date.Month && b.day == date.Day && !string.IsNullOrEmpty(b.character)) list.Add(b);
            return list;
        }

        public static bool IsBirthday(int day, string character, in CalendarSettings s)
        {
            foreach (CalendarBirthday b in Birthdays(day, s))
                if (b.character == character) return true;
            return false;
        }

        /// <summary>Days from <paramref name="day"/> to the festival's next occurrence (0 on the day), or -1 if there's no such festival.</summary>
        public static int DaysUntil(int day, string festival, in CalendarSettings s)
        {
            if (s.festivals == null) return -1;
            int from = day < 1 ? 1 : day;
            foreach (CalendarFestival f in s.festivals)
                if (f.id == festival) return NextDayOf(from, f.month, f.day, s) - from;
            return -1;
        }

        /// <summary>
        /// A schedule's or a conversation's calendar question by id: <c>festival:&lt;id&gt;</c> (that festival is today) or
        /// <c>birthday:&lt;character&gt;</c> (their birthday is today).
        /// </summary>
        public static bool Is(string occasion, int day, in CalendarSettings s)
        {
            if (string.IsNullOrEmpty(occasion)) return false;
            if (occasion.StartsWith("festival:")) return IsFestival(day, occasion.Substring("festival:".Length), s);
            if (occasion.StartsWith("birthday:")) return IsBirthday(day, occasion.Substring("birthday:".Length), s);
            return false;
        }

        /// <summary>The festivals and birthdays from <paramref name="day"/> through <paramref name="withinDays"/> days on, soonest first.</summary>
        public static List<CalendarOccasion> Upcoming(int day, int withinDays, in CalendarSettings s)
        {
            var list = new List<CalendarOccasion>();
            int from = day < 1 ? 1 : day;
            if (s.festivals != null)
                foreach (CalendarFestival f in s.festivals)
                {
                    int d = NextDayOf(from, f.month, f.day, s);
                    if (d - from <= withinDays) list.Add(new CalendarOccasion(f.id, null, DateOf(d, s), d - from));
                }
            if (s.birthdays != null)
                foreach (CalendarBirthday b in s.birthdays)
                {
                    int d = NextDayOf(from, b.month, b.day, s);
                    if (d - from <= withinDays) list.Add(new CalendarOccasion(null, b.character, DateOf(d, s), d - from));
                }
            list.Sort((a, b) => a.InDays.CompareTo(b.InDays));
            return list;
        }

        /// <summary>The one-time beat for something done once a year (StoryState.SeenHints; no save change).</summary>
        public static string YearlyBeat(string what, int day, in CalendarSettings s) => $"beat:{what}:{DateOf(day, s).Year}";
    }
}
