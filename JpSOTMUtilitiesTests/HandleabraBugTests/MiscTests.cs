using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;

using Handelabra;
using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.UnitTest;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class MiscellaneousTests : BaseTest
    {
        [Test()]
        public void TestSelectTurnTakers()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Ra", "Megalopolis");

            StartGame();

            DecisionAutoDecideIfAble = true;
            var storedResults = new List<SelectTurnTakerDecision>();
            RunCoroutine(
                GameController.SelectTurnTakersAndDoAction(
                    null,
                    new LinqTurnTakerCriteria(tt => tt.IsHero),
                    SelectionType.DiscardCard,
                    actionWithTurnTaker: tt => {
                        Console.WriteLine($"Performing action with {tt.Name}");
                        return DoNothing();
                    },
                    numberOfTurnTakers: 2,
                    optional: false,
                    requiredDecisions: 2,
                    storedResults: storedResults,
                    allowAutoDecide: true,
                    cardSource: baron.CharacterCardController.GetCardSource()
                )
            );

            ClassicAssert.AreEqual(storedResults.Count, 2);

            if (storedResults.First().SelectedTurnTaker == legacy.TurnTaker)
            {
                ClassicAssert.IsTrue(storedResults.First().SelectedTurnTaker == legacy.TurnTaker);
                ClassicAssert.IsTrue(storedResults.Last().SelectedTurnTaker == tempest.TurnTaker);
            }
            else
            {
                ClassicAssert.IsTrue(storedResults.First().SelectedTurnTaker == tempest.TurnTaker);
                ClassicAssert.IsTrue(storedResults.Last().SelectedTurnTaker == legacy.TurnTaker);
            }
        }

        // Magma's Rage gains tokens when it deals or is dealt damage. No Executions stops it being destroyed
        // and puts it on the bottom of the deck instead; the bug was its tokens surviving that, so it came
        // back into play with them still on it.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void TestMagmasRage()
        {
            SetupGameController("BaronBlade", "ChronoRanger", "NexusOfTheVoid");

            StartGame();

            var magma = PlayCard("MagmasRage");

            DecisionNextToCard = magma;

            PlayCard("NoExecutions");

            DealDamage(chrono, magma, 1, DamageType.Melee);

            AssertTokenPoolCount(magma.FindTokenPool("MagmasRagePool"), 1);

            DealDamage(chrono, magma, 999, DamageType.Radiant);

            AssertCardSpecialString(magma, 0, "There are 0 tokens in Magma's Rage Pool.");

            PlayCard(magma);

            AssertTokenPoolCount(magma.FindTokenPool("MagmasRagePool"), 0);
        }

        [Test()]
        public void TestHarpy()
        {
            SetupGameController("BaronBlade", "TheHarpy", "Megalopolis");

            StartGame();

            foreach (var pool in harpy.CharacterCard.TokenPools)
            {
                Log.Debug($"Pool {pool} icon {pool.Icon}");
            }
        }

        // Incapacitated Huge Sky-Scraper uses "Until the start of your next turn, this card is a 1 HP
        // indestructible target", then Spite: Agent of Gloom (challenge) is defeated and replaced by
        // GloomWeaver. The challenge text says this happens "without altering ... any of the heroes or hero
        // cards", so Sky-Scraper should arguably stay a target until her next turn; the original version of
        // this test asserted that. The engine clears every status effect on the changeover, and commit
        // 19bae1e changed the asserts to match, so this now checks the engine's behaviour rather than the
        // card text. The final assert (not a target after her next turn starts) is the uncontroversial part.
        [Test()]
        public void TestSkyscraperVsSpite()
        {
            SetupGameController(
                new string[] { "Spite", "Legacy", "SkyScraper", "TheFinalWasteland" },
                promoIdentifiers: new Dictionary<string, string> { { "Spite", "SpiteAgentOfGloomCharacter" } },
                challenge: true
            );

            MoveAllCards(env, env.TurnTaker.Deck, env.TurnTaker.OutOfGame);
            StackDeck("MyndPhyre");
            StartGame();
            ResetDecisions();
            StackDeck("CollateralDamage");

            GoToPlayCardPhase(legacy);

            // Switch to huge
            DecisionSelectTarget = spite.CharacterCard;
            DecisionDoNotSelectCard = SelectionType.DiscardCard;
            PlayCard("ColossalLeftHook");
            ResetDecisions();

            // Kill Skyscraper
            DealDamage(sky, sky, 999, DamageType.Infernal);

            PlayCard("HeroicInterception");

            GoToUseIncapacitatedAbilityPhase(sky);

            // Sky becomes target
            UseIncapacitatedAbility(sky, 2);

            AssertIsTarget(sky.CharacterCard);
            ClassicAssert.AreEqual(sky.CharacterCard.HitPoints, 1);

            // Kill spite
            DealDamage(spite, spite, 999, DamageType.Fire);

            AssertNotTarget(sky.CharacterCard);

            EnterNextTurnPhase();

            GoToPlayCardPhase(legacy);
            PlayCard("HeroicInterception");

            AssertNotTarget(sky.CharacterCard);

            GoToUseIncapacitatedAbilityPhase(sky);
            GoToUseIncapacitatedAbilityPhase(sky);

            AssertNotTarget(sky.CharacterCard);
        }

        // Controlled Demolition: when an environment card is destroyed Dr. Metropolis may deal himself 2 damage,
        // and if he does that card is treated as having no game text. Checks that Obsidian Field doesn't boost
        // that self-damage, and that the "no game text" doesn't stick to Obsidian Field once it's played again.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void ControlledDemolition()
        {
            SetupGameController("BaronBlade", "DrMetropolis", "InsulaPrimalis");

            StartGame();
            RemoveMobileDefensePlatform();

            PlayCard("ControlledDemolition");
            var field = PlayCard("ObsidianField");

            QuickHPStorage(metro);
            DecisionYesNo = true;
            DestroyCard(field);
            QuickHPCheck(-2);

            PlayCard(field);
            QuickHPStorage(baron);
            DealDamage(metro, baron, 1, DamageType.Energy);
            QuickHPCheck(-2);
        }

        // Twist the Ether on Mainstay plus Preemptive Payback: Mainstay hits himself, Payback destroys itself
        // and makes him deal 3 damage *before* the first hit resolves, and Twist asks for a choice on each
        // damage. No asserts, so presumably the nested damage used to crash or misorder the decisions.
        // Fixed as of the Dec 2024 engine: resolves 28 -> 23 HP as expected (the engine still logs
        // "DealDamageAction A tried to be performed before DealDamageAction B was finished").
        [Test()]
        public void PremptiveTwist()
        {
            SetupGameController("BaronBlade", "TheVisionary", "VoidGuardMainstay");

            DecisionSelectCard = voidMainstay.CharacterCard;
            PlayCard("TwistTheEther");
            PlayCard("PreemptivePayback");

            DecisionSelectTarget = voidMainstay.CharacterCard;
            DecisionSelectDamageType = DamageType.Cold;
            DecisionYesNo = true;
            DecisionSelectFunctions = new int?[] { 0, 1, null };

            UsePower(voidMainstay.CharacterCard);
        }

        // Completionist Guise's incap: "Remove a card under your card from the game. A hero with the same name as
        // that card uses that power now." The power is on a card that's been removed from the game, and the bug was
        // the power getting cut off before it finished. Tempest's power should hit every non-hero target, so
        // presumably not every minion was hit. See also OutOfPlayPowersCausingDraws, reported as "another" bug of
        // this kind.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void OutOfPlayPowersCausingDamage()
        {
            SetupGameController(
                new string[] { "GrandWarlordVoss", "Tempest", "Guise", "TheFinalWasteland" },
                promoIdentifiers: new Dictionary<string, string> { { "Guise", "CompletionistGuiseCharacter" } }
            );

            StartGame();
            RemoveVillainCards();

            DecisionSelectCard = tempest.CharacterCard;
            DecisionSelectFunction = 1;
            UsePower(guise);

            ResetDecisions();

            DealDamage(guise, guise, 40, DamageType.Infernal);


            var minion1 = PlayCard("GeneBoundFiresworn");
            var minion2 = PlayCard("GeneBoundShockInfantry");
            var minion3 = PlayCard("GeneBoundPsiWeaver");

            QuickHPStorage(minion1, minion2, minion3);
            UseIncapacitatedAbility(guise, 1);
            QuickHPCheck(-1, -1, -1);
        }

        // Reported to Handelabra 11/08/2023 as another out-of-play power getting cut off before it's done (not a
        // regression). As above, but with The Eternal Haka's power: "Draw a card. You may discard a card with 'Haka'
        // in the title. If you do, draw 2 cards." Used while the card is out of play (via Completionist Guise),
        // the draw-2 part only drew 1 card.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void OutOfPlayPowersCausingDraws()
        {
            SetupGameController(
                new string[] { "GrandWarlordVoss", "Legacy", "Unity", "Haka", "Guise", "TheFinalWasteland" },
                promoIdentifiers: new Dictionary<string, string> { { "Guise", "CompletionistGuiseCharacter" }, { "Haka", "TheEternalHakaCharacter" } }
            );

            StartGame();
            RemoveVillainCards();

            DecisionSelectCard = haka.CharacterCard;
            DecisionSelectFunction = 1;
            UsePower(guise);

            ResetDecisions();

            DealDamage(guise, guise, 40, DamageType.Infernal);

            var discard = MoveCard(haka, "HakaOfBattle", haka.HeroTurnTaker.Hand);
            DecisionSelectCard = discard;
            QuickHandStorage(haka);
            UseIncapacitatedAbility(guise, 1);
            QuickHandCheck(2); // Drew 3, discarded 1
        }

        // On The Prowl destroys Potential Sidekick, whose damage triggers Flame Barrier and defeats Spite: Agent of
        // Gloom (challenge) while On The Prowl is still resolving, so Spite is replaced by GloomWeaver mid-card.
        // No asserts, so presumably this used to crash.
        // Fixed as of the Dec 2024 engine. Oddity: On The Prowl is removed from the game with the rest of Spite's
        // cards, then gets moved from out of game into Spite's trash when it finishes resolving.
        [Test()]
        public void TestCardInPlayWhenSpiteChangesOver()
        {
            SetupGameController(
                new string[] { "Spite", "Legacy", "Ra", "TheFinalWasteland" },
                promoIdentifiers: new Dictionary<string, string> { { "Spite", "SpiteAgentOfGloomCharacter" } },
                challenge: true
            );

            StartGame();

            MoveAllCards(env, env.TurnTaker.Deck, env.TurnTaker.OutOfGame);

            PlayCard("FlameBarrier");
            SetHitPoints(spite, 1);
            var sidekick = PlayCard("PotentialSidekick");
            DecisionSelectCard = sidekick;
            var prowl = PlayCard("OnTheProwl");
        }

        // Mr. Fixer's power with Jack Handle hits every non-hero target; Explosives Wagon is destroyed first and
        // its explosion defeats Spite: Agent of Gloom (challenge), replacing him with GloomWeaver while Jack Handle
        // is still selecting targets. No asserts, so presumably this used to crash.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void TestSpiteDefeatedWithJackHandle()
        {
            SetupGameController(
                new string[] { "Spite", "Legacy", "MrFixer", "SilverGulch1883" },
                promoIdentifiers: new Dictionary<string, string> { { "Spite", "SpiteAgentOfGloomCharacter" } },
                challenge: true
            );

            StartGame();

            MoveAllCards(env, env.TurnTaker.Deck, env.TurnTaker.OutOfGame);

            var wagon = PlayCard("ExplosivesWagon");

            PlayCard("JackHandle");
            SetHitPoints(spite, 1);
            SetHitPoints(wagon, 1);

            DecisionSelectCards = new Card[] { wagon, spite.CharacterCard };

            UsePower(fixer);
        }

        // Reported to Handelabra 27/09/2023 ("Savage Mana doesn't respect Title: Death-Caller"; also observable
        // via Guise). Haka has Title: "Death-Caller", which destroys a target Haka leaves at low HP. Savage Mana
        // says "Whenever Haka destroys a target, you may put that card beneath this card"; the bug was
        // Death-Caller's destruction not counting as Haka destroying the target, so Savage Mana didn't trigger.
        // The DestroyCard with Haka's card source is a shortcut to get the title under Haka.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void TestSavageManaVsDeathcaller()
        {
            SetupGameController("KaargraWarfang", "Haka", "TheCelestialTribunal");

            MoveAllCards(warfang, warfang.TurnTaker.FindSubDeck("TitleDeck"), warfang.TurnTaker.OutOfGame);
            StartGame();
            RemoveVillainCards();
            RemoveVillainTriggers();

            var title = PlayCard("TitleDeathCaller");

            // Simple way of getting deathcaller onto haka
            var target = PlayCard("OrrimHiveminded");

            DestroyCard(target, cardSource: haka.CharacterCard);

            AssertAtLocation(title, haka.CharacterCard.BelowLocation);

            var mana = PlayCard("SavageMana");
            var target2 = PlayCard("OrrimHiveminded");
            SetHitPoints(target2, 10);
            DecisionYesNo = true;
            DealDamage(haka, target2, 9, DamageType.Infernal);
            AssertUnderCard(mana, target2);
        }

        // Reported to Handelabra 27/09/2023. Character Witness lets Haka use the power on Huge Extremist
        // Sky-Scraper (put into play by Representative of Earth), which destroys Celestial Executioner without
        // dealing it damage. Death-Caller goes under the target that destroyed it; the bug was it going to
        // Sky-Scraper rather than Haka, the hero who actually used the power.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void TestCharacterWitnessVsDeathcaller()
        {
            SetupGameController("KaargraWarfang", "Haka", "TheCelestialTribunal");

            MoveAllCards(warfang, warfang.TurnTaker.FindSubDeck("TitleDeck"), warfang.TurnTaker.OutOfGame);
            StartGame();
            RemoveVillainCards();
            RemoveVillainTriggers();

            var title = PlayCard("TitleDeathCaller");

            var witness = PlayCard("CharacterWitness");

            DecisionSelectFromBoxIdentifiers = new string[] { "ExtremistSkyScraperHugeCharacter" };
            DecisionSelectFromBoxTurnTakerIdentifier = "SkyScraper";
            var rep = PlayCard("RepresentativeOfEarth");
            var target = PlayCard("CelestialExecutioner");

            GoToStartOfTurn(env);

            AssertAtLocation(title, haka.CharacterCard.BelowLocation);
        }

        // Impeccable Pompadour: "Whenever this card is dealt damage while at over 0 HP, Greazer deals the source of
        // that damage 2 melee damage." The engine checks the HP before the damage (TargetHitPointsBeforeBeingDealtDamage
        // > 0), so lethal damage still gets hit back; in normal mode Pompadour is indestructible and does. In challenge
        // mode it isn't indestructible, and when the damage destroys it the destruction stops the hit-back.
        // Still broken as of the Dec 2024 engine.
        [Test()]
        public void TestPompadourDestructionVsTrigger()
        {
            SetupGameController(new String[] { "GreazerTeam", "Legacy", "InsulaPrimalis" }, challenge: true);

            StartGame();

            var hair = GetCardInPlay("ImpeccablePompadour");
            SetHitPoints(hair, 1);

            QuickHPStorage(legacy);
            DealDamage(legacy.CharacterCard, hair, 1, DamageType.Melee);
            AssertInTrash(hair);

            // 2 melee, +1 nemesis, +1 from the challenge (Legacy has the Living Paycheck)
            QuickHPCheck(-4);
        }

        // Friendly Fire: "Whenever a Hero target deals a non-Hero target damage, that Hero may also deal Setback 2
        // damage". Heroic Infinitor is a hero target (until he flips), so hitting him shouldn't let Haka hit
        // Setback; the bug was Setback taking damage.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void TestFriendlyFireHeroicInfinitor()
        {
            SetupGameController("Infinitor/HeroicInfinitor", "Setback", "Haka", "Megalopolis");
            StartGame();

            Card friendly = PlayCard("FriendlyFire");

            QuickHPStorage(setback);

            DecisionYesNo = true;

            DealDamage(haka.CharacterCard, infinitor, 30, DamageType.Fire);

            QuickHPCheck(0);
        }

        // Prime Wardens Fanatic has 3 power uses (Pillars of Hercules + Smite the Transgressor). Her power plays
        // Prayer of Desperation ("Immediately end your turn"); the bug was her still being able to use her
        // remaining powers afterwards.
        // Passes in the test harness as of the Dec 2024 engine. The save is for reproducing in the game client,
        // which this test doesn't cover.
        [Test()]
        public void TestImmediatelyEndYourTurn()
        {
            SetupGameController("BaronBlade", "Fanatic/PrimeWardensFanatic", "Legacy", "Tempest", "RuinsOfAtlantis");
            StartGame();

            RemoveVillainCards();
            RemoveVillainTriggers();

            MoveAllCards(baron, baron.TurnTaker.Deck, baron.TurnTaker.OutOfGame);
            MoveAllCards(baron, baron.TurnTaker.Trash, baron.TurnTaker.OutOfGame);

            PlayCard("ThePillarsOfHercules");

            GoToPlayCardPhase(fanatic);

            PlayCard("SmiteTheTransgressor");
            var absolution = PlayCard("Absolution");

            StackDeck("PrayerOfDesperation");

            DecisionSelectTurnTaker = legacy.TurnTaker;
            GoToUsePowerPhase(fanatic);

            SaveGameToTemp("FanaticTestGame", controller: GameController);

            UsePower(fanatic.CharacterCard);

            AssertCannotPerformPhaseAction();
        }

        // Hasty Augmentation's "increase that damage by 2" against Living Force Field's reduce-by-1: Tempest's
        // 1 damage power should deal Baron Blade 2. The bug was the increase and reduction not combining correctly.
        // Fixed as of the Dec 2024 engine.
        [Test()]
        public void TestHastyDR()
        {
            SetupGameController("BaronBlade", "Unity", "Tempest", "RuinsOfAtlantis");
            StartGame();

            RemoveVillainCards();
            PlayCard("LivingForceField");

            QuickHPStorage(baron);
            DecisionSelectTurnTaker = tempest.TurnTaker;
            PlayCard("HastyAugmentation");
            QuickHPCheck(-2);
        }

        // Revenant and Kinetic Neutralizer both care about "the villain target with the highest HP", and Friction
        // and Revenant are tied for it, so the damage preview would need a decision it can't make. No asserts,
        // so presumably the preview used to crash.
        // Fixed as of the Dec 2024 engine: the preview just picks Revenant as highest.
        [Test()]
        public void TestRevenantKinetic()
        {
            SetupGameController("FrictionTeam", "Knyfe", "RuinsOfAtlantis");
            StartGame();

            RemoveVillainCards();
            RemoveVillainTriggers();

            PlayCard("KineticNeutralizer");
            var revenant = PlayCard("Revenant");
            SetToSameHitPoints(frictionTeam.CharacterCard, revenant);

            GetDamagePreviewResults(knyfe.CharacterCard, revenant, 2, DamageType.Melee);
            GetDamagePreviewResults(revenant, knyfe.CharacterCard, 2, DamageType.Melee);
        }

        private IEnumerator ChokepointTestFunc(IDecision decision)
        {
            if (decision is SelectTargetDecision std)
            {
                if (std.Choices.Count() == 2 && std.Choices.ContainsAll(new Card[] { luminary.CharacterCard, choke.CharacterCard }))
                {
                    // This runs before the decision is made, so std.SelectedCard is still null; preview the
                    // target we're going to pick instead.
                    var source = std.DamageSource;
                    var previewResults = GetDamagePreviewResults(source, choke.CharacterCard, std.DynamicAmount, std.DamageType.Value, std.IsIrreducible, std.CardSource);
                    ClassicAssert.IsNotNull(previewResults.ElementAt(0).DealDamageAction);
                    AssertDamagePreviewResults(previewResults, 0, choke.CharacterCard, 3, DamageType.Fire);
                }
            }

            return MakeDecisions(decision);
        }

        // Reported by Mikey 26/08/2025 (against Advanced Chokepoint). Sabre Battle Drone destroys itself at the end
        // of Luminary's turn; Kinetic Looter puts it face-down in Chokepoint's play area, and the drone then deals
        // 1 target 3 fire damage. The damage preview for that hit is "Unknown Result" (no DealDamageAction); in
        // the game client it shows "Could not calculate preview - no action". It's only the preview: the damage
        // itself is calculated correctly once the target is confirmed.
        // Still broken as of the Dec 2024 engine; the engine's own preview log shows it too.
        [Test()]
        public void TestBattleDroneVsChokepoint()
        {
            SetupGameController("Chokepoint", "Luminary", "InsulaPrimalis");
            StartGame();

            GoToPlayCardPhaseAndPlayCard(choke, "KineticLooter");

            GoToPlayCardPhase(luminary);
            var drone = PlayCard("SabreBattleDrone");

            DecisionSelectTarget = choke.CharacterCard;
            DecisionDestroyCard = drone;

            GameController.OnMakeDecisions += ChokepointTestFunc;
            GoToEndOfTurn(luminary);
            GameController.OnMakeDecisions -= ChokepointTestFunc;
        }

        // Same bug as above, previewing the face-down drone's damage after the turn instead of during the decision.
        // Still broken as of the Dec 2024 engine.
        [Test()]
        public void TestBattleDroneVsChokepoint2()
        {
            SetupGameController("Chokepoint", "Luminary", "InsulaPrimalis");
            StartGame();

            GoToPlayCardPhaseAndPlayCard(choke, "KineticLooter");

            GoToPlayCardPhase(luminary);
            var drone = PlayCard("SabreBattleDrone");

            DecisionSelectTarget = choke.CharacterCard;
            DecisionDestroyCard = drone;

            GoToEndOfTurn(luminary);

            var previewResults = GetDamagePreviewResults(new DamageSource(GameController, drone), choke.CharacterCard, c => 3, DamageType.Fire, cardSource: FindCardController(drone).GetCardSource());
            ClassicAssert.IsNotNull(previewResults.ElementAt(0).DealDamageAction);
            AssertDamagePreviewResults(previewResults, 0, choke.CharacterCard, 3, DamageType.Fire);
        }

        // Reported to Handelabra 05/09/2025 (originally noted by Ruduen; this reproduces it with base game content).
        // AddAdditionalPhaseActionTrigger removes the extra phase action in a before-destroy action, but cards can
        // leave play without being destroyed, which leaves the phase action count wrong.
        // Onboard Cooling Systems gives an extra power. Overhaul Loadout returns it to hand and replays it during
        // the power phase; it should still be 2 powers, but it's 3. (The log shows a ReducePhaseActionCountAction
        // being attempted as it leaves play, but it's blocked with "Onboard Cooling Systems cannot do anything
        // else" because the card is already out of play.) Replaying it then adds another.
        // Still broken as of the Dec 2024 engine.
        [Test()]
        public void TestReturnPhaseActionGranterToHand()
        {
            SetupGameController("BaronBlade", "Benchmark", "Megalopolis");
            StartGame();

            RemoveVillainCards();
            RemoveVillainTriggers();

            var onboard = GoToPlayCardPhaseAndPlayCard(bench, "OnboardCoolingSystems");

            GoToUsePowerPhase(bench);

            // One power base, one from Onboard Cooling Systems
            AssertPhaseActionCount(2);

            DecisionReturnToHand = onboard;
            DecisionSelectCardToPlay = onboard;
            PlayCard("OverhaulLoadout");

            // Still one power base, one from Onboard Cooling Systems
            AssertPhaseActionCount(2);
        }

        // Reported to Handelabra 27/12/2024. GameController.SelectHeroToDrawCards has an off-by-one when handling
        // requiredDraws: draw i is only optional if (requiredDraws.Value < i), which should be <=. Passing 1 gives
        // 2 required draws, and so on.
        // Still broken as of the Dec 2024 engine (which predates the report).
        [Test()]
        public void TestSelectHeroToDrawCardsRequiredDraws()
        {
            SetupGameController("BaronBlade", "Legacy", "Megalopolis");
            StartGame();

            DecisionSelectTurnTaker = legacy.TurnTaker;
            DecisionYesNo = false;

            QuickHandStorage(legacy);
            RunCoroutine(
                GameController.SelectHeroToDrawCards(
                    legacy,
                    numberOfCards: 3,
                    requiredDraws: 1,
                    cardSource: legacy.CharacterCardController.GetCardSource()
                )
            );

            // One required draw, then decline the other two
            QuickHandCheck(1);
        }

        // Reported to Handelabra 15/04/2024. GameController.GetAllPowersForCardController only includes a card's
        // printed powers if cc.Card.HasPowers, but then reads them from cc.CardWithoutReplacements. When Called to
        // Judgement lets a hero use the power on the hero next to Representative of Earth, that card is replaced
        // (AskIfCardIsReplaced) with the power user's character card. If the power user's character card has no
        // printed powers, e.g. a mod hero whose powers all come from AskIfContributesPowersToCardController,
        // HasPowers is false and the representative's power isn't offered ("Called to Judgement would allow ... to
        // use a power, but they have no more usable powers left this turn"). Heroes with printed powers are fine.
        // Handelabra's view is that a hero has an innate power by definition, so it's low priority.
        // Uses the test mod's Powerless Test Hero, whose character card has no printed powers.
        // Still broken as of the Dec 2024 engine.
        [Test()]
        public void TestCalledToJudgementWithPowerlessHero()
        {
            SetupGameController("BaronBlade", "Jp.SOTMUtilities.TestMod.PowerlessTestHero", "Legacy", "TheCelestialTribunal");
            StartGame();

            RemoveVillainCards();
            RemoveVillainTriggers();

            var powerless = GetCard("PowerlessTestHeroCharacter");

            // Ra represents Earth; his power is "Ra deals 1 target 2 fire damage"
            DecisionSelectFromBoxIdentifiers = new string[] { "RaCharacter" };
            DecisionSelectFromBoxTurnTakerIdentifier = "Ra";
            DecisionSelectCard = powerless;
            DecisionSelectTarget = baron.CharacterCard;

            QuickHPStorage(baron);
            PlayCard("CalledToJudgement");
            QuickHPCheck(-2);
        }

        private List<Phase> PhasesTakenWithEmptyHand(bool concordantHelm)
        {
            SetupGameController("BaronBlade", "LaComodora", "Legacy", "Megalopolis");
            StartGame();

            RemoveVillainCards();
            RemoveVillainTriggers();

            if (concordantHelm)
            {
                PlayCard("ConcordantHelm");
            }

            GoToStartOfTurn(comodora);
            MoveAllCards(comodora, comodora.HeroTurnTaker.Hand, comodora.TurnTaker.OutOfGame);

            var phases = new List<Phase>();
            while (GameController.Game.ActiveTurnTaker == comodora.TurnTaker)
            {
                phases.Add(GameController.Game.ActiveTurnPhase.Phase);
                EnterNextTurnPhase();
            }

            return phases;
        }

        // Reported to Handelabra 15/07/2023. If a hero can take their phases in any order (Concordant HELM, or
        // Fugue State Parse's incap) and has no cards in hand, they still get a play phase, which can be triggered
        // on. Without a phase-ordering effect, an empty hand makes the engine skip the play phase entirely, so
        // nothing can trigger on it (e.g. on leaving the play phase). It should be consistent either way; this
        // checks La Comodora's empty-hand turn goes through the same phases with and without Concordant HELM.
        // Still broken as of the Dec 2024 engine: with the Helm it's Start, PlayCard, UsePower, DrawCard, End;
        // without it the PlayCard phase is missing.
        [Test()]
        public void TestEmptyHandPlayPhaseWithAndWithoutPhaseOrdering()
        {
            var withHelm = PhasesTakenWithEmptyHand(concordantHelm: true);
            var withoutHelm = PhasesTakenWithEmptyHand(concordantHelm: false);

            CollectionAssert.AreEqual(withHelm, withoutHelm);
        }

        private OnDealDamageStatusEffect MakeOnDealDamageStatusEffect(Card target)
        {
            var effect = new OnDealDamageStatusEffect(
                legacy.CharacterCard,
                "SomeMethod",
                "Test effect",
                new TriggerType[] { TriggerType.DealDamage },
                legacy.TurnTaker,
                legacy.CharacterCard
            );
            effect.TargetCriteria.IsSpecificCard = target;
            effect.NumberOfUses = 1;
            return effect;
        }

        // Reported to Handelabra 25/04/2023 (found by Origami Swami), part 1. OnDealDamageStatusEffect.IsSameAs
        // returned true whenever ReflectionStatusEffect.IsSameAs did, even if its own criteria didn't match (| rather
        // than &), and didn't check that the other effect was also an OnDealDamageStatusEffect.
        // Fixed as of the Dec 2024 engine. The other ReflectionStatusEffect subclasses (OnPhaseChangeStatusEffect,
        // OnDrawCardStatusEffect, OnGainHPStatusEffect) still use ReflectionStatusEffect.IsSameAs, which only compares
        // card identifiers and expiry criteria; e.g. a second OnPhaseChangeStatusEffect from the same card with a
        // different method is treated as the same effect and dropped.
        [Test()]
        public void TestOnDealDamageStatusEffectIsSameAs()
        {
            SetupGameController("BaronBlade", "Legacy", "Megalopolis");
            StartGame();

            var effect = MakeOnDealDamageStatusEffect(baron.CharacterCard);

            ClassicAssert.IsTrue(effect.IsSameAs(MakeOnDealDamageStatusEffect(baron.CharacterCard)));

            // Different target criteria
            ClassicAssert.IsFalse(effect.IsSameAs(MakeOnDealDamageStatusEffect(legacy.CharacterCard)));

            // Same card and method, but not an OnDealDamageStatusEffect
            var phaseChange = new OnPhaseChangeStatusEffect(legacy.CharacterCard, "SomeMethod", "Test effect", new TriggerType[] { TriggerType.PhaseChange }, legacy.CharacterCard);
            ClassicAssert.IsFalse(effect.IsSameAs(phaseChange));
        }

        // Reported to Handelabra 25/04/2023 (found by Origami Swami), part 2. OnDealDamageStatusEffect.
        // CombineWithExistingInstance ignored CanEffectStack, so identical limited-use effects were merged even when
        // they were meant to stack.
        // Fixed as of the Dec 2024 engine (with CanEffectStack false they're still merged, as intended).
        [Test()]
        public void TestOnDealDamageStatusEffectCanEffectStack()
        {
            SetupGameController("BaronBlade", "Legacy", "Megalopolis");
            StartGame();

            var first = MakeOnDealDamageStatusEffect(baron.CharacterCard);
            first.CanEffectStack = true;
            var second = MakeOnDealDamageStatusEffect(baron.CharacterCard);
            second.CanEffectStack = true;

            RunCoroutine(GameController.AddStatusEffect(first, true, legacy.CharacterCardController.GetCardSource()));
            RunCoroutine(GameController.AddStatusEffect(second, true, legacy.CharacterCardController.GetCardSource()));

            ClassicAssert.AreEqual(2, GameController.Game.StatusEffects.OfType<OnDealDamageStatusEffect>().Count());
        }
    }
}
