using Game.Scripts.Components;
using Game.Scripts.Components.EnemyOnly;
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
            var dt = SystemAPI.Time.DeltaTime;

            var playerQuery = SystemAPI.QueryBuilder()
                .WithAll<PlayerTag, LocalTransform, Radius>()
                .WithAllRW<Health>()
                .Build();

            if (!playerQuery.TryGetSingleton<LocalTransform>(out var playerTransform) ||
                !playerQuery.TryGetSingleton<Radius>(out var playerRadius) ||
                !playerQuery.TryGetSingletonRW<Health>(out var playerHealth))
                return;

            foreach (var (enemyCooldown, enemyRadius, enemyDamage, enemyTransform) in
                     SystemAPI.Query<RefRW<AttackCoolDown>, RefRO<Radius>, RefRO<Damage>, RefRO<LocalTransform>>()
                         .WithAll<EnemyTag>())
            {
                var distanceSq = math.distancesq(enemyTransform.ValueRO.Position, playerTransform.Position);
                var combinedRadius = enemyRadius.ValueRO.Value + playerRadius.Value;
                if (distanceSq > combinedRadius * combinedRadius) continue;

                if (enemyCooldown.ValueRO.Timer > 0f)
                {
                    enemyCooldown.ValueRW.Timer -= dt;
                    continue;
                }

                playerHealth.ValueRW.Value -= enemyDamage.ValueRO.Value;
                enemyCooldown.ValueRW.Timer = enemyCooldown.ValueRO.Interval;
            }
        }
    }
}