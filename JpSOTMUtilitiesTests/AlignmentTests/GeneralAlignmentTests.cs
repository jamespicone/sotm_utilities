using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Handelabra.Sentinels.Engine.Model;
using Handelabra.Sentinels.UnitTest;
using Handelabra.Sentinels.Engine.Controller;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class GeneralAlignmentTests : BaseTest
    {
        [Test()]
        public void TestNulls()
        {
            SetupGameController("BaronBlade", "Legacy", "Jp.SOTMUtilities.TestMod.AlignmentTestEnvironment");

            Card card = null;
            TurnTaker turntaker = null;
            CardController controller = null;

            Card realCard = baron.CharacterCard;
            TurnTaker realTurntaker = baron.TurnTaker;

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)card.Is().Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)turntaker.Is().Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)controller.Is().Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)realCard.Is(controller).Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)realCard.Is().Environment().AccordingTo(controller); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)realTurntaker.Is(controller).Environment(); }
            );

            Assert.Throws<NullReferenceException>(
                () => { bool b = (bool)realTurntaker.Is().Environment().AccordingTo(controller); }
            );
        }
    }
}
