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
    public class ChatOptionWorker
    {
        public ChatOption option;

        public ChatOptionWorker() { }

        public virtual void DoOutput()
        {
            if (option.onlyOnce)
            {
                InterStoryUtil.UseChatKey(option.chatKey);
            }
            InterStoryUtil.ChangeInfluenceWith(option.pawnDef, -option.influenceCost);
        }

        public virtual bool Requirements()
        {
            return true;
        }
    }
}
