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
    public class ChatOptionWorker_IntroComplete : ChatOptionWorker
    {
        public override void DoOutput()
        {
            base.DoOutput();
            WorldComp_InterStory comp = InterStoryUtil.GetWorldComp;
            if (comp.firstChatDone.ContainsKey(option.pawnDef.defName))
            {
                comp.firstChatDone[option.pawnDef.defName] = true;
            }
            else
            {
                comp.firstChatDone.Add(option.pawnDef.defName, true);
            }
        }
    }
}
