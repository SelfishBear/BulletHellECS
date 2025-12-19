using CodeBase.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class EnemyChaseSystem : IEcsRunSystem
    {
        private EcsFilter<EnemyChaseComponent, EnemyComponent> _filter;
        private EcsFilter<PlayerComponent> _playerFilter;

        public void Run()
        {
            if (_playerFilter.IsEmpty()) return;

            ref var player = ref _playerFilter.Get1(0);
            Vector3 playerPosition = player.PlayerTransform.position;

            foreach (var i in _filter)
            {
                ref var chase = ref _filter.Get1(i);
                ref var enemy = ref _filter.Get2(i);

                Vector3 enemyPosition = enemy.EnemyTransform.position;
                Vector3 direction = playerPosition - enemyPosition;
                
                direction.y = 0;

                float distanceSqr = direction.sqrMagnitude;

                if (distanceSqr > chase.StoppingDistance * chase.StoppingDistance)
                {
                    direction.Normalize();
                    enemy.EnemyTransform.position += direction * chase.Speed * Time.deltaTime;
                    
                    if (direction != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        enemy.EnemyTransform.rotation = Quaternion.Slerp(enemy.EnemyTransform.rotation, targetRotation, 10f * Time.deltaTime);
                    }
                }
            }
        }
    }
}

