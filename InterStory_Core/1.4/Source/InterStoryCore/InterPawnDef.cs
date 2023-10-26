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

        public string introKey;

        public string helloKey;

        public List<ChatOption> chatOptions;

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

        public PawnAttitude Attitude = PawnAttitude.Neutral;
    }
}
