using CodeBase.Components;
using CodeBase.Infrastructure.Services.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsFilter<InputDataComponent> _filter;
        private IInputService _inputService;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var inputDataComponent = ref _filter.Get1(i);
                
                Vector2 moveInput = _inputService.MoveDirection;

                inputDataComponent.MoveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
            }
        }
    }
}