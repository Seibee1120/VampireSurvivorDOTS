using Game.Scripts.Components;
using Game.Scripts.Components.Tags;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Scripts.System
{
    [BurstCompile]
    public partial struct EnemyAISystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerQuery = SystemAPI.QueryBuilder().WithAll<PlayerTag, LocalTransform>().Build();
            if (!playerQuery.TryGetSingleton<LocalTransform>(out var playerTransform))
                return;
            var playerPositon = playerTransform.Position;
            foreach (var (moveDirection, localTransform) in SystemAPI
                         .Query<RefRW<MoveDirection>, RefRW<LocalTransform>>().WithAll<EnemyTag>())
            {
                var directionToPlayer = playerPositon - localTransform.ValueRO.Position;
                moveDirection.ValueRW.Value = math.lengthsq(directionToPlayer) > 0.01f
                    ? math.normalize(directionToPlayer)
                    : float3.zero;
            }
        }
    }
}