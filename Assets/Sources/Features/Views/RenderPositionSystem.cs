using Entitas;
using System.Collections.Generic;

namespace Bartok
{
    public sealed class RenderPositionSystem : ReactiveSystem<GameEntity>
    {
        public RenderPositionSystem(Contexts contexts)
            : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.GameObject));
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                var tGO = e.gameObject.value;
                tGO.transform.localPosition = e.position.value;
            }
        }
    }
}
