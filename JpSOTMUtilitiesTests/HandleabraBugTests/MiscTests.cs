﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

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

            Assert.AreEqual(storedResults.Count, 2);

            if (storedResults.First().SelectedTurnTaker == legacy.TurnTaker)
            {
                Assert.IsTrue(storedResults.First().SelectedTurnTaker == legacy.TurnTaker);
                Assert.IsTrue(storedResults.Last().SelectedTurnTaker == tempest.TurnTaker);
            }
            else
            {
                Assert.IsTrue(storedResults.First().SelectedTurnTaker == tempest.TurnTaker);
                Assert.IsTrue(storedResults.Last().SelectedTurnTaker == legacy.TurnTaker);
            }
        }

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
            Assert.AreEqual(sky.CharacterCard.HitPoints, 1);

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
            DealDamage(haka, target2, 9, DamageType.Infernal);
            AssertUnderCard(mana, target2);
        }

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
    }
}
