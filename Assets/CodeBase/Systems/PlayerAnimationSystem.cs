using CodeBase.Components;
using Leopotam.Ecs;

namespace CodeBase.Systems
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerAnimatorComponent, InputDataComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var playerAnimatorComponent = ref _filter.Get1(i);
                ref var inputDataComponent = ref _filter.Get2(i);

                float speed = inputDataComponent.MoveDirection.normalized.magnitude;

                playerAnimatorComponent.HeroAnimator.PlayRun(speed);
            }
        }
    }
}