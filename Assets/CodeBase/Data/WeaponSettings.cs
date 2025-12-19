using UnityEngine;

namespace CodeBase.Data
{
    public class WeaponSettings : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public Transform ProjectileSpawnPoint;
        public float ProjectileSpeed = 10f;
        public float ProjectileRadius = 0.5f;
        public int Damage = 10;
        public float FireRate = 0.2f;
        public float ProjectileLifetime = 5f;
    }
}