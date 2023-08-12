using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.UnitTest;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class IsTurnTakersTurnPriorToOrDuringPhaseTests : BaseTest
    {
        [Test()]
        public void TestRegularPhaseOrderDuringTurn()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            GoToStartOfTurn(legacy);

            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
        }

        [Test()]
        public void TestWeirdPhaseOrder()
        {
            SetupGameController("BaronBlade", "LaComodora", "Tempest", "Megalopolis");

            StartGame();

            RemoveVillainTriggers();
            RemoveVillainCards();

            PlayCard("ConcordantHelm");
            
            GoToStartOfTurn(comodora);

            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            DecisionSelectTurnPhase = comodora.TurnTaker.TurnPhases.Where(tp => tp.Phase == Phase.DrawCard).First();
            EnterNextTurnPhase();

            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            DecisionSelectTurnPhase = comodora.TurnTaker.TurnPhases.Where(tp => tp.Phase == Phase.UsePower).First();
            EnterNextTurnPhase();

            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            DecisionSelectTurnPhase = comodora.TurnTaker.TurnPhases.Where(tp => tp.Phase == Phase.PlayCard).First(); ;
            EnterNextTurnPhase();

            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            Assert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
        }

        Phase expectedPhase = Phase.Start;
        private IEnumerator CheckEphemeralPhases(GameAction action)
        {
            if (! (action is PhaseChangeAction)) { yield break; }

            Handelabra.Log.Debug(action.ToString());
            lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start);
            switch (expectedPhase)
            {
                case Phase.Start:
                    Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
                    Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
                    Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
                    expectedPhase = Phase.PlayCard;
                    break;
                case Phase.PlayCard:
                    Assert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
                    Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
                    Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
                    expectedPhase = Phase.End;
                    break;
                case Phase.End:
                    Assert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
                    Assert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
                    Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
                    break;
            }

            yield break;
        }

        [Test()]
        public void TestEphemeralTurns()
        {
            SetupGameController("LaCapitanTeam", "Legacy", "Megalopolis");

            RemoveVillainTriggers();
            RemoveVillainCards();

            StartGame();

            var stitch = PlayCard("StitchInTime");
            StackDeck("AGoodTimeSpan");

            Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            Assert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            Assert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            Assert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            expectedPhase = Phase.Start;
            GameController.OnDidPerformAction += CheckEphemeralPhases;

            DestroyCard(stitch);

            GameController.OnDidPerformAction -= CheckEphemeralPhases;            

            EnterNextTurnPhase();
        }
    }
}
