using Entitas;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

namespace Bartok
{
    [Game, Unique]
    public sealed class CardCacheComponent : IComponent
    {
        public List<GameEntity> value;
    }
}