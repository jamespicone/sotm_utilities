﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities.TestMod.NameOfDeck
{
    public class TemplateCardController : CardController
    {
        public TemplateCardController(Card card, TurnTakerController controller) : base(card, controller)
        { }
    }
}
