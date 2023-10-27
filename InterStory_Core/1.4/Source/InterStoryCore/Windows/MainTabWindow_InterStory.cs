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

        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;

        public override void PostOpen()
        {
            base.PostOpen();
            Find.TickManager.Pause();
        }

        public override void PostClose()
        {
            base.PostClose();
            Find.TickManager.CurTimeSpeed = TimeSpeed.Normal;
        }

        public override Vector2 InitialSize => new Vector2(Prefs.ScreenWidth, 380f);

        public override void Close(bool doCloseSound = true)
        {
            selectedPawn = null;
            closeChat = false;
            base.Close(doCloseSound);
        }

        public override void DoWindowContents(Rect inRect)
        {
            if (closeChat)
            {
                selectedPawn = null;
                closeChat = false;
                return;
            }
            float chatWidth = 720f;
            if ((inRect.width - portraitEdgeLength) < chatWidth)
            {
                chatWidth = inRect.width - portraitEdgeLength;
            }
            float margin = 0f;
            if (inRect.width > (portraitEdgeLength + chatWidth))
            {
                margin = (inRect.width - (portraitEdgeLength + chatWidth)) / 2f;
            }
            if (selectedPawn == null)
            {
                Rect selectionRect = new Rect(inRect.x + margin, inRect.y, chatWidth + portraitEdgeLength, portraitEdgeLength);
                DoPawnSelection(selectionRect);
            }
            else
            {
                Rect chatRect = new Rect(inRect.x + margin, inRect.y, chatWidth, inRect.height);
                DoChatWorker(chatRect);
                Rect portraitRect = new Rect(inRect.width - (portraitEdgeLength + margin), inRect.y, portraitEdgeLength, portraitEdgeLength);
                DrawPortrait(portraitRect);
            }
        }

        public void DoChatWorker(Rect inRect)
        {
            Rect rect = inRect.ContractedBy(8f);
            selectedPawn.Worker.DoChat(rect, delegate { closeChat = true; });
        }

        public void DoPawnSelection(Rect inRect)
        {
            Rect outRect = inRect.ContractedBy(8f);
            bool flag = optionsViewRectHeight > outRect.height;
            Rect viewRect = new Rect(outRect.x, outRect.y, outRect.width - (flag ? 26f : 0f), optionsViewRectHeight);
            Widgets.BeginScrollView(outRect, ref optionsScrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            //listing.ColumnWidth *= 0.3f;
            Rect rect = new Rect(viewRect.x, viewRect.y, viewRect.width, 999999f);
            listing.Begin(rect);
            // ============================ CONTENTS ================================
            listing.Label("Select Contact");
            listing.GapLine();
            foreach (InterPawnDef pawn in DefDatabase<InterPawnDef>.AllDefs)
            {
                if (InterStoryUtil.IsContactable(pawn))
                {
                    string text = "Unaligned";
                    if(pawn.Faction != null)
                    {
                        text = pawn.Faction.NameColored;
                    }
                    if (listing.ButtonTextLabeled(text, pawn.LabelCap))
                    {
                        selectedPawn = pawn;
                        return;
                    }
                }
            }
            // ======================================================================
            optionsViewRectHeight = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
        }

        public void DrawPortrait(Rect inRect)
        {
            Rect drawRect = new Rect(inRect.x + 16f, inRect.y + 16f, inRect.width - 32f, inRect.height - 32f);
            Widgets.DrawTextureFitted(drawRect, MaterialPool.MatFrom(selectedPawn.pawnImagePath).mainTexture, 1f);
            Rect listingRect = new Rect(inRect.x + 16f, inRect.y + drawRect.height + 24f, inRect.width - 32f, 32f);
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(listingRect);
            string factionInfo = "";
            if (selectedPawn.Faction != null)
            {
                factionInfo = "Goodwill: " + selectedPawn.Faction.GoodwillWith(Faction.OfPlayer);
            }
            listing.LabelDouble("Influence: " + InterStoryUtil.GetInfluenceWith(selectedPawn), factionInfo);
            listing.End();
        }
    }
}
