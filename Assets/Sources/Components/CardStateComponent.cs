using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Bartok
{
    [Game, Event(true)]
    public sealed class CardStateComponent : IComponent
    {
        public CardState value;
    }
}