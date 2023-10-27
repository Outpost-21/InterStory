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
    public static class InterStoryUtil
    {
        public static WorldComp_InterStory GetWorldComp => Find.World.GetComponent<WorldComp_InterStory>();

        public static void UseChatKey(string key)
        {
            WorldComp_InterStory comp = GetWorldComp;
            if (comp.chatKeysUsed.ContainsKey(key))
            {
                comp.chatKeysUsed[key] = true;
            }
            else
            {
                comp.chatKeysUsed.Add(key, true);
            }
        }

        public static bool CheckChatKey(string key)
        {
            WorldComp_InterStory comp = GetWorldComp;
            if (!comp.chatKeysUsed.ContainsKey(key))
            {
                comp.chatKeysUsed.Add(key, false);
            }
            return comp.chatKeysUsed[key];
        }

        public static void ChangeInfluenceWith(InterPawnDef pawn, int value)
        {
            WorldComp_InterStory comp = GetWorldComp;
            if (comp.influenceEarned.ContainsKey(pawn.defName))
            {
                comp.influenceEarned[pawn.defName] += value;
            }
            else
            {
                comp.influenceEarned.Add(pawn.defName, value);
            }
        }

        public static int GetInfluenceWith(InterPawnDef pawn)
        {
            WorldComp_InterStory comp = GetWorldComp;
            if (!comp.influenceEarned.ContainsKey(pawn.defName))
            {
                comp.influenceEarned.Add(pawn.defName, 0);
            }
            return comp.influenceEarned[pawn.defName];
        }

        public static bool HasChattedBefore(InterPawnDef pawn)
        {
            WorldComp_InterStory comp = GetWorldComp;
            if (comp.firstChatDone.ContainsKey(pawn.defName))
            {
                if (comp.firstChatDone[pawn.defName])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsContactable(InterPawnDef pawn)
        {
            return true;
        }
    }
}
