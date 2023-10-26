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
    public static class InteractionUtil
    {
        public static bool HasChattedBefore(InterPawnDef pawn)
        {
            WorldComp_InterStory comp = InterStoryUtil.GetWorldComp;
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

        public static Color GetColorForAttitude(InterPawnDef pawn)
        {
            PawnAttitude attitude = pawn.Attitude;
            switch (attitude)
            {
                case PawnAttitude.Furious:
                    return Color.red;
                case PawnAttitude.Angry:
                    return Color.red;
                case PawnAttitude.Irritated:
                    return Color.yellow;
                case PawnAttitude.Dismissive:
                    return Color.yellow;
                case PawnAttitude.Neutral:
                    return Color.white;
                case PawnAttitude.Intrigued:
                    return Color.blue;
                case PawnAttitude.Upbeat:
                    return Color.blue;
                case PawnAttitude.Happy:
                    return Color.cyan;
                case PawnAttitude.Delighted:
                    return Color.cyan;
                default:
                    break;
            }
            return Color.white;
        }
    }
}
