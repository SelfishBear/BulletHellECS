using UnityEngine;

namespace CodeBase.Animation
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Death = Animator.StringToHash("Death");
        
        public void PlayDeath()
        {
            _animator.SetTrigger(Death);
        }
    }
}