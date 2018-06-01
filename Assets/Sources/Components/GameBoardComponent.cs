using Entitas;
using UnityEngine;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

namespace Bartok
{
    [Game, Unique]
    public sealed class GameBoardComponent : IComponent
    {
        public Vector2 multiplier;
        public List<SlotDef> slotDefs;
        public SlotDef drawPile;
        public SlotDef discardPile;
    }
}