using System.Collections.Generic;
using System.Linq;
using Hearthdelve.Shared.Calendar;
using Hearthdelve.Shared.Village;
using Hearthdelve.UI.Localization;
using NUnit.Framework;

namespace Hearthdelve.Tests
{
    /// <summary>
    /// The fantasy calendar (5b), pure: four months of 14 days and a week of seven, every date derived from the day count (no
    /// save change). The first year's dates are the ones shown to the owner before the calendar ships (they lock then).
    /// </summary>
    public class CalendarTests
    {
        static readonly CalendarSettings k_Default = CalendarSettings.Default;

        [Test]
        public void DayOne_IsTheFirstOfTheFirstMonth_AndDatesRollOver()
        {
            Assert.That(CalendarRules.DateOf(1, k_Default), Is.EqualTo(new CalendarDate(1, 1, 1, 1)));
            Assert.That(CalendarRules.DateOf(14, k_Default), Is.EqualTo(new CalendarDate(1, 1, 14, 7)));
            Assert.That(CalendarRules.DateOf(15, k_Default), Is.EqualTo(new CalendarDate(1, 2, 1, 1)), "a new month; the week's two to a month");
            Assert.That(CalendarRules.DateOf(56, k_Default), Is.EqualTo(new CalendarDate(1, 4, 14, 7)), "the year's last day");
            Assert.That(CalendarRules.DateOf(57, k_Default), Is.EqualTo(new CalendarDate(2, 1, 1, 1)), "a 56-day year");
            Assert.That(CalendarRules.DateOf(0, k_Default), Is.EqualTo(CalendarRules.DateOf(1, k_Default)), "nothing before day 1");
        }

        [Test]
        public void Remembrance_IsOnDay10_ThenEveryYear_AndNeverOnTheOpeningDays()
        {
            int[] days = Enumerable.Range(1, 200).Where(d => CalendarRules.IsFestival(d, CalendarRules.Remembrance, k_Default)).ToArray();
            Assert.That(days, Is.EqualTo(new[] { 10, 66, 122, 178 }));
            Assert.That(CalendarRules.DaysUntil(1, CalendarRules.Remembrance, k_Default), Is.EqualTo(9));
            Assert.That(CalendarRules.DaysUntil(10, CalendarRules.Remembrance, k_Default), Is.Zero);
            Assert.That(CalendarRules.DaysUntil(11, CalendarRules.Remembrance, k_Default), Is.EqualTo(55));
            Assert.That(CalendarRules.DaysUntil(1, "nothing", k_Default), Is.EqualTo(-1));
        }

        /// <summary>The first year as shown to the owner: Remembrance day 10; Ogrin's found day 20, Orik 37, Bart 46.</summary>
        [Test]
        public void TheFirstYearsBirthdays_FallOutsideRemembrancesMonth_OnePerMonth()
        {
            var first = new Dictionary<string, int>();
            for (int d = 1; d <= 56; d++)
                foreach (CalendarBirthday b in CalendarRules.Birthdays(d, k_Default))
                    first[b.character] = d;
            Assert.That(first, Is.EqualTo(new Dictionary<string, int> { ["ogrin"] = 20, ["pip"] = 37, ["bart"] = 46 }));
            foreach (int d in first.Values)
            {
                Assert.That(CalendarRules.DateOf(d, k_Default).Month, Is.Not.EqualTo(1), "none in Remembrance's month");
                Assert.That(CalendarRules.IsFestival(d, CalendarRules.Remembrance, k_Default), Is.False);
            }
            Assert.That(first.Values.Select(d => CalendarRules.DateOf(d, k_Default).Month).Distinct().Count(), Is.EqualTo(3), "one a month");
            Assert.That(CalendarRules.Is("birthday:ogrin", 76, k_Default), "and every year (20 + 56)");
            Assert.That(CalendarRules.Is("festival:remembrance", 10, k_Default));
            Assert.That(CalendarRules.Is("birthday:bart", 10, k_Default), Is.False);
        }

