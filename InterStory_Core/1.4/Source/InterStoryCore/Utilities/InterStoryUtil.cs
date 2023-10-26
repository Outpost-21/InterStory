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
    }
}
