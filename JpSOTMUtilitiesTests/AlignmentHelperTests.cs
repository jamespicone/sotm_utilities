using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities.UnitTest
{
    [TestFixture()]
    public class AlignmentHelperTests
    {
        [Test()]
        public void TestOne()
        {
            // TODO: This is going to be awkward to test; making entirely synthetic cards at runtime looks unpleasant.
            // Probably going to be easiest to have a 'test mod' that has useful cards.
            //
            //var game = new Game(new string[] { "TestTurnTaker" });
            //var turntaker = new TurnTaker(game, "TestTurnTaker");
            //var deckDefinition = new DeckDefinition();
            //var definition = new CardDefinition(json, deckDefinition, "Test");
            //var card = new Card(definition, turntaker, 0);
            Assert.AreEqual(CardAlignment.Hero, CardAlignment.Hero);
        }
    }
}
