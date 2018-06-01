using Entitas;
using System.Collections.Generic;

namespace Bartok
{
    [Game]
    public sealed class CardProspectorComponent : IComponent
    {
        public int layoutID;
        public SlotDef slotDef;
    }
}