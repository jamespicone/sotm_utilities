using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities.TestMod.PowerlessTestHero
{
    public class PowerlessTestHeroFillerCardController : CardController
    {
        public PowerlessTestHeroFillerCardController(Card card, TurnTakerController controller) : base(card, controller)
        { }
    }
}
