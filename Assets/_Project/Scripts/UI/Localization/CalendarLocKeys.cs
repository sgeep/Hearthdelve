using Hearthdelve.Shared.Calendar;

namespace Hearthdelve.UI.Localization
{
    /// <summary>
    /// UI table keys for the calendar (5b). The month, weekday and festival names (<see cref="Names"/>) are placeholders the owner
    /// renames in the string table: the builder adds them only when they're missing, never over a change, and no code or id ever
    /// holds a name.
    /// </summary>
    public static class CalendarLocKeys
    {
        /// <summary>"9 Thawing": the day of the month, the month's name (the HUD, under the clock).</summary>
        public const string Date = "surface.date";
        /// <summary>"Hearthday, 9 Thawing": the weekday, the day, the month (the board).</summary>
        public const string LongDate = "calendar.long_date";
        public const string Board = "calendar.board";
        public const string Title = "calendar.title";
        public const string Coming = "calendar.coming";
        /// <summary>A festival: "{0}" its name; today, tomorrow, or in {1} days.</summary>
        public const string FestivalToday = "calendar.festival_today";
        public const string FestivalTomorrow = "calendar.festival_tomorrow";
        public const string FestivalIn = "calendar.festival_in";
        /// <summary>A birthday: "{0}" the person; today, tomorrow, or on "{1}" (a date).</summary>
        public const string BirthdayToday = "calendar.birthday_today";
        public const string BirthdayTomorrow = "calendar.birthday_tomorrow";
        public const string BirthdayOn = "calendar.birthday_on";
        /// <summary>Ogrin's birthday is his found day.</summary>
        public const string FoundDayToday = "calendar.found_day_today";
        public const string FoundDayTomorrow = "calendar.found_day_tomorrow";
        public const string FoundDayOn = "calendar.found_day_on";
        public const string Nothing = "calendar.nothing";
        public const string Back = "calendar.back";

        /// <summary>The generated strings (formats and labels), refreshed by the builder.</summary>
        public static readonly (string key, string english)[] English =
        {
            (Date, "{0} {1}"),
            (LongDate, "{0}, {1} {2}"),
            (Board, "look at the calendar"),
            (Title, "the calendar"),
            (Coming, "coming up"),
            (FestivalToday, "{0} is today"),
            (FestivalTomorrow, "{0} is tomorrow"),
            (FestivalIn, "{0} in {1} days"),
            (BirthdayToday, "{0}'s birthday is today"),
            (BirthdayTomorrow, "{0}'s birthday is tomorrow"),
            (BirthdayOn, "{0}'s birthday, {1}"),
            (FoundDayToday, "{0}'s found day is today"),
            (FoundDayTomorrow, "{0}'s found day is tomorrow"),
            (FoundDayOn, "{0}'s found day, {1}"),
            (Nothing, "nothing this fortnight"),
            (Back, "back"),
        };

        /// <summary>
        /// The calendar's names: placeholders, added once and then the owner's (5b, C3). Months and weekdays are proper nouns in
        /// English, so capitalised.
        /// </summary>
        public static readonly (string key, string english)[] Names =
        {
            (CalendarRules.MonthKey(1), "Thawing"), (CalendarRules.MonthKey(2), "Highsun"), (CalendarRules.MonthKey(3), "Emberfall"),
            (CalendarRules.MonthKey(4), "Deepfrost"),
            (CalendarRules.WeekdayKey(1), "Firstday"), (CalendarRules.WeekdayKey(2), "Marketday"), (CalendarRules.WeekdayKey(3), "Midweek"),
            (CalendarRules.WeekdayKey(4), "Hearthday"), (CalendarRules.WeekdayKey(5), "Fifthday"), (CalendarRules.WeekdayKey(6), "Lampday"),
            (CalendarRules.WeekdayKey(7), "Restday"),
            (CalendarRules.FestivalKey(CalendarRules.Remembrance), "Karias Remembrance Day"),
        };

        /// <summary>A date as the HUD writes it ("9 Thawing").</summary>
        public static string Short(CalendarDate d) => Loc.UI(Date, d.Day, Loc.UI(CalendarRules.MonthKey(d.Month)));

        /// <summary>A date with its weekday ("Hearthday, 9 Thawing").</summary>
        public static string Long(CalendarDate d) => Loc.UI(LongDate, Loc.UI(CalendarRules.WeekdayKey(d.Weekday)), d.Day, Loc.UI(CalendarRules.MonthKey(d.Month)));
    }
}
