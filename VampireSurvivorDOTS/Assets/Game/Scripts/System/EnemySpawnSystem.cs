using Game.Scripts.Components.EnemyOnly;
using Game.Scripts.Components.Tags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Scripts.System
{
    [BurstCompile]
    public partial struct EnemySpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemySpawnConfig>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var random = new Random((uint)(SystemAPI.Time.ElapsedTime * 1000 + 1));

            // 拿玩家位置
            var playerQuery = SystemAPI.QueryBuilder().WithAll<PlayerTag, LocalTransform>().Build();
            if (!playerQuery.TryGetSingleton<LocalTransform>(out var playerTransform))
            {
                ecb.Dispose();
                return;
            }

            var playerPosition = playerTransform.Position;

            // 拿敌人生成器
            if (!SystemAPI.TryGetSingletonRW<EnemySpawnConfig>(out var config) ||
                config.ValueRO.EnemyPrefab == Entity.Null)
            {
                ecb.Dispose();
                return;
            }

            // 更新敌人生成器的计时器
            config.ValueRW.Timer -= dt;
            config.ValueRW.ElapsedTime += dt;

            if (config.ValueRO.Timer <= 0f)
            {
                // 生成敌人
                var spawnCount = config.ValueRO.SpawnCount;
                for (var i = 0; i < spawnCount; i++)
                {
                    // 玩家四周随机位置
                    var angle = random.NextFloat(0f, math.PI * 2f);
                    var direction = new float3(math.cos(angle), 0f, math.sin(angle));
                    var spawnPosition = playerPosition + direction * config.ValueRO.SpawnDistance;

                    var enemy = ecb.Instantiate(config.ValueRO.EnemyPrefab);
                    ecb.SetComponent(enemy, LocalTransform.FromPosition(spawnPosition));
                    ecb.AddComponent(enemy, new RandomState { Value = new Random(random.NextUInt()) });
                }

                // 难度随时间提升
                var elapsedMinutes = config.ValueRO.ElapsedTime / 60f;
                var newInterval = math.max(config.ValueRO.MinSpawnInterval,
                    config.ValueRO.BaseSpawnInterval - elapsedMinutes * config.ValueRO.IntervalDecayRate);
                var newSpawnCount = math.min(config.ValueRO.MaxSpawnCount,
                    config.ValueRO.SpawnCount + (int)(elapsedMinutes * config.ValueRO.SpawnCountIncreasePerMin));

                config.ValueRW.SpawnInterval = newInterval;
                config.ValueRW.SpawnCount = newSpawnCount;
                config.ValueRW.Timer = newInterval;
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}