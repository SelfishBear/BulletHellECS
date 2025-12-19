using CodeBase.Animation;
using UnityEngine;

namespace CodeBase.Components
{
    public struct PlayerComponent
    {
        public Transform PlayerTransform;
        public CharacterController PlayerCharacterController;
        public float MoveSpeed;
    }
}