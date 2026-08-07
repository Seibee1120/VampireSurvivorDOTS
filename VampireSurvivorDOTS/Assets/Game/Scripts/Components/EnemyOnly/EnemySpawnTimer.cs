using Unity.Entities;

namespace Game.Scripts.Components.EnemyOnly
{
    public struct EnemySpawnTimer : IComponentData
    {
        public float Timer;
        public float Interval;
    }
}