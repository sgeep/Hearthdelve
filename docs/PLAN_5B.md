# Phase 5b: the fantasy calendar and the first festival (plan, proposed 2026-10-10)

_Proposed for the owner's approval. Nothing is built until approved. `main` only; `release/0.4i` stays frozen._

## 0. Inputs

- **Playtest input** (`docs/PLAYTEST_4I.md`, *Phase 5 input*): **none logged yet.** Round 1 is waiting for the first answers; nothing from testers shapes this plan. If findings arrive before 5b is built, any that touch the day, the HUD or the village are read in at the first checkpoint.
- **Story** (`docs/STORY.md`): §4 Act II beat 5 (*Karias Remembrance Day: Maximo's speech and song, the village gathered; Gimp's absence, loud; Glimmer flickers at the name; Bart sings his half a song, and the keeper notices which half*) and §8's 5b row (*Remembrance Day as the first festival; Ogrin's "found day" as his birthday; acts never wait on a date: a festival can deepen a beat, never gate one*).
- **Locked rules** (CLAUDE.md, GDD §11.1): the calendar creates anticipation, never punishing deadlines; nothing story-critical is missable because of a date; schedules stay on the game day and minute; month and year lengths, weekday names, birthdays and festival dates are unlocked until this plan's choices.

## 1. The experience

**The calendar:** *the village has a rhythm the keeper starts to live by: they know what day it is, something is always coming, and looking forward to it is part of the pleasure of an ordinary day.*

**Karias Remembrance Day:** *Kariaston remembers who it's named for, and invites the keeper in: for one day the odd, funny village is quiet and serious together, and the keeper feels the weight of a story the village only half tells.* (Lenses: **Community** (a shared ritual is what makes a place a home), **Anticipation** (the days before matter as much as the day), **Story machine** (one fixed day that keeps meeting a changing story, year after year).)

**What's essential:** knowing it's coming; the village gathered at the memorial; Maximo meaning every word; someone conspicuously missing; the evening at Tally Ho! carrying it on. **Not essential:** festival games, stalls, decorations everywhere, a new tune.

## 2. How long things take (for the choices below)

A day is about **12–16 real minutes**: the daytime about 4 (8 am to 5 pm at 0.43 s a game minute), Prep 1–2, service 2.5 plus the results, the delve 4–7, the night half a minute. **A 45–60 minute session sees about 3–5 days; a long session about 6–8; a weekend about 15–30.** A month the player should feel the shape of fits in a few sessions; a year a player meets more than once.

## 3. The calendar's choices (all unlocked until the owner chooses)

### C1. Month and year length

| Option | A year | What it's like | |
|---|---|---|---|
| **A. 4 months of 28 days** | 112 days | Stardew's shape; a month is 7–9 sessions, a year ~30. A festival comes round about once in 25–35 hours. | Too slow for a 4-minute day: most players see Remembrance once. |
| **B. 4 months of 14 days** | 56 days | A month is 3–4 sessions, a year ~15. Remembrance comes round every 12–17 hours of play. | **Recommended.** Long enough to feel like seasons later (5g: one month per season), short enough that a yearly festival returns, and birthdays don't cluster. |
| **C. 4 months of 10 days** | 40 days | A month is 2–3 sessions; a year ~10. | Fast; festivals risk feeling routine, and 4 seasons of 10 days is short for crops. |

### C2. The week

| Option | What it's like | |
|---|---|---|
| **A. 7 named days, 2 a month** (with C1-B) | Familiar; weekdays can carry routine later ("Bart plays on Fifthday"). | **Recommended**: two weeks a month gives anticipation a short horizon ("next Restday"). |
| **B. 5-day week** | More weeks a month, more rhythm; less familiar. | |
| **C. No week; dates only** | Simplest; nothing for routines to hang on. | |

Weekdays in 5b only show on the date; no schedule uses them yet (later milestones may).

### C3. Names (the owner writes them; my drafts only show the tone)

Months named for the watch's year, not the real calendar: e.g. *Thawing, Highsun, Emberfall, Deepfrost*. Weekdays plain and local: e.g. *Firstday, Marketday, Midweek, Hearthday, Fifthday, Lampday, Restday*. Options: **A** the owner's own names (recommended: it's writing, and the owner's world); **B** my drafts as placeholders until then; **C** numbers only ("the 3rd of the 2nd month"). Names go through the Localization table, American spelling, lower case where not proper nouns (months and weekdays are proper nouns in English: capitalised).

