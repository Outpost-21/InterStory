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
    public class ChatWorker
    {
        public InterPawnDef def;

        public ChatOption lastChatOption;
        public ChatOption nextChatOption;
        public float chatHeight;
        public float responseHeight;

        public bool saidHello = false;

        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;

        public ChatWorker() { }

        public void InitChat()
        {
            if (lastChatOption == null)
            {
                if (!InterStoryUtil.HasChattedBefore(def) && !def.introKey.NullOrEmpty())
                {
                    IEnumerable<ChatOption> options = from o in def.chatOptions
                                                      where o.chatKey == def.introKey
                                                      select o;

                    nextChatOption = options.RandomElement();
                }
                else if (!saidHello && !def.helloKey.NullOrEmpty())
                {
                    IEnumerable<ChatOption> options = from o in def.chatOptions
                                                      where o.chatKey == def.helloKey && o.CanShow && o.Worker.Requirements()
                                                      select o;

                    nextChatOption = options.RandomElement();
                    saidHello = true;
                }
            }
            if(nextChatOption != null)
            {
                nextChatOption.Worker.DoOutput();
                //if (nextChatOption.returnToRoot) { lastChatOption = null; }
                //else  { lastChatOption = nextChatOption; }
                lastChatOption = nextChatOption;
                nextChatOption = null;
            }
        }

        public virtual void DoChat(Rect inRect, Action action)
        {
            bool flag = optionsViewRectHeight > inRect.height;
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width - (flag ? 26f : 0f), optionsViewRectHeight);
            Widgets.BeginScrollView(inRect, ref optionsScrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            Rect rect = new Rect(viewRect.x, viewRect.y, viewRect.width, 999999f);
            listing.Begin(rect);
            // ============================ CONTENTS ================================
            listing.Label(def.LabelCap);
            listing.GapLine();
            InitChat();
            DoChatListing(listing);
            DoButtonListing(listing);
            DoCloseButton(listing, action);
            // ======================================================================
            optionsViewRectHeight = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
        }

        public virtual void DoChatListing(Listing_Standard listing)
        {
            if (lastChatOption != null)
            {
                if (lastChatOption.inputText != null)
                {
                    Listing_Standard chat = listing.BeginSection(chatHeight);
                    chat.Label(lastChatOption.inputText);
                    chatHeight = chat.CurHeight;
                    listing.EndSection(chat);
                    listing.Gap();
                }
                if (lastChatOption.outputText != null)
                {
                    Listing_Standard response = listing.BeginSection(responseHeight);
                    response.Label(lastChatOption.outputText);
                    responseHeight = response.CurHeight;
                    listing.EndSection(response);
                    listing.Gap();
                }
            }
        }

        public virtual void DoButtonListing(Listing_Standard listing)
        {
            foreach (ChatOption chat in def.chatOptions)
            {
                if (chat.CanShow)
                {
                    if (lastChatOption == null || lastChatOption.returnToRoot)
                    {
                        if (!def.rootKeys.NullOrEmpty() && def.rootKeys.Contains(chat.chatKey))
                        {
                            listing.DoChatButton(chat, delegate { nextChatOption = chat; });
                            continue;
                        }
                    }
                    else if (!lastChatOption.acceptedKeys.NullOrEmpty() && lastChatOption.acceptedKeys.Contains(chat.chatKey))
                    {
                        listing.DoChatButton(chat, delegate { nextChatOption = chat; });
                        continue;
                    }
                }
            }
        }

        public virtual void DoCloseButton(Listing_Standard listing, Action action)
        {
            if ((lastChatOption == null || lastChatOption.allowCloseChat) && listing.ButtonText("[Close Communication]"))
            {
                lastChatOption = null;
                saidHello = false;
                action.Invoke();
            }
        }
    }
}
