using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace InterStoryCore
{
    public class WorldComp_InterStory : WorldComponent
    {
        public Dictionary<string, bool> firstChatDone = new Dictionary<string, bool>();

        public Dictionary<string, int> influenceEarned = new Dictionary<string, int>();

        public Dictionary<string, bool> chatKeysUsed = new Dictionary<string, bool>();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref firstChatDone, "firstChatDone");
            Scribe_Collections.Look(ref influenceEarned, "influenceEarned");
            Scribe_Collections.Look(ref chatKeysUsed, "chatKeysUsed");
        }

        public WorldComp_InterStory(World world) : base(world)
        {
            if (firstChatDone.NullOrEmpty())
            {
                firstChatDone = new Dictionary<string, bool>();
            }
        }
    }
}
