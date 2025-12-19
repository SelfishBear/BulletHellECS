using CodeBase.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent, InputDataComponent, GravityComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var playerComponent = ref _filter.Get1(i);
                ref var inputDataComponent = ref _filter.Get2(i);
                ref var gravityComponent = ref _filter.Get3(i);

                if (playerComponent.PlayerCharacterController.isGrounded)
                {
                    gravityComponent.VerticalVelocity = -2f;
                }
                else
                {
                    gravityComponent.VerticalVelocity += gravityComponent.Gravity * Time.deltaTime;
                }

                Vector3 moveDirection = (Vector3.forward * inputDataComponent.MoveDirection.z + Vector3.right * inputDataComponent.MoveDirection.x).normalized;
                Vector3 horizontalMove = moveDirection * playerComponent.MoveSpeed * Time.deltaTime;
                Vector3 verticalMove = new Vector3(0, gravityComponent.VerticalVelocity, 0) * Time.deltaTime;

                playerComponent.PlayerCharacterController.Move(horizontalMove + verticalMove);
            }
        }
    }
}