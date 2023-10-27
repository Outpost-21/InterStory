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
            string buttonText = chat.buttonText.NullOrEmpty() ? chat.inputText : chat.buttonText;
            if (!chat.buttonLabel.NullOrEmpty())
            {
                if(listing.ButtonTextLabeledPct(chat.buttonLabel, buttonText, 0.2f, TextAnchor.UpperLeft, null, chat.inputText))
                {
                    action.Invoke();
                }
            }
            else
            {
                if (listing.ButtonText(buttonText))
                {
                    action.Invoke();
                }
            }
        }
    }
}