### C4. Showing the date

| Option | What it's like | |
|---|---|---|
| **A. On the HUD under the clock** ("Hearthday, 9 Thawing") | Always there; one more line on a small screen. | |
| **B. Only in the world**: a calendar board in Tally Ho! by the menu board, and people saying it | The most immersive; a player can lose track. | |
| **C. Both: a short HUD line ("9 Thawing") and the board for what's coming** | The HUD answers "what day is it", the board answers "what's coming" (the next festival, birthdays this month), so anticipation lives in a place. | **Recommended.** |

**No year number is shown in 5b.** A year counted from the founding ("the 352nd year of the watch") would tell the player how old Maximo is, which is the spine's Act III reveal (STORY.md §2, the binding). The board and the HUD show month and day only; the keeper's own years can be counted later ("your second Remembrance").

### C5. Birthdays (which, and what they do)

Which in 5b:
- **A. Ogrin's found day only.**
- **B. Ogrin's found day, Orik and Bart** (one in three of the months, none in Remembrance's month). **Recommended:** enough to prove a birthday is worth noticing and to spread them out; the rest come with later milestones.
- **C. Every villager.**

**Not Maximo, Boog or Gimp yet:** a bound sealer's birthday is either a joke about not ageing or a count of years, and both point at the binding before Act III.

