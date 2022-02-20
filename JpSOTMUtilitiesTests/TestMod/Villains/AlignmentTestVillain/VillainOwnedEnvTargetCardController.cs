using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Handelabra.Sentinels.Engine.Controller;
using Handelabra.Sentinels.Engine.Model;

namespace Jp.SOTMUtilities.TestMod.AlignmentTestVillain
{
    public class VillainOwnedEnvTargetCardController : CardController
    {
        public VillainOwnedEnvTargetCardController(Card card, TurnTakerController controller) : base(card, controller)
        { }
    }
}
