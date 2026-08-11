using UnityEngine;

namespace Game.Scripts.Authoring
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public float spawnInterval = 3f;
        public int spawnCount = 3;
        public float spawnDistance = 15f;
        public float minSpawnInterval = 0.5f;
        public float intervalDecayRate = 0.3f;
        public int maxSpawnCount = 20;
        public int spawnCountIncreasePerMin = 2;
        public GameObject enemyPrefab;
    }
}