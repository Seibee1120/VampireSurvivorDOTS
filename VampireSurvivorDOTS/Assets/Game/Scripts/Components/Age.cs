using Unity.Entities;

namespace Game.Scripts.Components
{
    public struct Age : IComponentData
    {
        public float Elapsed;
        public float Max;
    }
}