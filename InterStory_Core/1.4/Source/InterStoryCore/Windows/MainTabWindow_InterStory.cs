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
    public class MainTabWindow_InterStory : MainTabWindow
    {
        public float portraitEdgeLength = 280f;

        public InterPawnDef selectedPawn;

        public bool closeChat = false;

        public override void DoWindowContents(Rect inRect)
        {
            if (closeChat)
            {
                selectedPawn = null;
                closeChat = false;
                return;
            }
            if(selectedPawn == null)
            {
                Rect selectionRect = new Rect(inRect.x, inRect.y, Mathf.Min(inRect.width - inRect.height, 720f), inRect.height);
                DoPawnSelection(selectionRect);
            }
            else
            {
                Rect chatRect = new Rect(inRect.x, inRect.y, Mathf.Min(inRect.width - portraitEdgeLength, 720f), inRect.height);
                DoChatWorker(chatRect);
                Rect portraitRect = new Rect(inRect.width - portraitEdgeLength, inRect.y, portraitEdgeLength, portraitEdgeLength);
                DrawPortrait(portraitRect);
            }
        }

        public void DoChatWorker(Rect inRect)
        {
            Rect rect = inRect.ContractedBy(32f);
            selectedPawn.Worker.DoChat(rect, delegate { closeChat = true; });
        }

        public void DoPawnSelection(Rect inRect)
        {
            Rect rect = inRect.ContractedBy(32f);
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(rect);
            listing.Label("Select Contact");
            listing.GapLine();
            foreach(InterPawnDef pawn in DefDatabase<InterPawnDef>.AllDefs)
            {
                if (InterStoryUtil.IsContactable(pawn))
                {
                    if (listing.ButtonText(pawn.LabelCap))
                    {
                        selectedPawn = pawn;
                        return;
                    }
                }
            }
            listing.End();
        }

        public void DrawPortrait(Rect inRect)
        {
            Rect drawRect = new Rect(inRect.x + 16f, inRect.y + 16f, inRect.width - 32f, inRect.height - 32f);
            Widgets.DrawTextureFitted(drawRect, MaterialPool.MatFrom(selectedPawn.pawnImagePath).mainTexture, 1f);
            Rect listingRect = new Rect(inRect.x + 16f, inRect.y + drawRect.height + 24f, inRect.width - 32f, 32f);
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(listingRect);
            listing.LabelDouble("Favor: ", InterStoryUtil.GetFavorWith(selectedPawn).ToString());
            if(selectedPawn.Faction != null)
            {
                listing.LabelDouble("Faction Goodwill:", selectedPawn.Faction.GoodwillWith(Faction.OfPlayer).ToString());
            }
            listing.End();
        }
    }
}
