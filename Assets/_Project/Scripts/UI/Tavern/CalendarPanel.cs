using System.Collections.Generic;
using Hearthdelve.Core.Events;
using Hearthdelve.Shared.Calendar;
using Hearthdelve.Shared.Surface;
using Hearthdelve.Tavern.Scene;
using Hearthdelve.UI.Localization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hearthdelve.UI.Tavern
{
    /// <summary>
    /// The calendar board in Tally Ho! (5b): today's date, and what's coming in the next fortnight (the next festival, birthdays),
    /// soonest first. Anticipation lives here; nothing on it is a deadline. Holds the surface clock while it's read; "back" (or
    /// Escape / B) closes it.
    /// </summary>
    public sealed class CalendarPanel : MonoBehaviour
    {
        /// <summary>How far ahead the board looks (two weeks: a month's half).</summary>
        public const int AheadDays = 14;

        [SerializeField] GameObject m_Root;
        [SerializeField] LocalizedSuperText m_Today;
        [SerializeField] LocalizedSuperText[] m_Lines;
        [SerializeField] Button m_Back;

        public bool IsOpen => m_Root != null && m_Root.activeSelf;
        /// <summary>The lines on show (tests).</summary>
        public IReadOnlyList<LocalizedSuperText> Lines => m_Lines;
        public LocalizedSuperText Today => m_Today;

        public void Configure(GameObject root, LocalizedSuperText today, LocalizedSuperText[] lines, Button back)
        {
            m_Root = root;
            m_Today = today;
            m_Lines = lines;
            m_Back = back;
        }

        void Awake()
        {
            if (m_Back != null) m_Back.onClick.AddListener(Close);
            if (m_Root != null) m_Root.SetActive(false);
        }

        void OnEnable() => EventBus<DaytimePlaceUsed>.Subscribe(OnPlaceUsed);

        void OnDisable()
        {
            EventBus<DaytimePlaceUsed>.Unsubscribe(OnPlaceUsed);
            SurfacePause.Release(this);
        }

        void OnPlaceUsed(DaytimePlaceUsed e)
        {
            if (e.Kind == TavernInteractableKind.CalendarBoard) Open();
        }

        public void Open()
        {
            TavernDirector director = TavernDirector.Instance;
            if (IsOpen || m_Root == null || director == null || director.Phase != TavernPhase.Daytime) return;
            Fill();
            m_Root.SetActive(true);
            SurfacePause.Hold(this);
            Hearthdelve.Core.Input.InputMaps.ActivateUIOnly();
            if (EventSystem.current != null && m_Back != null) EventSystem.current.SetSelectedGameObject(m_Back.gameObject);
        }

        public void Close()
        {
            if (!IsOpen) return;
            m_Root.SetActive(false);
            SurfacePause.Release(this);
            TavernDirector.RestoreInput();
        }

        void Fill()
        {
            if (m_Today != null) m_Today.Set(CalendarLocKeys.LongDate, LongDateArgs(GameCalendar.Today));
            List<CalendarOccasion> coming = GameCalendar.Upcoming(AheadDays);
            CalendarSettings settings = GameCalendar.Settings;
            for (int i = 0; i < (m_Lines?.Length ?? 0); i++)
            {
                if (m_Lines[i] == null) continue;
                if (i < coming.Count) Describe(m_Lines[i], coming[i], settings);
                else m_Lines[i].Set(i == 0 ? CalendarLocKeys.Nothing : null);
            }
        }

        static object[] LongDateArgs(CalendarDate d) =>
            new object[] { Loc.UI(CalendarRules.WeekdayKey(d.Weekday)), d.Day, Loc.UI(CalendarRules.MonthKey(d.Month)) };

        static void Describe(LocalizedSuperText line, CalendarOccasion o, in CalendarSettings settings)
        {
            if (!o.IsBirthday)
            {
                string name = Loc.UI(CalendarRules.FestivalKey(o.Festival));
                if (o.InDays == 0) line.Set(CalendarLocKeys.FestivalToday, name);
                else if (o.InDays == 1) line.Set(CalendarLocKeys.FestivalTomorrow, name);
                else line.Set(CalendarLocKeys.FestivalIn, name, o.InDays);
                return;
            }
            string who = Loc.Get(Loc.ContentTable, NameKey(o.Character, settings));
            bool found = o.Character == "ogrin";
            if (o.InDays == 0) line.Set(found ? CalendarLocKeys.FoundDayToday : CalendarLocKeys.BirthdayToday, who);
            else if (o.InDays == 1) line.Set(found ? CalendarLocKeys.FoundDayTomorrow : CalendarLocKeys.BirthdayTomorrow, who);
            else line.Set(found ? CalendarLocKeys.FoundDayOn : CalendarLocKeys.BirthdayOn, who, CalendarLocKeys.Short(o.Date));
        }

        /// <summary>The person's name key in the Content table (staff are "staff.&lt;id&gt;", villagers "villager.&lt;id&gt;").</summary>
        static string NameKey(string character, in CalendarSettings s) => character == "pip" || character == "gunta" ? $"staff.{character}" : $"villager.{character}";

        void Update()
        {
            if (!IsOpen) return;
            bool back = UnityEngine.InputSystem.Keyboard.current != null && UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame
                        || UnityEngine.InputSystem.Gamepad.current != null && UnityEngine.InputSystem.Gamepad.current.buttonEast.wasPressedThisFrame;
            if (back) Close();
        }
    }
}
