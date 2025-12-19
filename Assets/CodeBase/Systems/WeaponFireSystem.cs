using CodeBase.Components;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class WeaponFireSystem : IEcsRunSystem
    {
        private IProjectilePool _projectilePool;
        
        private EcsFilter<WeaponComponent, HasWeaponComponent> _weaponFilter = null;
        private EcsWorld _world = null;

        public void Run()
        {
            foreach (var idx in _weaponFilter)
            {
                ref var weapon = ref _weaponFilter.Get1(idx);

                float currentTime = Time.time;
                
                if (currentTime - weapon.LastFireTime < weapon.FireRate)
                    continue;

                weapon.LastFireTime = currentTime;

                SpawnProjectile(ref weapon);
            }
        }

        private void SpawnProjectile(ref WeaponComponent weapon)
        {
            if (weapon.ProjectileSpawnPoint == null)
                return;

            if (!_projectilePool.HasAvailable())
            {
                Debug.LogWarning("No available projectiles in pool!");
                return;
            }

            GameObject projectileGo = _projectilePool.Get();
            
            projectileGo.transform.position = weapon.ProjectileSpawnPoint.position;
            projectileGo.transform.rotation = weapon.ProjectileSpawnPoint.rotation;

            EcsEntity projectileEntity = _world.NewEntity();
            ref var projectile = ref projectileEntity.Get<ProjectileComponent>();

            projectile.ProjectileGameObject = projectileGo;
            projectile.Direction = weapon.ProjectileSpawnPoint.forward;
            projectile.Speed = weapon.ProjectileSpeed;
            projectile.Lifetime = weapon.ProjectileLifetime;
            projectile.SpawnTime = Time.time;
            projectile.Damage = weapon.Damage;
        }
    }
}

