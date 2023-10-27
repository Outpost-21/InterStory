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
    public class ChatOptionWorker_UnderAttack : ChatOptionWorker
    {
        public override bool Requirements()
        {
            if(Find.Maps.Any(m => m.IsPlayerHome && GenHostility.AnyHostileActiveThreatToPlayer(m, true)))
            {
                return true;
            }
            return false;
        }
    }
}
