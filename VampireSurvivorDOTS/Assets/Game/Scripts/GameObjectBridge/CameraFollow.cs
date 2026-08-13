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

        private EntityQuery _playerQuery;

        private void Start()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null || !world.IsCreated)
                return;

            _playerQuery = world.EntityManager.CreateEntityQuery(
                typeof(PlayerTag), typeof(LocalTransform));
        }

        private void LateUpdate()
        {
            if (_playerQuery == default)
                return;

            if (!_playerQuery.TryGetSingleton<LocalTransform>(out var playerTransform))
                return;

            var targetPosition = playerTransform.Position + offset;
            var t = 1f - math.exp(-smoothSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(targetPosition.x, targetPosition.y, targetPosition.z),
                t);
        }

        private void OnDestroy()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (_playerQuery != default && world is { IsCreated: true })
                _playerQuery.Dispose();
        }
    }
}