using CodeBase.Components;
using CodeBase.Infrastructure.Services.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class ProjectileLifetimeSystem : IEcsRunSystem
    {
        private IProjectilePool _projectilePool;
        private EcsFilter<ProjectileComponent> _projectileFilter = null;

        public void Run()
        {
            foreach (var idx in _projectileFilter)
            {
                ref var projectile = ref _projectileFilter.Get1(idx);

                float elapsedTime = Time.time - projectile.SpawnTime;

                if (elapsedTime >= projectile.Lifetime)
                {
                    if (projectile.ProjectileGameObject != null)
                        _projectilePool.Return(projectile.ProjectileGameObject);

                    _projectileFilter.GetEntity(idx).Destroy();
                }
            }
        }
    }
}

