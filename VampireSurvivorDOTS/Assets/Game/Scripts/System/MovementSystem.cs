using Game.Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Game.Scripts.System
{
    [BurstCompile]
    public partial struct MovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;
            foreach (var (transform, direction, speed) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRO<MoveDirection>, RefRO<MoveSpeed>>())
                transform.ValueRW.Position += direction.ValueRO.Value * speed.ValueRO.Value * dt;
        }
    }
}