What a birthday does (the smallest version that matters, all from systems that exist):
1. **The board** shows it a few days ahead, and on the day the person's talk opens with it (a short birthday line, theirs).
2. **They come to dinner that evening** (always, beside the usual familiar faces).
3. **They ask for their favourite dish** as a special request (4f's requests; a birthday request is extra to the evening's cap). Each gets a favourite on their `CharacterDefinition`.
4. **Met:** a thank-you and a small Affinity deed (`remembered_birthday`, through `RelationshipRules`; repeats fade by the existing curve). **Missed:** they enjoy the evening anyway, and say nothing; nothing worse.
5. **Ogrin's found day:** he's out and well that day whatever his health (Grim: "he's always well on his found day"), and Grim and Kaloren drop by the cottage at midday (schedules). Ogrin is a child, not a patron: his favourite dish is something the keeper can take him (**Proposed:** carried out of Tally Ho! and handed over at the cottage, using the plate carrying that already exists; if that's too much for 5b, his birthday is dialogue and the visit only).

No gifts system: a cooked favourite is the gift (meaningful interaction over a new system).

### C6. Festival dates

- **Remembrance Day:** one fixed date a year (the anniversary of the sealing). Options for the first one: **A** about day 10 of a new game (a few sessions in, after the keeper knows the village: **recommended**); **B** about day 20 (after more of Act I settles); **C** wherever the date falls for a random start (unreliable anticipation).
- A new game always starts on the same date (a tuning value, `CalendarConfig.startDate`), chosen so Remembrance falls on day 10. Never on days 1–2 (arrival day and the first free day carry the opening).
- **Proposed** for later milestones (not built): one festival a month eventually (a harvest feast, a Delver's Feast, a winter one), each its own milestone's choice.

## 4. Storage: no save change

The calendar is **derived from the day count**, which the save already holds (`GameState.Cycle.Day`): `date = start + (day − 1)`, month and weekday by arithmetic over `CalendarConfig`. Festival and birthday days are dates in the config; a day's facts (Remembrance today? whose birthday?) are pure functions of the day. What happened on them is recorded where one-time beats already live (`StoryState.SeenHints`, `beat:remembrance_speech:<year>`, `beat:birthday:<id>:<year>`), as the 4h community beats are. **Save version stays 10;** testers' saves load as they are and get dates from their day count.

The one rule this creates: **`CalendarConfig`'s start date and month lengths are locked once this ships to players**, or every existing save's dates move (a later change would be a save migration, planned then).

## 5. Karias Remembrance Day, the smallest version

**The day before (anticipation):** the board says "tomorrow"; at least one villager mentions it in passing (Maximo rehearsing, Orik reminding the keeper), as a short line in their talk.

**The morning:** white flowers at the memorial (existing flower drawings, shown only on the eve and the day); on coming downstairs, Orik says it's Remembrance and Maximo speaks at the memorial at ten.

**10:00–11:30, the gathering (schedules):** Maximo at the memorial; Kaloren, Grim, Ogrin (if well) and Bart in a loose ring of new anchors round it; Musashi watches from his cart, which stays open (the square is his; closing the market would cost the player for a ritual: immersion not worth the frustration). **Gimp never comes up on Remembrance Day** (his visit rule is off that day).

**Maximo's speech:** when the keeper comes into the square during the gathering, the speech plays as one conversation (the clock stops for it, as for all talk; the day's tune holds quiet through it). Maximo, theatrical and completely sincere: his apprentice, who held his staff like a wish come true, who gave his life so this village could stand where it stands; the creed. One or two short lines from the others (Grim's dry one, Kaloren's kind one, Ogrin's question). Once a year. Seeded once for the node editor, the owner's to rewrite.

**If the keeper misses it** (out in the garden, indoors, busy): Maximo's talk that day carries it ("you missed it! no matter. tonight, at your tavern, i'll give the short one"), and the evening carries the rest.

**The evening at Tally Ho! (always reached):** a remembrance supper. Every named villager who dines comes (Maximo, Bart, Grim, Musashi, Kaloren; Ogrin goes home with Grim), as familiar faces, no Gimp. When service opens Maximo raises a toast to Karias (a bark), and the room answers. Boog, to the keeper, quietly: Gimp never comes up today. Never has. (Act I safe: it shows the hostility already in the game, and explains nothing.) Bart plays something warm and ordinary.

**Who shows up and who doesn't:** everyone in the village; not Gimp. The absence is noticed by Boog and by Maximo's one glance at the hatch; never explained.

## 6. Story timing: what's safe before Act II

| Beat (STORY.md Act II 5) | Before Act II? | Why |
|---|---|---|
| Maximo's speech and toast | **Safe** | Karias, the founding, the creed: Act I canon. Never a number of years; nobody remarks on Maximo's age. |
| The village gathered | **Safe** | |
| Gimp's absence (Boog's line) | **Safe** | Shows the hostility the game already shows; explains nothing. |
| Glimmer flickering at the name | **Waits for Act II** | It ties Glimmer to Karias. Built in 5b behind a story-act condition that's false until Act II exists, or (recommended) not built until Act II's milestone. |
| Bart's half a song | **Waits for Act II** | Which half he won't sing points at the Five. Before then he plays an ordinary song. |
| Maximo's song | **Safe**, if it's his own heroic song about Karias the hero (his rude songs stay for the tavern) | |

5b builds only the safe beats. The waiting ones are listed in `STORY.md` for Act II's milestone, which adds them to the same day.

## 7. The rules

- **A festival deepens a story beat; it never gates one.** No act, quest or relationship ever waits for a date; Remembrance and birthdays only add.
- **Nothing is missable.** Every year's Remembrance has the same essentials (a later year can't hold a one-time story reveal unless that reveal is also reachable on an ordinary day); the speech has the evening's short version; a birthday request missed costs nothing.
- **Anticipation, never deadlines.** The board and people say what's coming; nothing fails if the keeper doesn't prepare. No timers, no "festival ends in".
- **Sleeping through it:** the keeper can't skip a day, but can spend it indoors or go straight to Prep. They still get Orik's morning line, Maximo's (or someone's) passing line if met, and the supper and toast at Tally Ho!, so the day is felt even without the gathering. The next day, Bart or Maximo mentions it once.
- **Continuing mid-festival:** dates and schedules derive from the day and minute, so a saved Remembrance morning resumes as Remembrance, with the speech still to come if it hadn't played.

## 8. Systems and files

**Pure (Shared, EditMode-tested):**
- `Shared/Calendar/CalendarConfig` (ScriptableObject: months and their lengths, the week, start date, festivals and birthdays as dates, loc keys for names) and `CalendarRules` (date of a day, weekday, year index, `IsFestival`, `Birthdays(day)`, `NextOccurrence`, `DaysUntil`).
- `ScheduleRules`: one new condition kind, **Festival** (`ScheduleCondition.OnFestival("remembrance")` and its negation), read from the calendar; `VillageDays.GimpVisit` false on Remembrance.
- `CommunityRules.Tonight`: on Remembrance, every named patron; on a birthday, that person plus the usual draw.
- Requests: a birthday request alongside the evening's cap (`ServiceConfig` unchanged; the birthday one is extra).

