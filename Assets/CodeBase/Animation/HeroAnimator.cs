using UnityEngine;

namespace CodeBase.Animation
{
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Death = Animator.StringToHash("Death");

        public void PlayRun(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void PlayJump()
        {
            _animator.SetTrigger(Jump);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(Death);
        }
    }
}