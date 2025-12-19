using CodeBase.Components;
using CodeBase.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent> _filter;
        private StaticData _staticData;
        private SceneData _sceneData;
        private Vector3 _currentVelocity;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var playerComponent = ref _filter.Get1(i);

                var currentPos = _sceneData.MainCamera.transform.position;
                currentPos = Vector3.SmoothDamp(currentPos,
                    playerComponent.PlayerTransform.position + _staticData.CameraOffset, ref _currentVelocity,
                    _staticData.CameraSmoothTime);
                _sceneData.MainCamera.transform.position = currentPos;
            }
        }
    }
}