using CodeBase.Components;
using CodeBase.Components.Events;
using CodeBase.Infrastructure.Services.Input;
using Leopotam.Ecs;

namespace CodeBase.Systems
{
    public class PlayerDeathSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerAnimatorComponent, EventDeathComponent> _filter;
        private IInputService _inputService;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var animatorComponent = ref _filter.Get1(i);
                animatorComponent.HeroAnimator.PlayDeath();
                _inputService.DisableAllActionMaps();
            }
        }
    }
}