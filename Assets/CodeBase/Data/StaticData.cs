using UnityEngine;

namespace CodeBase.Data
{
    [CreateAssetMenu(fileName = "StaticData", menuName = "Data/StaticData", order = 0)]
    public class StaticData : ScriptableObject
    {
        [Header("PlayerSettings")]
        public GameObject PlayerPrefab;
        public float PlayerMoveSpeed;
        public float Gravity = -9.81f;
        
        [Header("CameraSettings")]
        public float CameraSmoothTime;
        public Vector3 CameraOffset;
    }
}