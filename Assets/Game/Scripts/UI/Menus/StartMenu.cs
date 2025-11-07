using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MenusLayer
{
    using Utils;

    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitGameButton;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(OnStartButtonClicked);
            _exitGameButton.onClick.AddListener(OnExitButtonClicked);

        }

        public void Shutdown()
        {
            _startGameButton.onClick.RemoveListener(OnStartButtonClicked);
            _exitGameButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            SceneLoader.LoadNextLevel();
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}
