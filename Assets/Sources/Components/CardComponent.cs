using Entitas;
using System.Collections.Generic;
using UnityEngine;

namespace Bartok
{
    [Game]
    public sealed class CardComponent : IComponent
    {
        public string name;
        public string suit;
        public int rank;
        public CardDefinition def;
        public List<GameObject> decoGOs;
        public Color color;
        public string colS;
    }
}