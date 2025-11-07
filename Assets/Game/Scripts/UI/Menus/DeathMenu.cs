using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.MenusLayer
{
    using Character;
    using Utils;

    public class DeathMenu : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitMenuButton;

        private event Action _exit;

        private CharacterBase _character;

        public event Action Exit
        {
            add
            {
                _exit -= value;
                _exit += value;
            }
            remove => _exit -= value;
        }

        public void Initialize(CharacterBase character)
        {
            _character = character;

            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _exitMenuButton.onClick.AddListener(OnExitMenuButtonClicked);

            _character.Collision.ObstacleCollision += Open;
        }

        public void Shutdown()
        {
            _character.Collision.ObstacleCollision -= Open;

            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _exitMenuButton.onClick.RemoveListener(OnExitMenuButtonClicked);
        }

        public void Open() =>
            gameObject.SetActive(true);

        public void Close() => 
            gameObject.SetActive(false);

        private void OnRestartButtonClicked()
        {
            _exit?.Invoke();

            SceneLoader.RestartLevel();
        }

        private void OnExitMenuButtonClicked()
        {
            _exit?.Invoke();

            SceneLoader.LoadStartMenu();
        }
    }
}
