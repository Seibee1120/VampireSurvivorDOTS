using Unity.Entities;

namespace Game.Scripts.Components
{
    public struct WeaponSlot : IBufferElementData
    {
        public Entity BulletPrefab;
        public float CooldownTimer;
        public float CooldownInterval;
        public int ProjectileCount;
        public float SpreadAngle;
        public float BulletSpeed;
    }
}