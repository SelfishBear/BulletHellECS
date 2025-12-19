using CodeBase.Animation;
using CodeBase.Components;
using CodeBase.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        private SceneData _sceneData;

        public void Init()
        {
            EcsEntity playerEntity = _world.NewEntity();

            ref var playerComponent = ref playerEntity.Get<PlayerComponent>();
            ref var inputDataComponent = ref playerEntity.Get<InputDataComponent>();
            ref var animationComponent = ref playerEntity.Get<PlayerAnimatorComponent>();
            ref var gravityComponent = ref playerEntity.Get<GravityComponent>();

            ConfigurePlayer(ref playerComponent, ref gravityComponent, ref animationComponent);
        }

        private void ConfigurePlayer(ref PlayerComponent playerComponent, ref GravityComponent gravityComponent, ref PlayerAnimatorComponent playerAnimatorComponent)
        {
            GameObject playerInstance = Object.Instantiate(_staticData.PlayerPrefab,
                _sceneData.PlayerSpawnPoint.position, Quaternion.identity);

            playerComponent.PlayerTransform = playerInstance.transform;

            playerAnimatorComponent.HeroAnimator = playerInstance.GetComponent<HeroAnimator>();
            playerComponent.PlayerCharacterController = playerInstance.GetComponent<CharacterController>();
            playerComponent.MoveSpeed = _staticData.PlayerMoveSpeed;

            gravityComponent.Gravity = _staticData.Gravity;
            gravityComponent.VerticalVelocity = 0f;
        }
    }
}