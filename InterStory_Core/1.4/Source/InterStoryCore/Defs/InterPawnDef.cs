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
    public class InterPawnDef : Def
    {
        public string pawnImagePath;

        public FactionDef faction;

        public Faction Faction
        {
            get
            {
                if (faction != null)
                {
                    return Find.FactionManager.FirstFactionOfDef(faction) ?? null;
                }
                return null;
            }
        }

        public string introKey;

        public string helloKey;

        public List<string> rootKeys = new List<string>();

        public List<ChatOption> chatOptions = new List<ChatOption>();

        public Type chatWorker = typeof(ChatWorker);

        public ChatWorker workerInt;

        public ChatWorker Worker
        {
            get
            {
                if (workerInt == null)
                {
                    workerInt = (ChatWorker)Activator.CreateInstance(chatWorker);
                    workerInt.def = this;
                }
                return workerInt;
            }
        }
    }
}
