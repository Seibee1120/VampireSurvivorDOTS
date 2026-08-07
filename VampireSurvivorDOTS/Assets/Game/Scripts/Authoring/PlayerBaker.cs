using Game.Scripts.Components;
using Game.Scripts.Components.Tags;
using Unity.Entities;

namespace Game.Scripts.Authoring
{
    public class PlayerBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(entity);
            AddComponent<MoveDirection>(entity);
            AddComponent(entity, new MoveSpeed { Value = authoring.moveSpeed });
        }
    }
}