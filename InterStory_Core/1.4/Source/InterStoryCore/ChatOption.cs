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

        public string initialText;

        public string inputText;

        public string outputText;

        public List<string> acceptedKeys;

        public bool responseOnly = false;

        public bool allowCloseChat = true;

        public InterPawnDef pawnDef;

        public Type outputWorker = typeof(ChatOptionWorker);

        public ChatOptionWorker workerInt;

        public ChatOptionWorker Worker
        {
            get
            {
                if (workerInt == null)
                {
                    workerInt = (ChatOptionWorker)Activator.CreateInstance(outputWorker);
                    workerInt.pawn = pawnDef; 
                }
                return workerInt;
            }
        }
    }
}
