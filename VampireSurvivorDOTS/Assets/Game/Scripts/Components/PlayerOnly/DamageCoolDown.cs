using Unity.Entities;

namespace Game.Scripts.Components.PlayerOnly
{
    public struct DamageCoolDown : IComponentData
    {
        public float Timer;
        public float Duration;
    }
}