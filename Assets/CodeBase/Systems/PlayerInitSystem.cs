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
        private RuntimeData _runtimeData;

        public void Init()
        {
            EcsEntity playerEntity = _world.NewEntity();
            
            _runtimeData.PlayerEntity = playerEntity;

            ref var playerComponent = ref playerEntity.Get<PlayerComponent>();
            playerEntity.Get<InputDataComponent>();
            ref var animationComponent = ref playerEntity.Get<PlayerAnimatorComponent>();
            ref var gravityComponent = ref playerEntity.Get<GravityComponent>();
            
            ref var playerHealthComponent = ref playerEntity.Get<PlayerHealthComponent>();
            
            GameObject playerInstance = Object.Instantiate(_staticData.PlayerPrefab,
                _sceneData.PlayerSpawnPoint.position, Quaternion.identity);

            EcsEntity weaponEntity = _world.NewEntity();
            WeaponSettings weaponView = playerInstance.GetComponent<WeaponSettings>();

            weaponEntity.Get<HasWeaponComponent>();
            ref var weapon = ref weaponEntity.Get<WeaponComponent>();

            weapon.OwnerEntity = playerEntity;
            weapon.ProjectilePrefab = weaponView.ProjectilePrefab;
            weapon.ProjectileRadius = weaponView.ProjectileRadius;
            weapon.ProjectileSpawnPoint = weaponView.ProjectileSpawnPoint;
            weapon.ProjectileSpeed = weaponView.ProjectileSpeed;
            weapon.Damage = weaponView.Damage;
            weapon.FireRate = weaponView.FireRate;
            weapon.LastFireTime = 0f;
            weapon.ProjectileLifetime = weaponView.ProjectileLifetime;
            
            playerHealthComponent.PlayerCurrentHealth = _staticData.PlayerCurrentHealth;
            playerHealthComponent.PlayerMaxHealth = _staticData.PlayerMaxHealth;
            
            
            playerComponent.PlayerTransform = playerInstance.transform;
            playerComponent.PlayerCharacterController = playerInstance.GetComponent<CharacterController>();
            playerComponent.MoveSpeed = _staticData.PlayerMoveSpeed;
            
            animationComponent.HeroAnimator = playerInstance.GetComponent<HeroAnimator>();

            gravityComponent.Gravity = _staticData.Gravity;
            gravityComponent.VerticalVelocity = 0f;

        }
    }
}