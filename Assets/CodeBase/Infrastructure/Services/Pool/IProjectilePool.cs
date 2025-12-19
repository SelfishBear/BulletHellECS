using UnityEngine;

namespace CodeBase.Infrastructure.Services.Pool
{
    public interface IProjectilePool : IService
    {
        void Initialize(GameObject prefab, int poolSize);
        GameObject Get();
        void Return(GameObject projectile);
        bool HasAvailable();
    }
}

