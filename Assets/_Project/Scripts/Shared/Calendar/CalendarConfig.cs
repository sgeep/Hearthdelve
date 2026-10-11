using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthdelve.Shared.Calendar
{
    /// <summary>A day of the year that comes round every year: a festival, by its stable id (its name is in the string table).</summary>
    [Serializable]
    public struct CalendarFestival
    {
        [Tooltip("Stable id (e.g. remembrance); never a display name.")] public string id;
        [Min(1)] public int month;
        [Min(1)] public int day;
    }

    /// <summary>A villager's birthday (Ogrin's is his "found day"): the date, the conversation that opens their talk that day, and the dish they ask for at dinner.</summary>
    [Serializable]
    public struct CalendarBirthday
    {
        [Tooltip("The character's stable id (e.g. ogrin, pip for Orik).")] public string character;
        [Min(1)] public int month;
        [Min(1)] public int day;
        [Tooltip("Played first when the keeper talks to them that day, once a year (empty: none).")] public string conversation;
        [Tooltip("The recipe id they ask for if they come to dinner that evening (empty: none).")] public string favouriteDish;
    }

    /// <summary>
    /// The calendar's shape (5b, the owner's choices: four months of 14 days, a week of seven). Pure data: <see cref="CalendarRules"/>
    /// derives every date from the day count. Month and weekday names live only in the string table (keys
    /// <see cref="CalendarRules.MonthKey"/>, <see cref="CalendarRules.WeekdayKey"/>), never here or in ids.
    /// </summary>
    [Serializable]
    public struct CalendarSettings
    {
        [Min(1)] public int months;
        [Min(1)] public int daysPerMonth;
        [Min(1)] public int weekLength;
        [Tooltip("The date of day 1 (a new game's arrival day). LOCKED once shipped: changing it moves every save's dates.")]
        [Min(1)] public int startMonth;
        [Min(1)] public int startDay;
        [Tooltip("The weekday of day 1 (1 = the first weekday).")] [Min(1)] public int startWeekday;
        public CalendarFestival[] festivals;
        public CalendarBirthday[] birthdays;

        public int DaysPerYear => Mathf.Max(1, months) * Mathf.Max(1, daysPerMonth);

        /// <summary>5b's calendar: the year starts on arrival day, Remembrance on day 10, three birthdays outside its month.</summary>
        public static CalendarSettings Default => new()
        {
            months = 4,
            daysPerMonth = 14,
            weekLength = 7,
            startMonth = 1,
            startDay = 1,
            startWeekday = 1,
            festivals = new[] { new CalendarFestival { id = CalendarRules.Remembrance, month = 1, day = 10 } },
            birthdays = new[]
            {
                new CalendarBirthday { character = "ogrin", month = 2, day = 6, conversation = "Birthday/Ogrin", favouriteDish = "eggs_on_toast" },
                new CalendarBirthday { character = "pip", month = 3, day = 9, conversation = "Birthday/Orik", favouriteDish = "brackenford_ale" },
                new CalendarBirthday { character = "bart", month = 4, day = 4, conversation = "Birthday/Bart", favouriteDish = "grilled_spider_leg" },
            },
        };
    }

    [CreateAssetMenu(menuName = "Hearthdelve/Calendar", fileName = "Calendar")]
    public sealed class CalendarConfig : ScriptableObject
    {
        public CalendarSettings settings = CalendarSettings.Default;
    }

    /// <summary>A date: the month and day (1-based), the weekday (1-based) and the year (the keeper's first year is 1).</summary>
    public readonly struct CalendarDate : IEquatable<CalendarDate>
    {
        public readonly int Year, Month, Day, Weekday;

        public CalendarDate(int year, int month, int day, int weekday)
        {
            Year = year;
            Month = month;
            Day = day;
            Weekday = weekday;
        }

        public bool Equals(CalendarDate other) => Year == other.Year && Month == other.Month && Day == other.Day && Weekday == other.Weekday;
        public override bool Equals(object obj) => obj is CalendarDate d && Equals(d);
        public override int GetHashCode() => (Year * 397 + Month) * 397 + Day;
        public override string ToString() => $"year {Year}, month {Month}, day {Day} (weekday {Weekday})";
    }

    /// <summary>A festival or birthday coming up: what, when, and in how many days (0 = today).</summary>
    public readonly struct CalendarOccasion
    {
        public readonly string Festival, Character;
        public readonly CalendarDate Date;
        public readonly int InDays;

        public CalendarOccasion(string festival, string character, CalendarDate date, int inDays)
        {
            Festival = festival;
            Character = character;
            Date = date;
            InDays = inDays;
        }

        public bool IsBirthday => !string.IsNullOrEmpty(Character);
    }
}
