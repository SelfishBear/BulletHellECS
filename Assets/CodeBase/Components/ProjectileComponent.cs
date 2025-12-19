using UnityEngine;

namespace CodeBase.Components
{
    public struct ProjectileComponent
    {
        public GameObject ProjectileGameObject;
        public Vector3 Direction;
        public float Speed;
        public float Lifetime;
        public float SpawnTime;
        public int Damage;
    }
}

