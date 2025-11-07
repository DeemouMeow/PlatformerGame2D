using UnityEngine;

namespace Game.Obstacles.Mace
{
    public class MaceVisual : MonoBehaviour
    {
        private const string k_idleKey = "mace_idle";
        private const string k_attackKey = "mace_attack";

        private Animator _animator;

        private bool _canPlayAttack;

        public void Initialize(Animator animator)
        {
            _animator = animator;

            PlayIdle();
        }

        public void PlayAttack()
        {
            if (!_canPlayAttack)
                return;

            _animator.Play(k_attackKey);

            _canPlayAttack = false;
        }

        public void PlayIdle()
        {
            _animator.Play(k_idleKey);

            _canPlayAttack = true;
        }
    }
}
