using CodeBase.Components.Events;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Cursor;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Pool;
using CodeBase.Systems;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;
using Zenject;

namespace CodeBase.EcsStartup
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneData _sceneData;

        private StaticData _staticData;
        private EcsWorld _world;
        private IInputService _inputService;
        private ICursorService _cursorService;
        private IProjectilePool _projectilePool;

        [Inject]
        public void Construct(StaticData staticData, EcsWorld world, IInputService inputService,
            ICursorService cursorService, IProjectilePool projectilePool)
        {
            _staticData = staticData;
            _world = world;
            _inputService = inputService;
            _cursorService = cursorService;
            _projectilePool = projectilePool;
        }

        private RuntimeData _runtimeData;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;


        private void Start()
        {
#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
#endif
            _runtimeData = new RuntimeData();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);

            _updateSystems
                .Add(new CursorInitSystem())
                .Add(new ProjectilePoolInitSystem())
                .Add(new PlayerInitSystem())
                .Add(new HudInitSystem())
                .Add(new EnemySpawnerInitSystem()) 
                .Add(new PlayerInputSystem())
                .Add(new RotatePlayerSystem())
                .Add(new PlayerMoveSystem())
                .Add(new WeaponFireSystem())
                .Add(new ProjectileMoveSystem())
                .Add(new ProjectileLifetimeSystem())
                .Add(new EnemySpawnerSystem()) 
                .Add(new EnemyChaseSystem())
                .Add(new PlayerHealthDamageSystem())
                .Add(new PlayerHealthUIUpdateSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new CameraFollowSystem())
                .OneFrame<EventHitComponent>()
                .Inject(_staticData)
                .Inject(_sceneData)
                .Inject(_runtimeData)
                .Inject(_inputService)
                .Inject(_cursorService)
                .Inject(_projectilePool)
                .Init();

            _fixedUpdateSystems
                .Init();

#if UNITY_EDITOR
            EcsSystemsObserver.Create(_updateSystems);
            EcsSystemsObserver.Create(_fixedUpdateSystems);
#endif
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;
            }

            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}