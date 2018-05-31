using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Bartok
{
    [Game, Unique]
    public sealed class DeckResMgrComponent : IComponent
    {
        public DeckResMgr value;
    }
}