**Village and tavern (thin):**
- Schedules: Remembrance blocks (10:00–11:30) added once to the existing schedule assets by a one-time pass that only inserts blocks it doesn't find (schedule assets are tuned by hand); new anchors round the memorial in `VillageContent.KariastonAnchors` (checked by `KariastonWalkTests` automatically).
- `FestivalDressing` (Village): shows the memorial's flowers on the eve and the day.
- `RemembranceGathering` (Village): starts the speech when the keeper reaches the square in the window, once a year; holds the music quiet (`MusicHolds`).
- Tally Ho!: the toast and Boog's line through `AmbientBarks`/a tavern moment at service open on Remembrance.
- **Calendar board:** an inspectable fixture by the menu board in Tally Ho!, opening a small panel (UI assembly): today, the next festival and days to it, birthdays this month. Art: a notice board from owned art (catalog search in Step 1; recorded in ASSET_MAP).
- **HUD:** a date line under the clock (STM, Body style, `TypographyTests` fit, `ContrastTests`).

**Story (adapters and seeds):**
- Lua: `HH_Date()` (a localized date string), `HH_Festival("remembrance")` (today?), `HH_DaysUntil("remembrance")`, `HH_Birthday("ogrin")`.
- Conversations seeded once: `Festival/Remembrance/Speech`, `Festival/Remembrance/Missed`, the eve and morning lines, `Ambient/RemembranceToast`, `Birthday/Ogrin|Orik|Bart`. Adding the birthday and Remembrance lines to the existing hubs (`Maximo/Hub`, `Orik/Hub`, the birthday people's hubs) is a one-off author's edit, only where the hub is still as written; anything the owner changed is left and listed for the owner to link by hand.
- Characters: `favouriteDish` on `CharacterDefinition` (Ogrin, Orik, Bart in 5b); deed `remembered_birthday`.

**Music:** no new track. The day's tune pauses for the speech and comes back; the supper keeps service's tune. (A festival tune is the owner's call later; HeatleyBros is the source.)

**Localization:** month and weekday names, the date formats, the board's labels, in the UI tables; `TextStyleTests` glyphs, `TypographyTests` fit.

**Save:** none (§4).

## 9. Tests

- **EditMode:** `CalendarRules` (dates, weekdays, month and year roll-over, the start date, Remembrance on day 10 of a new game and every year after, never days 1–2, `DaysUntil`, birthdays never on a festival); the Festival schedule condition; Gimp off on Remembrance; `CommunityRules` on Remembrance and birthdays; every new string fits and draws in Silver; the schedule pass adds its blocks once and never twice.
- **PlayMode:** on Remembrance at 10:00 everyone is at the memorial and Gimp isn't in Tally Ho!; the speech plays once when the keeper reaches the square, and not again that year; missed, Maximo's line carries it and the supper toast plays; the remembrance supper brings the village; a birthday brings that person to dinner with their request, met gives the deed, missed gives nothing worse; a save on Remembrance morning continues as Remembrance with the speech still to come; the HUD and the board show the derived date; an old day-2 save (the testers' fixtures) shows a date and loads unchanged.
- Both full suites, Slow included, before the push.

## 10. Checkpoints

**A, the calendar** (Steps 1–3): the config and rules, the HUD date and the board, the Lua functions, birthdays (Ogrin, Orik, Bart if C5-B). **Playtest question:** do I know what day it is, and is there something I'm looking forward to?

**B, Remembrance Day** (Steps 4–6): the schedules and anchors, the flowers, the gathering and the speech, the missed path, the supper and toast, Gimp's absence. **Playtest question:** does the village feel like it's remembering someone, and do I feel let in?

## 11. What the owner does

- Choose C1–C6 (or say "recommended" for any).
- Write or approve the month and weekday names (C3), and the favourite dishes (Ogrin, Orik, Bart).
- After seeding: rewrite the speech and the birthday lines in the node editor as you like (they're first drafts).
- Playtest each checkpoint; on the PC, jump the day with the development skip (F8) to reach Remembrance quickly.

## 12. Risks

- **The HUD gets crowded.** The date line is short ("9 Thawing") and tested to fit; the board carries the rest.
- **Festival lines go stale on repeat years.** The speech gets a second-year variant at most in 5b; later years reuse with small changes; Act II adds its own beats to the same day.
- **Leaking the spine:** no year counts, no ages, no Glimmer or Bart's half-song before Act II (§6); the speech's drafts are checked against `STORY.md` §2.

## Approved (2026-10-10)

The owner approved the plan with every recommendation: **C1-B** (4 months of 14 days, a 56-day year), **C2-A** (7 named days), **C4-C** (a HUD line and the board), **C5-B** (Ogrin's found day, Orik, Bart), **C6-A** (Remembrance around day 10). **C3:** the drafts as placeholders, renamed by the owner in the string table later; names live only in the table, never in code or ids. **Ogrin's found day:** dialogue and the midday visit only (no plate carried to the cottage in 5b). **Favourites:** three dishes from the current menu. Before Checkpoint A ships, the first year's dates go to the owner (the start date and month lengths lock then). Build Checkpoint A, then stop for the playtest.

## As built: Checkpoint A (2026-10-10)

**The first year** (the start date and month lengths lock once this reaches players; names are the placeholders):

| | Game day | Date |
|---|---|---|
| Arrival day | 1 | Firstday, 1 Thawing |
| **Karias Remembrance Day** | **10** | Midweek, 10 Thawing |
| **Ogrin's found day** | **20** | Lampday, 6 Highsun |
| **Orik's birthday** | **37** | Marketday, 9 Emberfall |
| **Bart's birthday** | **46** | Hearthday, 4 Deepfrost |
| Year two begins | 57 | Firstday, 1 Thawing |
| Remembrance, year two | 66 | Midweek, 10 Thawing |

- **The calendar** (`Shared/Calendar`): `CalendarConfig` (`Data/Config/Calendar.asset`, made once; its start date and month lengths lock once shipped) and pure `CalendarRules` (dates, weekdays, festivals, birthdays, what's coming, a yearly beat); `GameCalendar` reads today from the day count. **No save change** (version 10): once-a-year things are recorded as `beat:<what>:<year>` in `StoryState.SeenHints`.
- **The date** on its own tab under the clock ("2 Thawing"); the harvest note moved one tab down.
- **The calendar board** beside the menu board in Tally Ho! (More Signage's framed board, cells 16–17 of the front row, reserved like the menu board's): today's long date and up to four things in the next fortnight, soonest first ("Karias Remembrance Day in 8 days", "Ogrin's found day, 6 Highsun"). Holds the clock while read; back, Esc or B closes it.
- **Names** (months, weekdays, the festival) are added to the UI table only when missing (`LocalizationBuilder.AddMissing`), so the owner's renames survive every rebuild; formats and labels are generated as usual.
- **Schedules:** a new condition kind (`Calendar`: `festival:<id>`, `birthday:<character>`); Ogrin is always well on his found day; his found-day midday (12:00–13:00): Ogrin in the yard, Grim beside him, Kaloren at the door, each with a happy face now and then (blocks added once to the hand-tuned schedules, never twice).
- **Birthdays:** on the day, talking to the person plays their birthday conversation first, once a year (`Birthday/Ogrin|Orik|Bart`, first drafts for the node editor; C# decides when, the graph what; no hub was edited). **Bart** always comes to dinner on his birthday, orders **grilled spider leg** if it's on the menu, and it's a special request beyond the evening's cap; met, the deed `remembered_birthday` (warmth, only Bart learns of it). **Orik's** favourite is **Brackenford ale** and **Ogrin's** **eggs on toast**: named in their lines, with no meal for either in 5b.
- **Lua:** `HH_Festival(id)`, `HH_DaysUntil(id)`, `HH_Birthday(id)`, `HH_Date()`.
- **Deviation:** Orik works the evening, so his birthday is his line ("one ale tonight, an' i'll pour it myself") and the board, not a dinner; a night off as a guest needs his look as a patron (later, if wanted). Favourites are on the calendar's birthday entries rather than `CharacterDefinition` (the tavern reads the calendar, not the story's cast).
- **Known:** a tester's saved layout with a piece on cells 16–17 of the front row would overlap the board (no error; the 4h menu board had the same exposure).
- **Tests:** EditMode `CalendarTests` (dates and roll-over, Remembrance on day 10 and every 56 days, the first year's birthdays outside its month, what's coming, the yearly beat, Ogrin well on his found day, a birthday guest always at dinner, the names only in the table), the deed vocabulary and the tavern's baseline updated; PlayMode `CalendarPlayTests` (the HUD date and the board, Bart's birthday talk once and his dinner and favourite, Ogrin's found-day midday). Results: EditMode 847 passed, 0 failed (1 skipped); PlayMode 289 passed, 0 failed (31 explicit captures skipped), Slow included.
