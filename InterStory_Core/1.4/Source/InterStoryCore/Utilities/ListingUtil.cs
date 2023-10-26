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
    public static class ListingUtil
    {
        public static void DoChatButton(this Listing_Standard listing, ChatOption chat, Action action)
        {
            if (listing.ButtonText(chat.inputText))
            {
                action.Invoke();
            }
        }
    }
}
