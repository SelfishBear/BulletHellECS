using CodeBase.Data;
using CodeBase.Infrastructure.Services.Cursor;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Systems;
using Leopotam.Ecs;
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

        [Inject]
        public void Construct(StaticData staticData, EcsWorld world, IInputService inputService, ICursorService cursorService)
        {
            _staticData = staticData;
            _world = world;
            _inputService = inputService;
            _cursorService = cursorService;
        }

        private RuntimeData _runtimeData;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;


        private void Start()
        {
            _runtimeData = new RuntimeData();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);

            _updateSystems
                .Add(new CursorInitSystem())
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new RotatePlayerSystem())
                .Add(new PlayerMoveSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new CameraFollowSystem())
                .Inject(_staticData)
                .Inject(_sceneData)
                .Inject(_runtimeData)
                .Inject(_inputService)
                .Inject(_cursorService)
                .Init();

            _fixedUpdateSystems
                .Init();
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