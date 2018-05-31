using Entitas;
using UnityEngine;

namespace Bartok
{
    [Game]
    public sealed class GameObjectComponent : IComponent
    {
        public GameObject value;
    }
}