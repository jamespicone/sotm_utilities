using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using NUnit.Framework.Legacy;

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

            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsFalse(legacy.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsFalse(tempest.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
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

            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            DecisionSelectTurnPhase = comodora.TurnTaker.TurnPhases.Where(tp => tp.Phase == Phase.DrawCard).First();
            EnterNextTurnPhase();

            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            DecisionSelectTurnPhase = comodora.TurnTaker.TurnPhases.Where(tp => tp.Phase == Phase.UsePower).First();
            EnterNextTurnPhase();

            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            DecisionSelectTurnPhase = comodora.TurnTaker.TurnPhases.Where(tp => tp.Phase == Phase.PlayCard).First(); ;
            EnterNextTurnPhase();

            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.DrawCard));
            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.UsePower));
            ClassicAssert.IsFalse(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(comodora.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
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
                    ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
                    ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
                    ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
                    expectedPhase = Phase.PlayCard;
                    break;
                case Phase.PlayCard:
                    ClassicAssert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
                    ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
                    ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
                    expectedPhase = Phase.End;
                    break;
                case Phase.End:
                    ClassicAssert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
                    ClassicAssert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
                    ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));
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

            ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            EnterNextTurnPhase();

            ClassicAssert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.Start));
            ClassicAssert.IsFalse(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.PlayCard));
            ClassicAssert.IsTrue(lacapitanTeam.IsTurnTakersTurnPriorToOrDuringPhase(Phase.End));

            expectedPhase = Phase.Start;
            GameController.OnDidPerformAction += CheckEphemeralPhases;

            DestroyCard(stitch);

            GameController.OnDidPerformAction -= CheckEphemeralPhases;            

            EnterNextTurnPhase();
        }
    }
}
