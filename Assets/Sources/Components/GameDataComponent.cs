using Entitas;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

namespace Bartok
{
    [Game, Unique]
    public sealed class GameDataComponent : IComponent
    {
        public GameEntity target;
        public List<GameEntity> drawPile;
        public List<GameEntity> discardPile;
        public List<GameEntity> tableau;
    }
}