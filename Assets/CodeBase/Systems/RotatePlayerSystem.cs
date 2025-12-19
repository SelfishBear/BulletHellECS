using CodeBase.Components;
using CodeBase.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace CodeBase.Systems
{
    public class RotatePlayerSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerComponent> _filter;
        private SceneData _sceneData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var playerComponent = ref _filter.Get1(i);

                Ray ray = _sceneData.MainCamera.ScreenPointToRay(Input.mousePosition);

                Plane plane = new Plane(Vector3.up, playerComponent.PlayerTransform.position);

                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 targetPoint = ray.GetPoint(distance);

                    Vector3 direction = targetPoint - playerComponent.PlayerTransform.position;
                    direction.y = 0;

                    if (direction != Vector3.zero)
                    {
                        playerComponent.PlayerTransform.rotation = Quaternion.LookRotation(direction);
                    }
                }
            }
        }
    }
}