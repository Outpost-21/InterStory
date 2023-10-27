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
    public class ChatOption
    {
        public string chatKey;

        public string buttonLabel;

        public string buttonText;

        public string inputText;

        public string outputText;

        public List<string> acceptedKeys;

        public bool allowCloseChat = true;

        public bool needsFaction = false;

        public bool onlyOnce = false;

        public bool returnToRoot = false;

        public bool devOnly = false;

        public InterPawnDef pawnDef;

        public int favorCost = 0;

        public int minGoodwill = -999;

        public int maxGoodwill = 999;

        public int goodwillChange = 0;

        public Type outputWorker = typeof(ChatOptionWorker);

        public ChatOptionWorker workerInt;

        public ChatOptionWorker Worker
        {
            get
            {
                if (workerInt == null)
                {
                    workerInt = (ChatOptionWorker)Activator.CreateInstance(outputWorker);
                    workerInt.option = this;
                }
                return workerInt;
            }
        }

        public bool CanShow
        {
            get
            {
                if (onlyOnce && InterStoryUtil.CheckChatKey(chatKey))
                {
                    return false;
                }
                if (InterStoryUtil.GetFavorWith(pawnDef) < favorCost)
                {
                    return false;
                }
                if (pawnDef.Faction != null)
                {
                    if (pawnDef.Faction.GoodwillWith(Faction.OfPlayer) < minGoodwill)
                    {
                        return false;
                    }
                    if (pawnDef.Faction.GoodwillWith(Faction.OfPlayer) > maxGoodwill)
                    {
                        return false;
                    }
                }
                if (needsFaction && pawnDef.Faction == null)
                {
                    return false;
                }
                if (devOnly && !DebugSettings.godMode)
                {
                    return false;
                }
                return Worker.Requirements();
            }
        }
    }
}
