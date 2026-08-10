using Game.Scripts.Components;
using Game.Scripts.Components.Tags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Scripts.System
{
    public partial struct PlayerInputSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var input = new float3(horizontal, 0, vertical);
            input = math.lengthsq(input) > 0 ? math.normalize(input) : input;
            foreach (var moveDirection in SystemAPI.Query<RefRW<MoveDirection>>().WithAll<PlayerTag>())
                moveDirection.ValueRW.Value = input;
        }
    }
}