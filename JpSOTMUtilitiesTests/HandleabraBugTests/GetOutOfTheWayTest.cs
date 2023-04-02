using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.UnitTest;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class GetOutOfTheWayTest : BaseTest
    {
        [Test()]
        public void TestGetOutOfTheWayDistinctTargetsSameCard()
        {
            SetupGameController(
                new string[] { "BaronBlade", "TheScholar", "TheCelestialTribunal" }
            );

            StartGame();

            RemoveMobileDefensePlatform();

            MoveAllCards(baron, FindEnvironment().TurnTaker.Deck, FindEnvironment().TurnTaker.OutOfGame);

            var executioner = PlayCard("CelestialExecutioner");
            executioner.SetHitPoints(1);

            var battalion = PlayCard("BladeBattalion");
            battalion.SetHitPoints(1);

            DecisionSelectCard = battalion;
            PlayCard("FoundWanting");

            scholar.CharacterCard.SetHitPoints(10);
            QuickHPStorage(scholar);

            DecisionSelectTargets = new Card[] { baron.CharacterCard, executioner, battalion, executioner };

            PlayCard("GetOutOfTheWay");

            QuickHPCheck(4);
        }
    }
}