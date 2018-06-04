using Entitas;

namespace Bartok
{
    [Game]
    public sealed class TouchComponent : IComponent
    {
        public GameEntity target;
    }
}