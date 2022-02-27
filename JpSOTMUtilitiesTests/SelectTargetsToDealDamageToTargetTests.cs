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
    public class SelectTargetsToDealDamageToTargetTests : BaseTest
    {
        [Test()]
        public void TestRegularUsage()
        {
            SetupGameController("BaronBlade", "Legacy", "Tempest", "Megalopolis");

            StartGame();

            DecisionSelectCards = new Card[]
            {
                legacy.CharacterCard,
                GetMobileDefensePlatform().Card,
                baron.CharacterCard
            };

            QuickHPStorage(new Card[] {
                baron.CharacterCard,
                legacy.CharacterCard,
                tempest.CharacterCard,
                GetMobileDefensePlatform().Card
            });

            RunCoroutine(
                legacy.CharacterCardController.SelectTargetsToDealDamageToTarget(
                    legacy,
                    c => c.Is().Hero().Target(),
                    c => c.Is().Villain().Target().AccordingTo(legacy.CharacterCardController),
                    2,
                    DamageType.Infernal
                )
            );

            QuickHPCheck(0, 0, 0, -2);
        }
    }
}
