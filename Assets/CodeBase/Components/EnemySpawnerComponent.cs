using UnityEngine;

namespace CodeBase.Components
{
    public struct EnemySpawnerComponent
    {
        public GameObject EnemyPrefab;
        public Transform SpawnPoint;
        public float SpawnInterval;
        public float LastSpawnTime;
        public int MaxEnemies;
        public int CurrentEnemyCount;
    }
}

