using Entitas;
using UnityEngine;
using System.Collections.Generic;
using Entitas.Unity;
using System;

namespace Bartok
{
    public sealed class CreateGameBoardSystem : IInitializeSystem
    {
        GameContext game;
        Transform layoutAnchor;

        public CreateGameBoardSystem(Contexts contexts)
        {
            this.game = contexts.game;
        }

        public void Initialize()
        {
            game.SetGameData(null, new List<GameEntity>(), new List<GameEntity>(), new List<GameEntity>());
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
            var cards = this.game.cardCache.value;
            cards.Shuffle();
            int index = 0;
            for (int i = 0; i < gameBoard.slotDefs.Count; i++)
            {
                var tSD = gameBoard.slotDefs[i];
                var e = cards[index];
                e.AddCardState(CardState.tableau);
                e.AddCardProspector(tSD.id, tSD);
                e.AddFaceUp(tSD.faceUp);
                e.AddPosition(new Vector3(
                    gameBoard.multiplier.x * tSD.x,
                    gameBoard.multiplier.y * tSD.y,
                    -tSD.layerID
                ));
                e.ReplaceSortOrder(0);
                game.gameData.tableau.Add(e);
                index++;
            }

            MoveToTarget(cards[index], gameBoard);
            index++;

            var j = 0;
            for (; index < cards.Count; index++)
            {
                game.gameData.drawPile.Add(cards[index]);
                UpdateDrawPile(cards[index], j, gameBoard);
                j++;
            }
        }

        // 移动当前牌到弃牌堆
        void MoveToDiscard(GameEntity e, GameBoardComponent gameBoard) {
            e.ReplaceCardProspector(gameBoard.discardPile.id, gameBoard.discardPile);
            e.ReplaceCardState(CardState.discard);
            e.ReplaceFaceUp(false);
            e.ReplacePosition(new Vector3(
                gameBoard.discardPile.x * gameBoard.multiplier.x,
                gameBoard.discardPile.y * gameBoard.multiplier.y,
                -gameBoard.discardPile.layerID + 0.5f
            ));
            e.ReplaceSortOrder(-100 + game.gameData.discardPile.Count);
            game.gameData.discardPile.Add(e);
        }

        // 使 e 成为新的目标牌
        void MoveToTarget(GameEntity e, GameBoardComponent gameBoard) {
            if (game.gameData.target != null)
            {
                MoveToDiscard(game.gameData.target, gameBoard);
            }
            game.gameData.target = e;
            e.ReplaceCardProspector(gameBoard.discardPile.id, gameBoard.discardPile);
            e.ReplaceCardState(CardState.target);
            e.ReplaceFaceUp(false);
            e.ReplacePosition(new Vector3(
                gameBoard.discardPile.x * gameBoard.multiplier.x,
                gameBoard.discardPile.y * gameBoard.multiplier.y,
                -gameBoard.discardPile.layerID
            ));
            e.ReplaceSortOrder(0);
        }

        void UpdateDrawPile(GameEntity e, int i, GameBoardComponent gameBoard) {
            e.ReplaceCardProspector(gameBoard.drawPile.id, gameBoard.drawPile);
            e.ReplaceFaceUp(true);
            e.ReplacePosition(new Vector3(
                (gameBoard.drawPile.x + i * gameBoard.drawPile.stagger.x) * gameBoard.multiplier.x,
                (gameBoard.drawPile.y + i * gameBoard.drawPile.stagger.y) * gameBoard.multiplier.y,
                -gameBoard.discardPile.layerID + 0.1f * i
            ));
            e.ReplaceSortOrder(-10 * i);
        }
    }
}