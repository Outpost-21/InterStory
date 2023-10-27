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
    public class ChatOptionWorker_SendAid : ChatOptionWorker
    {
        public override void DoOutput()
        {
            base.DoOutput();
            IncidentParms incidentParms = new IncidentParms();
            incidentParms.target = Find.CurrentMap;
            incidentParms.faction = option.pawnDef.Faction;
            incidentParms.raidArrivalModeForQuickMilitaryAid = true;
            incidentParms.points = DiplomacyTuning.RequestedMilitaryAidPointsRange.RandomInRange;
            option.pawnDef.Faction.lastMilitaryAidRequestTick = Find.TickManager.TicksGame;
            IncidentDefOf.RaidFriendly.Worker.TryExecute(incidentParms);
        }
    }
}
