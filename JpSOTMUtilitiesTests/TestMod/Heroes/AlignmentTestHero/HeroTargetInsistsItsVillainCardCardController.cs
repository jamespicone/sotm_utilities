﻿
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities.TestMod.AlignmentTestHero
{
    public class HeroTargetInsistsItsVillainCardCardController : CardController
    {
        public HeroTargetInsistsItsVillainCardCardController(Card card, TurnTakerController controller) : base(card, controller)
        {
            AddThisCardControllerToList(CardControllerListType.ModifiesDeckKind);
        }

        public override bool? AskIfIsVillain(Card card, CardSource cardSource)
        {
            if (card == Card) { return true; }

            return null;
        }

        public override bool? AskIfIsHero(Card card, CardSource cardSource)
        {
            if (card == Card) { return false; }

            return null;
        }
    }
}
