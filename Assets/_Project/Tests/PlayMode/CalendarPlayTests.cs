using System.Collections;
using System.Linq;
using Hearthdelve.Shared.Calendar;
using Hearthdelve.Shared.Characters;
using Hearthdelve.Shared.Story;
using Hearthdelve.Shared.Surface;
using Hearthdelve.Shared.Village;
using Hearthdelve.Tavern.Scene;
using Hearthdelve.UI.Localization;
using Hearthdelve.UI.Tavern;
using Hearthdelve.Village;
using NUnit.Framework;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Hearthdelve.Tests.PlayMode
{
    /// <summary>
    /// 5b Checkpoint A through the real game: the date under the clock, the calendar board, a birthday guest (Bart: his talk opens
    /// with it once, he comes to dinner and asks for his favourite) and Ogrin's found day (Grim and Kaloren drop by at midday).
    /// </summary>
    public class CalendarPlayTests : BootFixture
    {
        static string Shown(LocalizedSuperText t) => t.GetComponent<SuperTextMesh>().text;

        static IEnumerator At(int minute)
        {
            Flow.State.Surface.Restore(minute);
            yield return Frames(2);
            VillagePresence.Instance?.Refresh();
            yield return null;
        }

        [UnityTest]
        public IEnumerator TheDate_IsUnderTheClock_AndTheBoardSaysWhatsComing()
        {
            yield return StartDaytime();   // the clock running: the board must hold it
            SurfaceClockView clock = Object.FindAnyObjectByType<SurfaceClockView>();
            yield return WaitUntil(() => clock.ShownDay == 2, 5f, "the date on the HUD");
            CalendarDate today = GameCalendar.Today;
            Assert.That((today.Month, today.Day), Is.EqualTo((1, 2)), "day 2 is the second of the first month");
            Assert.That(Shown(clock.Date), Is.EqualTo(CalendarLocKeys.Short(today)));

            TavernInteractable board = Object.FindObjectsByType<TavernInteractable>(FindObjectsSortMode.None).Single(i => i.Kind == TavernInteractableKind.CalendarBoard);
            CalendarPanel panel = Object.FindAnyObjectByType<CalendarPanel>();
            board.Use();
            yield return Frames(2);
            Assert.That(panel.IsOpen, "the board opens the calendar");
            Assert.That(Shown(panel.Today), Is.EqualTo(CalendarLocKeys.Long(today)));
            Assert.That(Shown(panel.Lines[0]), Is.EqualTo(Loc.UI(CalendarLocKeys.FestivalIn, Loc.UI(CalendarRules.FestivalKey(CalendarRules.Remembrance)), 8)),
                "Remembrance in eight days");
            Assert.That(SurfacePause.IsHeld, "the clock stands still while it's read");
            panel.Close();
            yield return Frames(2);
            Assert.That(panel.IsOpen, Is.False);
        }

        [UnityTest]
        public IEnumerator OnBartsBirthday_HisTalkOpensWithItOnce_AndHeComesToDinnerForHisFavourite()
        {
            yield return StartDaytime(gimpSeen: true, hold: true, day: 46);
            Assert.That(GameCalendar.IsBirthday(CharacterIds.Bart));
            yield return At(9 * 60);
            Villager bart = null;
            yield return WaitUntil(() => (bart = Villager.All.FirstOrDefault(v => v.CharacterId == CharacterIds.Bart && v.Shown)) != null, 20f, "Bart about");

            Assert.That(StoryServices.Conversations.Talk(CharacterIds.Bart), "talking to him");
            Assert.That(DialogueManager.lastConversationStarted, Is.EqualTo("Birthday/Bart"), "it's his birthday first");
            DialogueManager.StopConversation();
            yield return Frames(2);
            Assert.That(StoryServices.Conversations.Talk(CharacterIds.Bart));
            Assert.That(DialogueManager.lastConversationStarted, Is.EqualTo("Bart/Hub"), "once a year, then his usual talk");
            DialogueManager.StopConversation();
            yield return Frames(2);

            SurfacePause.Release(ClockHold);
            Flow.StartEvening();
            yield return WaitUntil(() => IsIn(TavernPhase.Prep), 30f, "the evening");
            yield return Revealed();
            Director.FillStoreroom();
            Director.OpenDebugEvening();
            yield return WaitUntil(() => IsIn(TavernPhase.Service), 10f, "service");
            NamedPatron patron = Director.FamiliarFaces.FirstOrDefault(p => p.character == CharacterIds.Bart);
            Assert.That(patron, Is.Not.Null, "Bart comes to dinner on his birthday");
            CustomerAgent guest = Director.SpawnFamiliarFace(patron);
            Assert.That(guest.Logic.Birthday, "his birthday");
            Assert.That(guest.Logic.Favourite != null ? guest.Logic.Favourite.id : null, Is.EqualTo("grilled_spider_leg"), "and his favourite");
            Director.EndServiceNow();
        }

        [UnityTest]
        public IEnumerator OnOgrinsFoundDay_HesOut_AndGrimAndKalorenDropByAtMidday()
        {
            yield return StartDaytime(gimpSeen: true, hold: true, day: 20);
            Assert.That(GameCalendar.IsBirthday(CharacterIds.Ogrin), "day 20 is his found day");
            yield return At(12 * 60 + 30);
            Assert.That(VillageLife.Doing(CharacterIds.Ogrin), Is.EqualTo("found_day"), "out in the yard");
            Assert.That(VillageLife.Doing(CharacterIds.Grim), Is.EqualTo("found_day"));
            Assert.That(VillageLife.Doing(CharacterIds.Kaloren), Is.EqualTo("found_day"));
            yield return At(10 * 60);
            Assert.That(VillageLife.Now(CharacterIds.Ogrin)?.anchor, Is.Not.EqualTo("ogrin.window"), "well, not in bed, all his found day");
        }
    }
}
