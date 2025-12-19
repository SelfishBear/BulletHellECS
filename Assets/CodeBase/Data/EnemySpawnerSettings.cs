using UnityEngine;

namespace CodeBase.Data
{
    public class EnemySpawnerSettings : MonoBehaviour
    {
        [Header("Spawn Settings")]
        public GameObject EnemyPrefab;
        public float SpawnInterval = 2f;
        public int MaxEnemies = 10;
        
        [Header("Spawn Point")]
        public Transform SpawnPoint;

        private void OnValidate()
        {
            if (SpawnPoint == null)
                SpawnPoint = transform;
        }
    }
}

