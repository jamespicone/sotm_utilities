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

            AssertIsTarget(sky.CharacterCard);
            Assert.AreEqual(sky.CharacterCard.HitPoints, 1);

            EnterNextTurnPhase();

            GoToPlayCardPhase(legacy);
            PlayCard("HeroicInterception");

            AssertIsTarget(sky.CharacterCard);
            Assert.AreEqual(sky.CharacterCard.HitPoints, 1);

            GoToUseIncapacitatedAbilityPhase(sky);
            GoToUseIncapacitatedAbilityPhase(sky);

            AssertNotTarget(sky.CharacterCard);
        }
    }
}
