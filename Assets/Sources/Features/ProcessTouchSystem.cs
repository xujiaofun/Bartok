using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Bartok
{
    public sealed class ProcessTouchSystem : ReactiveSystem<GameEntity>
    {
        GameContext game;

        public ProcessTouchSystem(Contexts contexts)
            : base(contexts.game)
        {
            this.game = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Touch);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                var target = e.touch.target;
                if (target.cardState.value == CardState.drawpile) {
                    var card = game.gameData.drawPile[0];
                    game.gameData.drawPile.RemoveAt(0);
                    this.game.MoveToTarget(card);
                    this.game.UpdateDrawPile();
                    Debug.Log("ProcessTouchSystem" + game.gameData.drawPile.Count);
                }
                else if (target.cardState.value == CardState.tableau) {
                    if (target.faceUp.value == false)
                    {
                        return;
                    }
                    var d = Mathf.Abs(target.card.rank - game.gameData.target.card.rank);
                    if (d == 1 || d == 12)
                    {
                        this.game.MoveToTarget(target);
                        this.game.SetTableauFaces();
                    }
                }
            }
        }
    }
}
