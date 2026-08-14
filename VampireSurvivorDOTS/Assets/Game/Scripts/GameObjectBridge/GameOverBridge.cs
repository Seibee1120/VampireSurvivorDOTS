using Game.Scripts.Components.Tags;
using Unity.Entities;
using UnityEngine;

namespace Game.Scripts.GameObjectBridge
{
    public class GameOverBridge : MonoBehaviour
    {
        private EntityQuery _gameOverQuery;
        private bool _handled;

        private void Start()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null || !world.IsCreated)
                return;

            _gameOverQuery = world.EntityManager.CreateEntityQuery(
                typeof(GameOverTag), typeof(PlayerTag));
        }

        private void Update()
        {
            if (_handled || _gameOverQuery == default)
                return;

            if (_gameOverQuery.IsEmptyIgnoreFilter)
                return;

            _handled = true;
            Time.timeScale = 0f;
            ShowGameOver();
        }

        private void OnDestroy()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (_gameOverQuery != default && world is { IsCreated: true })
                _gameOverQuery.Dispose();
        }

        private static void ShowGameOver()
        {
            // TODO: 弹出游戏结束 UI 界面
            Debug.Log("Game Over");
        }
    }
}