using Unity.Entities;
using Unity.Mathematics;

namespace Game.Scripts.Components.EnemyOnly
{
    public struct RandomState : IComponentData
    {
        public Random Value;
    }
}