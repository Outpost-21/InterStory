﻿using RimWorld;
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

                    lastChatOption = options.RandomElement();
                }
                else if (!def.helloKey.NullOrEmpty())
                {
                    IEnumerable<ChatOption> options = from o in def.chatOptions
                                                      where o.chatKey == def.helloKey && o.CanShow && o.Worker.Requirements()
                                                      select o;

                    lastChatOption = options.RandomElement();
                }
            }
            if(nextChatOption != null)
            {
                if (nextChatOption.returnToRoot)
                {
                    lastChatOption = null;
                    nextChatOption = null;
                    return;
                }
                lastChatOption = nextChatOption;
                nextChatOption.Worker.DoOutput();
                nextChatOption = null;
            }
        }

        public virtual void DoChat(Rect inRect, Action action)
        {
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(inRect);
            listing.Label(def.LabelCap);
            listing.GapLine();
            InitChat();
            DoChatListing(listing);
            DoButtonListing(listing);
            DoCloseButton(listing, action);
            listing.End();
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
                    if (lastChatOption == null)
                    {
                        if (!def.rootKeys.NullOrEmpty() && def.rootKeys.Contains(chat.chatKey))
                        {
                            listing.DoChatButton(chat, delegate { nextChatOption = chat; });
                        }
                    }
                    else if (!lastChatOption.acceptedKeys.NullOrEmpty() && lastChatOption.acceptedKeys.Contains(chat.chatKey))
                    {
                        listing.DoChatButton(chat, delegate { nextChatOption = chat; });
                    }
                }
            }
        }

        public virtual void DoCloseButton(Listing_Standard listing, Action action)
        {
            if ((lastChatOption == null || lastChatOption.allowCloseChat) && listing.ButtonText("[Close Communication]"))
            {
                lastChatOption = null;
                action.Invoke();
            }
        }
    }
}
