using Game.Scripts.Components;
using Game.Scripts.Components.EnemyOnly;
using Game.Scripts.Components.Tags;
using Unity.Entities;

namespace Game.Scripts.Authoring
{
    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<EnemyTag>(entity);
            AddComponent(entity, new MoveSpeed { Value = authoring.moveSpeed });
            AddComponent<MoveDirection>(entity);
            AddComponent(entity, new Health { Value = authoring.health });
            AddComponent(entity, new Damage { Value = authoring.damage });
            AddComponent(entity, new AttackCoolDown { Timer = 0f, Interval = authoring.attackInterval });
            AddComponent(entity, new Radius { Value = authoring.collisionRadius });
        }
    }
}