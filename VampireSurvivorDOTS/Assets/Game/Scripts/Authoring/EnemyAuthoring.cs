using UnityEngine;

namespace Game.Scripts.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float health = 100f;
        public float damage = 10f;
        public float attackInterval = 0.5f;
        public float collisionRadius = 0.5f;
    }
}