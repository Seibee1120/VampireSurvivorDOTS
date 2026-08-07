using Unity.Entities;
using Unity.Mathematics;

namespace Game.Scripts.Components
{
    public struct MoveDirection : IComponentData
    {
        public float3 Value;
    }
}