using Game.Scripts.Components.Tags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Game.Scripts.GameObjectBridge
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float3 offset = new(0, 15, -10);
        [SerializeField] private float smoothSpeed = 5f;

        private EntityManager _entityManager;
        private Entity _playerEntity;
        private EntityQuery _playerQuery;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _playerQuery = _entityManager.CreateEntityQuery(typeof(PlayerTag), typeof(LocalTransform));
        }

        private void LateUpdate()
        {
            if (_playerEntity == Entity.Null || !_entityManager.Exists(_playerEntity))
                if (!_playerQuery.TryGetSingletonEntity<LocalTransform>(out _playerEntity))
                    return;
            var playerTransform = _entityManager.GetComponentData<LocalTransform>(_playerEntity);
            var targetPosition = playerTransform.Position + offset;
            var t = 1f - math.exp(-smoothSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(targetPosition.x, targetPosition.y, targetPosition.z), t);
        }

        private void OnDestroy()
        {
            if (_playerQuery != null) _playerQuery.Dispose();
        }
    }
}