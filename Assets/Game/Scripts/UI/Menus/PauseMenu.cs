using UnityEngine;

namespace Game.UI.MenusLayer
{
    using UnityEngine.UI;
    using Utils;

    public class PauseMenu : PauseableBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;

        private event System.Action _exit;

        public event System.Action Exit
        {
            add
            {
                _exit -= value;
                _exit += value;
            }
            remove => _exit -= value;
        }

        public new void Initialize()
        {
            PauseStateChanged += OnPauseStateChanged;

            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        public new void Shutdown()
        {
            PauseStateChanged -= OnPauseStateChanged;

            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);

            Close();
        }

        public void Open() =>
            gameObject.SetActive(true);

        public void Close() =>
            gameObject.SetActive(false);

        private void OnResumeButtonClicked()
        {
            if (_isPaused)
                SimulatePausePress();
        }

        private void OnExitButtonClicked()
        {
            _exit?.Invoke();

            SceneLoader.LoadStartMenu();
        }

        private void OnPauseStateChanged(bool state)
        {
            print("Pause Pause State Changed! " + state);

            if (state)
                Open();
            else
                Close();
        }
    }
}
