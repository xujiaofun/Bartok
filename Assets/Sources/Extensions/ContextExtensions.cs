using System;
using UnityEngine;

namespace Bartok
{
    public static class ContextExtensions
    {
        /// <summary>
        /// 使 e 成为新的目标牌
        /// </summary>
        /// <param name="game">Game.</param>
        /// <param name="e">E.</param>
        public static void MoveToTarget(this GameContext game, GameEntity e) {
            GameBoardComponent gameBoard = game.gameBoard;
            if (game.gameData.target != null)
            {
                game.MoveToDiscard(game.gameData.target);
            }
            game.gameData.target = e;
            e.ReplaceCardProspector(gameBoard.discardPile.id, gameBoard.discardPile);
            e.ReplaceCardState(CardState.target);
            e.ReplaceFaceUp(true);
            e.ReplacePosition(new Vector3(
                gameBoard.discardPile.x * gameBoard.multiplier.x,
                gameBoard.discardPile.y * gameBoard.multiplier.y,
                -gameBoard.discardPile.layerID
            ));
            e.ReplaceSortOrder(0);
        }

        /// <summary>
        /// 移动当前牌到弃牌堆
        /// </summary>
        /// <param name="game">Game.</param>
        /// <param name="e">E.</param>
        public static void MoveToDiscard(this GameContext game, GameEntity e) {
            GameBoardComponent gameBoard = game.gameBoard;
            e.ReplaceCardProspector(gameBoard.discardPile.id, gameBoard.discardPile);
            e.ReplaceCardState(CardState.discard);
            e.ReplaceFaceUp(true);
            e.ReplacePosition(new Vector3(
                gameBoard.discardPile.x * gameBoard.multiplier.x,
                gameBoard.discardPile.y * gameBoard.multiplier.y,
                -gameBoard.discardPile.layerID + 0.5f
            ));
            e.ReplaceSortOrder(-100 + game.gameData.discardPile.Count);
            game.gameData.discardPile.Add(e);
        }

        public static void UpdateDrawPile(this GameContext game) {
            GameBoardComponent gameBoard = game.gameBoard;

            for (int i = 0; i < game.gameData.drawPile.Count; i++)
            {
                var e = game.gameData.drawPile[i];
                e.ReplaceCardProspector(gameBoard.drawPile.id, gameBoard.drawPile);
                e.ReplaceFaceUp(false);
                e.ReplaceCardState(CardState.drawpile);
                e.ReplacePosition(new Vector3(
                    (gameBoard.drawPile.x + i * gameBoard.drawPile.stagger.x) * gameBoard.multiplier.x,
                    (gameBoard.drawPile.y + i * gameBoard.drawPile.stagger.y) * gameBoard.multiplier.y,
                    -gameBoard.discardPile.layerID + 0.1f * i
                ));
                e.ReplaceSortOrder(-10 * i);
            }
        }

        public static void SetTableauFaces(this GameContext game) {
            GameBoardComponent gameBoard = game.gameBoard;
            foreach (var card in game.gameData.tableau)
            {
                var hiddenBy = card.hiddenBy.hiddenBy;
                var faceUp = true;
                foreach (var item in hiddenBy)
                {
                    if (item.cardState.value == CardState.tableau)
                    {
                        faceUp = false;
                    }
                }
                card.ReplaceFaceUp(faceUp);
            }
        }


    }
}

