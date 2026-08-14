using Game.Scripts.Components;
using Game.Scripts.Components.Tags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Scripts.System
{
    [BurstCompile]
    [UpdateAfter(typeof(DamageSystem))]
    public partial struct DeathSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Health>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            // 玩家死亡：不销毁，标记 GameOverTag
            foreach (var (health, entity) in SystemAPI.Query<RefRO<Health>>()
                         .WithAll<PlayerTag>()
                         .WithNone<GameOverTag>()
                         .WithEntityAccess())
            {
                if (health.ValueRO.Value > 0f) continue;
                ecb.AddComponent<GameOverTag>(entity);
            }

            // 其它实体死亡：销毁
            foreach (var (health, entity) in SystemAPI.Query<RefRO<Health>>()
                         .WithNone<PlayerTag>()
                         .WithEntityAccess())
            {
                if (health.ValueRO.Value > 0f) continue;
                ecb.DestroyEntity(entity);
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}