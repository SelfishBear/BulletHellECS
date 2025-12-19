using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Components
{
    public struct WeaponComponent
    {
        public EcsEntity OwnerEntity;
        public GameObject ProjectilePrefab;
        public Transform ProjectileSpawnPoint;
        public float ProjectileSpeed;
        public float ProjectileRadius;
        public int Damage;
        public float FireRate;
        public float LastFireTime;
        public float ProjectileLifetime;
    }
}