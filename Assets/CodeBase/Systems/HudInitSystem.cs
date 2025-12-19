using CodeBase.Components;
using CodeBase.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class HudInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SceneData _sceneData;

        public void Init()
        {
            if (_sceneData.HudPrefab == null)
            {
                Debug.LogError("HudPrefab is not assigned in SceneData!");
                return;
            }

            GameObject hudInstance = Object.Instantiate(_sceneData.HudPrefab);
            HudSettings hudSettings = hudInstance.GetComponent<HudSettings>();

            if (hudSettings == null)
            {
                Debug.LogError("HudSettings component not found on HudPrefab!");
                return;
            }

            EcsEntity hudEntity = _world.NewEntity();
            ref var healthUI = ref hudEntity.Get<PlayerHealthUIComponent>();
            
            healthUI.HealthSlider = hudSettings.HealthSlider;

            if (healthUI.HealthSlider != null)
            {
                healthUI.HealthSlider.minValue = 0f;
                healthUI.HealthSlider.maxValue = 1f;
                healthUI.HealthSlider.value = 1f;
            }

            Debug.Log("HUD initialized successfully");
        }
    }
}

