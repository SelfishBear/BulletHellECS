using CodeBase.Components.Events;
using CodeBase.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.MonoBehaviours
{
    [RequireComponent(typeof(Collider))]
    public class EnemyCollisionHandler : MonoBehaviour
    {
        [SerializeField] private float _damage = 10f;
        
        private RuntimeData _runtimeData;

        public void Initialize(RuntimeData runtimeData)
        {
            _runtimeData = runtimeData;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_runtimeData == null)
            {
                Debug.LogError("EnemyCollisionHandler: RuntimeData is null!");
                return;
            }

            if (other.CompareTag("Player"))
            {
                if (!_runtimeData.PlayerEntity.IsAlive())
                {
                    Debug.LogError("EnemyCollisionHandler: PlayerEntity is not alive!");
                    return;
                }

                ref var hitEvent = ref _runtimeData.PlayerEntity.Get<EventHitComponent>();
                
                
                hitEvent.Damage = _damage;
                
                Debug.Log($"Enemy hit player! Adding damage event: {_damage}");
            }
        }
    }
}