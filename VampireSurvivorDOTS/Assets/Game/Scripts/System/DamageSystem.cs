using Game.Scripts.Components;
using Game.Scripts.Components.PlayerOnly;
using Game.Scripts.Components.Tags;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Scripts.System
{
    [BurstCompile]
    public partial struct DamageSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
            state.RequireForUpdate<EnemyTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerQuery = SystemAPI.QueryBuilder().WithAll<PlayerTag>().Build();
            if (!playerQuery.TryGetSingletonEntity<PlayerTag>(out var playerEntity) ||
                !playerQuery.TryGetSingleton<LocalTransform>(out var playerTransform) ||
                !playerQuery.TryGetSingleton<Radius>(out var playerRadius) ||
                !playerQuery.TryGetSingletonRW<DamageCoolDown>(out var playerDamageCoolDown) ||
                !playerQuery.TryGetSingletonRW<Health>(out var playerHealth)) return;

            playerDamageCoolDown.ValueRW.Timer -= SystemAPI.Time.DeltaTime;

            foreach (var valueTuple in SystemAPI
                         .Query<RefRO<Radius>, RefRO<Damage>, RefRO<LocalTransform>>().WithAll<EnemyTag>())
            {
                var (enemyRadius, enemyDamage, enemyTransform) = valueTuple;
                var distanceSq = math.distancesq(enemyTransform.ValueRO.Position, playerTransform.Position);
                var combinedRadius = enemyRadius.ValueRO.Value + playerRadius.Value;
                if (distanceSq > combinedRadius * combinedRadius) continue;
                if (playerDamageCoolDown.ValueRO.Timer > 0f) continue;
                playerHealth.ValueRW.Value -= enemyDamage.ValueRO.Value;
                playerDamageCoolDown.ValueRW.Timer = playerDamageCoolDown.ValueRO.Duration;
            }
        }
    }
}