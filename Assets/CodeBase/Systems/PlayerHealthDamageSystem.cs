using CodeBase.Components;
using CodeBase.Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class PlayerHealthDamageSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerHealthComponent, EventHitComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var health = ref _filter.Get1(i);
                ref var hitEvent = ref _filter.Get2(i);
                
                EcsEntity playerEntity = _filter.GetEntity(i);

                health.PlayerCurrentHealth -= hitEvent.Damage;

                if (health.PlayerCurrentHealth <= 0f)
                {
                    health.PlayerCurrentHealth = 0f;

                    playerEntity.Get<EventDeathComponent>();
                    playerEntity.Get<PlayerDeathComponent>();
                }
            }
        }
    }
}