using CodeBase.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class ProjectileMoveSystem : IEcsRunSystem
    {
        private EcsFilter<ProjectileComponent> _projectileFilter = null;

        public void Run()
        {
            foreach (var idx in _projectileFilter)
            {
                ref var projectile = ref _projectileFilter.Get1(idx);

                if (projectile.ProjectileGameObject == null)
                    continue;

                Vector3 movement = projectile.Direction * projectile.Speed * Time.deltaTime;
                projectile.ProjectileGameObject.transform.position += movement;
            }
        }
    }
}

