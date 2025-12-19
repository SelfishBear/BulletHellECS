using CodeBase.Components;
using Leopotam.Ecs;

namespace CodeBase.Systems
{
    public class PlayerHealthUIUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerHealthComponent> _playerFilter;
        private EcsFilter<PlayerHealthUIComponent> _uiFilter;

        public void Run()
        {
            if (_playerFilter.IsEmpty() || _uiFilter.IsEmpty())
                return;

            ref var health = ref _playerFilter.Get1(0);
            ref var healthUI = ref _uiFilter.Get1(0);

            if (healthUI.HealthSlider != null)
            {
                float normalizedHealth = health.PlayerCurrentHealth / health.PlayerMaxHealth;
                healthUI.HealthSlider.value = normalizedHealth;
            }
        }
    }
}

