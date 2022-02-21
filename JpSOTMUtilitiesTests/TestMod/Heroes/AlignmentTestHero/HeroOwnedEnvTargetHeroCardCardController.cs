﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities.TestMod.AlignmentTestHero
{
    public class HeroOwnedEnvTargetHeroCardCardController : CardController
    {
        public HeroOwnedEnvTargetHeroCardCardController(Card card, TurnTakerController controller) : base(card, controller)
        { }
    }
}