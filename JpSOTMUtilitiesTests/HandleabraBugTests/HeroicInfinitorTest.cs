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
    public class HeroicInfinitorTest : BaseTest
    {
        [Test()]
        public void TestLeadAndMaze()
        {
            SetupGameController(
                new string[] { "Infinitor", "Legacy", "MMFFCC" },
                promoIdentifiers: new Dictionary<string, string> { { "Infinitor", "HeroicInfinitorCharacter" } }
            );

            StartGame();

            PlayCard("MazeOfMirrors");

            // Use up the MoM redirect
            DealDamage(infinitor, legacy, 1, DamageType.Infernal);

            PlayCard("LeadFromTheFront");

            // "Whenever a hero target would be dealt damage by a ~~villain~~ hero card, you may redirect that damage to legacy".
            // Infinitor is a hero target and a villain card, so should NOT get redirect

            DecisionYesNo = true;
            QuickHPStorage(infinitor, legacy);
            DealDamage(infinitor, infinitor, 2, DamageType.Radiant);

            // Infinitor does +1 damage
            QuickHPCheck(-3, 0);
        }

        [Test()]
        public void TestMazeAndDamage()
        {
            SetupGameController(
                new string[] { "Infinitor", "Luminary", "MMFFCC" },
                promoIdentifiers: new Dictionary<string, string> { { "Infinitor", "HeroicInfinitorCharacter" } }
            );

            StartGame();

            PlayCard("MazeOfMirrors");

            // Use up the MoM redirect
            DealDamage(luminary, luminary, 1, DamageType.Infernal);

            DecisionYesNo = false;
            QuickHPStorage(infinitor, luminary);

            // "{Luminary} deals each ~~villain~~ hero target 1 lightning damage."
            // Should hit infinitor
            PlayCard("HastenVictory");

            QuickHPCheck(-1, -1);
        }

        [Test()]
        public void TestLead()
        {
            SetupGameController(
                new string[] { "Infinitor", "Legacy", "MMFFCC" },
                promoIdentifiers: new Dictionary<string, string> { { "Infinitor", "HeroicInfinitorCharacter" } }
            );

            StartGame();

            PlayCard("LeadFromTheFront");

            // "Whenever a hero target would be dealt damage by a villain card, you may redirect that damage to legacy".
            // Infinitor is a hero target and a villain card, so should get redirect.

            DecisionYesNo = true;
            QuickHPStorage(infinitor, legacy);
            DealDamage(infinitor, infinitor, 2, DamageType.Radiant);

            // Infinitor does +1 damage
            QuickHPCheck(0, -3);
        }
    }
}