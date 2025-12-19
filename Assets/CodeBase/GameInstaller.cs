using CodeBase.Data;
using CodeBase.Infrastructure.Services.Cursor;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Pool;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace CodeBase
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private StaticData _staticData;
        
        public override void InstallBindings()
        {
            Container.Bind<StaticData>().FromInstance(_staticData).AsSingle();
            Container.Bind<EcsWorld>().FromMethod(_ => new EcsWorld()).AsSingle().NonLazy();
            Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();
            Container.Bind<IProjectilePool>().To<ProjectilePool>().AsSingle().NonLazy();
            Container.Bind<ICursorService>().To<CursorService>().AsSingle().NonLazy();
        }
    }
}