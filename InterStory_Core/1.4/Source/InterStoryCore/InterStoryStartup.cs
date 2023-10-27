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
    [StaticConstructorOnStartup]
    public static class InterStoryStartup
    {
        static InterStoryStartup()
        {
            PassAlongPawnToChats();
        }

        public static void PassAlongPawnToChats()
        {
            foreach(InterPawnDef pawn in DefDatabase<InterPawnDef>.AllDefs)
            {
                for (int i = 0; i < pawn.chatOptions.Count; i++)
                {
                    ChatOption option = pawn.chatOptions[i];
                    option.pawnDef = pawn;
                }
            }
        }
    }
}
