using CodeBase.Components;
using CodeBase.Data;
using CodeBase.MonoBehaviours;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class EnemySpawnerSystem : IEcsRunSystem
    {
        private EcsFilter<EnemySpawnerComponent> _spawnerFilter = null;
        private EcsWorld _world = null;
        private RuntimeData _runtimeData = null;

        public void Run()
        {
            foreach (var idx in _spawnerFilter)
            {
                ref var spawner = ref _spawnerFilter.Get1(idx);

                if (spawner.CurrentEnemyCount >= spawner.MaxEnemies)
                    continue;

                float currentTime = Time.time;
                
                if (currentTime - spawner.LastSpawnTime < spawner.SpawnInterval)
                    continue;

                spawner.LastSpawnTime = currentTime;

                SpawnEnemy(ref spawner);
            }
        }

        private void SpawnEnemy(ref EnemySpawnerComponent spawner)
        {
            if (spawner.EnemyPrefab == null || spawner.SpawnPoint == null)
                return;

            GameObject enemyGo = Object.Instantiate(
                spawner.EnemyPrefab,
                spawner.SpawnPoint.position,
                spawner.SpawnPoint.rotation
            );

            EcsEntity enemyEntity = _world.NewEntity();
            ref var enemy = ref enemyEntity.Get<EnemyComponent>();

            enemy.EnemyGameObject = enemyGo;
            enemy.EnemyTransform = enemyGo.transform;

            ref var chase = ref enemyEntity.Get<EnemyChaseComponent>();
            chase.Speed = 3f;
            chase.StoppingDistance = 1f;

            var collisionHandler = enemyGo.GetComponent<EnemyCollisionHandler>();
            if (collisionHandler == null)
                collisionHandler = enemyGo.AddComponent<EnemyCollisionHandler>();
            
            collisionHandler.Initialize(_runtimeData);

            spawner.CurrentEnemyCount++;

            Debug.Log($"Enemy spawned! Total: {spawner.CurrentEnemyCount}/{spawner.MaxEnemies}");
        }
    }
}