        [Test]
        public void Upcoming_ListsTheFortnight_SoonestFirst()
        {
            List<CalendarOccasion> fromDay2 = CalendarRules.Upcoming(2, 14, k_Default);
            Assert.That(fromDay2.Select(o => o.Festival ?? o.Character), Is.EqualTo(new[] { CalendarRules.Remembrance }));
            Assert.That(fromDay2[0].InDays, Is.EqualTo(8));
            List<CalendarOccasion> fromDay10 = CalendarRules.Upcoming(10, 14, k_Default);
            Assert.That(fromDay10.Select(o => (o.Festival ?? o.Character, o.InDays)), Is.EqualTo(new[] { (CalendarRules.Remembrance, 0), ("ogrin", 10) }));
        }

        [Test]
        public void TheYearlyBeat_ChangesWithTheYear()
        {
            Assert.That(CalendarRules.YearlyBeat("birthday:bart", 46, k_Default), Is.EqualTo("beat:birthday:bart:1"));
            Assert.That(CalendarRules.YearlyBeat("birthday:bart", 102, k_Default), Is.EqualTo("beat:birthday:bart:2"));
        }

        [Test]
        public void OnOgrinsFoundDay_HesWell_AndTheCalendarConditionHolds()
        {
            var settings = VillageLifeSettings.Default;
            settings.ogrinWellChance = 0f;
            settings.ogrinWellAfterHerbs = 0f;
            var foundDay = new ScheduleWorld(20, 7, true, settings, calendar: k_Default);
            var ordinary = new ScheduleWorld(21, 7, true, settings, calendar: k_Default);
            Assert.That(VillageDays.Holds(DayRule.OgrinWell, foundDay), "always well on his found day");
            Assert.That(VillageDays.Holds(DayRule.OgrinWell, ordinary), Is.False, "never well otherwise, at a zero chance");
            var block = new ScheduleBlock(12 * 60, 13 * 60, "ogrin.yard", "found_day", ScheduleCondition.OnCalendar("birthday:ogrin"));
            var blocks = new List<ScheduleBlock> { block, new(8 * 60, 17 * 60, "ogrin.window", "bed") };
            Assert.That(ScheduleRules.Resolve(blocks, foundDay, 12 * 60 + 30), Is.SameAs(block));
            Assert.That(ScheduleRules.Resolve(blocks, ordinary, 12 * 60 + 30).activity, Is.EqualTo("bed"));
        }

        [Test]
        public void ABirthdayGuest_AlwaysComesToDinner_BesideTheUsualDraw()
        {
            var candidates = new List<CommunityRules.Patron> { new("maximo", 0f), new("bart", 0f), new("grim", 0f) };
            Assert.That(CommunityRules.Tonight(5, 46, candidates), Is.Empty, "nobody, at a zero chance");
            Assert.That(CommunityRules.Tonight(5, 46, candidates, always: new[] { "bart" }), Is.EqualTo(new[] { "bart" }));
            var keen = new List<CommunityRules.Patron> { new("maximo", 1f), new("bart", 0f), new("grim", 1f) };
            Assert.That(CommunityRules.Tonight(5, 46, keen, always: new[] { "bart" }), Is.EqualTo(new[] { "bart", "maximo", "grim" }), "and the usual two beside");
        }

        [Test]
        public void TheCalendarsNames_AreOnlyInTheStringTable()
        {
            Assert.That(CalendarLocKeys.Names.Select(n => n.key),
                Is.EquivalentTo(Enumerable.Range(1, 4).Select(CalendarRules.MonthKey).Concat(Enumerable.Range(1, 7).Select(CalendarRules.WeekdayKey))
                    .Append(CalendarRules.FestivalKey(CalendarRules.Remembrance))));
            IReadOnlyDictionary<string, string> ui = ProjectScan.English(Hearthdelve.UI.Localization.Loc.UITable);
            foreach (var (key, _) in CalendarLocKeys.Names) Assert.That(ui.ContainsKey(key), $"{key} is in the UI table");
        }
    }
}
