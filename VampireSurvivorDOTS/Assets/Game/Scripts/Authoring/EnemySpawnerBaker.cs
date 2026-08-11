using Game.Scripts.Components.EnemyOnly;
using Unity.Entities;

namespace Game.Scripts.Authoring
{
    public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new EnemySpawnConfig
            {
                BaseSpawnInterval = authoring.spawnInterval,
                SpawnInterval = authoring.spawnInterval,
                SpawnCount = authoring.spawnCount,
                SpawnDistance = authoring.spawnDistance,
                Timer = 0f,
                ElapsedTime = 0f,
                EnemyPrefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                MinSpawnInterval = authoring.minSpawnInterval,
                IntervalDecayRate = authoring.intervalDecayRate,
                MaxSpawnCount = authoring.maxSpawnCount,
                SpawnCountIncreasePerMin = authoring.spawnCountIncreasePerMin
            });
        }
    }
}