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
    public class MainButtonWorker_Contact : MainButtonWorker_ToggleTab
    {
        public override bool Disabled
        { 
            get 
            {
                if (base.Disabled) { return true; }
                if (DebugSettings.godMode) { return false; }
                if (DefDatabase<InterPawnDef>.AllDefs.EnumerableNullOrEmpty()) { return true; }
                Map map = Find.CurrentMap;
                if (map == null) { return true; }
                if (map.listerBuildings.allBuildingsColonist.Any(b => b.def.IsCommsConsole && (b.GetComp<CompPowerTrader>() == null || b.GetComp<CompPowerTrader>().PowerOn)))
                {
                    return false;
                }
                return true; 
            } 
        }

        public override bool Visible => !Disabled;
    }
}
