using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Pool
{
    public class ProjectilePool : IProjectilePool
    {
        private GameObject _prefab;
        private readonly Queue<GameObject> _availableProjectiles = new Queue<GameObject>();
        private readonly List<GameObject> _allProjectiles = new List<GameObject>();
        private Transform _poolContainer;

        public void Initialize(GameObject prefab, int poolSize)
        {
            _prefab = prefab;
            
            _poolContainer = new GameObject("[Projectile Pool]").transform;
            Object.DontDestroyOnLoad(_poolContainer.gameObject);

            for (int i = 0; i < poolSize; i++)
            {
                GameObject projectile = Object.Instantiate(prefab, _poolContainer);
                projectile.name = $"Projectile_{i}";
                projectile.SetActive(false);
                
                _availableProjectiles.Enqueue(projectile);
                _allProjectiles.Add(projectile);
            }
        }

        public GameObject Get()
        {
            if (_availableProjectiles.Count == 0)
            {
                Debug.LogWarning("Projectile pool is empty! Consider increasing pool size.");
                GameObject newProjectile = Object.Instantiate(_prefab, _poolContainer);
                newProjectile.name = $"Projectile_Extra_{_allProjectiles.Count}";
                _allProjectiles.Add(newProjectile);
                return newProjectile;
            }

            GameObject projectile = _availableProjectiles.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }

        public void Return(GameObject projectile)
        {
            if (projectile == null)
                return;

            projectile.SetActive(false);
            projectile.transform.SetParent(_poolContainer);
            projectile.transform.position = Vector3.zero;
            
            if (!_availableProjectiles.Contains(projectile))
                _availableProjectiles.Enqueue(projectile);
        }

        public bool HasAvailable()
        {
            return _availableProjectiles.Count > 0;
        }
    }
}
