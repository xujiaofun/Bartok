using Entitas;
using UnityEngine;
using System.Collections.Generic;
using Entitas.Unity;

namespace Bartok
{
    public sealed class CreateGameBoardSystem : IInitializeSystem
    {
        GameContext game;

        public CreateGameBoardSystem(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            var sortingLayerNames = new string[] { "Row0", "Row1", "Row2", "Row3", "Discard", "Draw" };
            var txt = Resources.Load<TextAsset>("LayoutXML");  // 此处不需要后缀


            var xmlr = new PT_XMLReader();
            xmlr.Parse(txt.text);
            var xml = xmlr.xml["xml"][0];

            Debug.Log(xml["multiplier"][0].att("x"));

            // 用于设置纸牌间隙的系数
            Vector2 multiplier;
            multiplier.x = float.Parse(xml["multiplier"][0].att("x"));
            multiplier.y = float.Parse(xml["multiplier"][0].att("y"));

            SlotDef drawPile = null;
            SlotDef discardPile = null;
            var slotDefs = new List<SlotDef>();
            var slotsX = xml["slot"];
            for (int i = 0; i < slotsX.Count; i++)
            {
                var tSD = new SlotDef();
                if (slotsX[i].HasAtt("type"))
                {
                    tSD.type = slotsX[i].att("type");
                }
                else
                {
                    tSD.type = "slot";
                }

                tSD.x = float.Parse(slotsX[i].att("x"));
                tSD.y = float.Parse(slotsX[i].att("y"));
                tSD.layerID = int.Parse(slotsX[i].att("layer"));
                tSD.layerName = sortingLayerNames[tSD.layerID];

                if (tSD.type == "slot")
                {
                    tSD.id = int.Parse(slotsX[i].att("id"));
                    tSD.faceUp = slotsX[i].att("faceup") == "1";

                    if (slotsX[i].HasAtt("hiddenby"))
                    {
                        var hiding = slotsX[i].att("hiddenby").Split(',');
                        foreach (var s in hiding)
                        {
                            tSD.hiddenBy.Add(int.Parse(s));
                        }
                    }
                    slotDefs.Add(tSD);
                }
                else if (tSD.type == "drawpile")
                {
                    tSD.stagger.x = float.Parse(slotsX[i].att("xstagger"));
                    drawPile = tSD;
                }
                else if (tSD.type == "discardpile")
                {
                    discardPile = tSD;
                }
            }

            this.game.SetGameBoard(multiplier, slotDefs, drawPile, discardPile);
            this.SetupGameBoard(this.game.gameBoard);
        }

        private void SetupGameBoard(GameBoardComponent gameBoard)
        {
            var tGO = GameObject.Find("_LayoutAnchor");
            if (tGO == null)
            {
                tGO = new GameObject("_LayoutAnchor");
                tGO.transform.position = Vector3.zero;
            }
            var layoutAnchor = tGO.transform;

            var cards = this.game.cardCache.value;
            cards.Shuffle();

            for (int i = 0; i < gameBoard.slotDefs.Count; i++)
            {
                var tSD = gameBoard.slotDefs[i];
                var e = cards[i];
                e.AddCardState(CardState.tableau);
                e.AddCardProspector(tSD.id, tSD);
                e.AddFaceUp(tSD.faceUp);

                var cGO = e.gameObject.value;
                cGO.transform.parent = layoutAnchor;
                cGO.transform.localPosition = new Vector3(
                    gameBoard.multiplier.x * tSD.x,
                    gameBoard.multiplier.y * tSD.y,
                    -tSD.layerID
                );

                cGO.Link(e, this.game);
                cGO.AddComponent<CardViewBehaviour>();

                var spriteRenders = cGO.GetComponentsInChildren<SpriteRenderer>();
                foreach (var tSR in spriteRenders)
                {
                    tSR.sortingLayerName = tSD.layerName;
                }

            }

        }
    }
}