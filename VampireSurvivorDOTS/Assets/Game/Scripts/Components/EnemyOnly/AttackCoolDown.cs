using Unity.Entities;

namespace Game.Scripts.Components.EnemyOnly
{
    public struct AttackCoolDown : IComponentData
    {
        public float Timer;
        public float Interval;
    }
}