using System;
using UnityEngine;

namespace Game
{
    public abstract class PauseableBehaviour : MonoBehaviour
    {
        private static event Action<bool> _pauseStateChanged;

        protected static bool _isPaused;

        public static event Action<bool> PauseStateChanged
        {
            add
            {
                _pauseStateChanged -= value;
                _pauseStateChanged += value;
            }
            remove => _pauseStateChanged -= value;
        }

        public static void Initialize()
        {
            _isPaused = false;

            GameInput.Instance.Pause += OnPausePerformed;
        }

        public static void Shutdown()
        {
            GameInput.Instance.Pause -= OnPausePerformed;
        }

        protected void SimulatePausePress()
        {
            OnPausePerformed();
        }

        private static void OnPausePerformed()
        {
            _isPaused = !_isPaused;

            _pauseStateChanged?.Invoke(_isPaused);
        }
    }
}
