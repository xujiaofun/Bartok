using Entitas;
using System.Collections.Generic;

namespace Bartok
{
    [Game]
    public sealed class HiddenByComponent : IComponent
    {
        public List<GameEntity> hiddenBy = new List<GameEntity>();
    }
}