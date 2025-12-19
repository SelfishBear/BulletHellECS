using CodeBase.Data;
using CodeBase.Infrastructure.Services.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class ProjectilePoolInitSystem : IEcsInitSystem
    {
        private IProjectilePool _projectilePool;
        private StaticData _staticData;

        public void Init()
        {
            GameObject playerPrefab = _staticData.PlayerPrefab;
            WeaponSettings weaponSettings = playerPrefab.GetComponent<WeaponSettings>();

            if (weaponSettings == null || weaponSettings.ProjectilePrefab == null)
            {
                Debug.LogError("WeaponSettings or ProjectilePrefab not found on PlayerPrefab!");
                return;
            }

            _projectilePool.Initialize(weaponSettings.ProjectilePrefab, _staticData.ProjectilePoolSize);
            
            Debug.Log($"Projectile pool initialized with {_staticData.ProjectilePoolSize} projectiles");
        }
    }
}

