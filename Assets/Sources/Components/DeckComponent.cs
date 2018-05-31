using Entitas;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

namespace Bartok
{
    [Game, Unique]
    public sealed class DeckComponent : IComponent
    {
        public List<Decorator> decotators;
        public List<CardDefinition> cardDefs;
    }
}