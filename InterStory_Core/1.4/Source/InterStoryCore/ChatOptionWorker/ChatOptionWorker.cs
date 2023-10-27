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
            InterStoryUtil.GainFavorWith(option.pawnDef, -option.favorCost);
        }

        public virtual bool Requirements()
        {
            return true;
        }
    }
}
