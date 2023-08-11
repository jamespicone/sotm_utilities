using System;
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
    }
}
