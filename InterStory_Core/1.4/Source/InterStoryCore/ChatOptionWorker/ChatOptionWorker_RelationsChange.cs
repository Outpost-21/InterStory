using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace InterStoryCore
{
    public class ChatOptionWorker_RelationsChange : ChatOptionWorker
    {
        public override void DoOutput()
        {
            base.DoOutput();
            option.pawnDef.Faction.TryAffectGoodwillWith(Faction.OfPlayer, option.goodwillChange);
        }
    }
}
