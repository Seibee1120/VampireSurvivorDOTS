using Unity.Entities;

namespace Game.Scripts.Components.EnemyOnly
{
    public struct EnemySpawnConfig : IComponentData
    {
        public float BaseSpawnInterval;
        public float SpawnInterval;
        public int SpawnCount;
        public float SpawnDistance;
        public float Timer;
        public float ElapsedTime;
        public Entity EnemyPrefab;
        public float MinSpawnInterval;
        public float IntervalDecayRate;
        public int MaxSpawnCount;
        public int SpawnCountIncreasePerMin;
    }
}