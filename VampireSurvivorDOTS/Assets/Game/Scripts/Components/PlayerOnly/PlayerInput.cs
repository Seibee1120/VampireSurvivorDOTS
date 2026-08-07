using Unity.Entities;
using Unity.Mathematics;

namespace Game.Scripts.Components.PlayerOnly
{
    public struct PlayerInput : IComponentData
    {
        public float2 Value;
    }
}