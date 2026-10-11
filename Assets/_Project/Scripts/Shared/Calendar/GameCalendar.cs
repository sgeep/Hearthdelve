using System.Collections.Generic;
using Hearthdelve.Shared.Game;

namespace Hearthdelve.Shared.Calendar
{
    /// <summary>
    /// Today on the calendar (5b): the game's day count read through <see cref="CalendarRules"/> and the database's calendar. Holds
    /// no state; the HUD, the board, the village, the tavern and the story's <c>HH_</c> functions all ask here.
    /// </summary>
    public static class GameCalendar
    {
        /// <summary>Tests: a calendar to use instead of the database's.</summary>
        public static CalendarSettings? SettingsOverride { get; set; }

        public static CalendarSettings Settings
        {
            get
            {
                if (SettingsOverride.HasValue) return SettingsOverride.Value;
                GameDatabase db = GameFlow.Instance != null ? GameFlow.Instance.Database : null;
                return db != null && db.calendar != null ? db.calendar.settings : CalendarSettings.Default;
            }
        }

        static bool InGame => GameFlow.Instance != null && GameFlow.Instance.InGame;

        /// <summary>The game day (1 outside a game).</summary>
        public static int Day => InGame ? GameFlow.Instance.State.Day : 1;

        public static CalendarDate Today => CalendarRules.DateOf(Day, Settings);
        public static bool IsFestival(string festival) => InGame && CalendarRules.IsFestival(Day, festival, Settings);
        public static bool IsBirthday(string character) => InGame && CalendarRules.IsBirthday(Day, character, Settings);
        public static List<CalendarBirthday> Birthdays => InGame ? CalendarRules.Birthdays(Day, Settings) : new List<CalendarBirthday>();
        public static int DaysUntil(string festival) => CalendarRules.DaysUntil(Day, festival, Settings);
        public static List<CalendarOccasion> Upcoming(int withinDays) => CalendarRules.Upcoming(Day, withinDays, Settings);
        public static string YearlyBeat(string what) => CalendarRules.YearlyBeat(what, Day, Settings);
    }
}
