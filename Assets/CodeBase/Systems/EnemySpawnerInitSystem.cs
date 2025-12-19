using CodeBase.Components;
using CodeBase.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class EnemySpawnerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;

        public void Init()
        {
            EnemySpawnerSettings[] spawners = Object.FindObjectsByType<EnemySpawnerSettings>(FindObjectsSortMode.None);

            if (spawners.Length == 0)
            {
                Debug.LogWarning("No enemy spawners found on scene!");
                return;
            }

            foreach (var spawnerSettings in spawners)
            {
                if (spawnerSettings.EnemyPrefab == null)
                {
                    Debug.LogError($"Enemy prefab not assigned on spawner '{spawnerSettings.name}'!");
                    continue;
                }

                if (spawnerSettings.SpawnPoint == null)
                    spawnerSettings.SpawnPoint = spawnerSettings.transform;

                EcsEntity spawnerEntity = _world.NewEntity();
                ref var spawner = ref spawnerEntity.Get<EnemySpawnerComponent>();

                spawner.EnemyPrefab = spawnerSettings.EnemyPrefab;
                spawner.SpawnPoint = spawnerSettings.SpawnPoint;
                spawner.SpawnInterval = spawnerSettings.SpawnInterval;
                spawner.MaxEnemies = spawnerSettings.MaxEnemies;
                spawner.LastSpawnTime = -spawnerSettings.SpawnInterval;
                spawner.CurrentEnemyCount = 0;

                Debug.Log($"Enemy spawner initialized: {spawnerSettings.name} " +
                          $"(Interval: {spawnerSettings.SpawnInterval}s, Max: {spawnerSettings.MaxEnemies})");
            }
        }
    }
}
