using UnityEngine;

namespace Game.Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterVisual : PauseableBehaviour
    {
        private const string k_idleKey = "Idle";
        private const string k_runKey = "run";

        private Animator _animator;

        public new void Initialize()
        {
            _animator = GetComponent<Animator>();

            PauseStateChanged += OnPauseStateChanged;
        }

        public new void Shutdown() 
        {
            PauseStateChanged -= OnPauseStateChanged;
        }

        public void PlayIdle()
        {
            _animator.Play(k_idleKey);
        }

        public void PlayRun()
        {
            _animator.Play(k_runKey);
        }
        
        private void OnPauseStateChanged(bool state)
        {
            _animator.speed = state ? 0 : 1;

            enabled = !state;
        }
    }
}
