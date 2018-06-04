using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
public sealed class PositionComponent : IComponent {
    public Vector3 value;
}
