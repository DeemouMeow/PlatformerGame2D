using UnityEngine;

namespace Game.UI.MenusLayer
{
    using Character;

    public class Menus : MonoBehaviour
    {
        [SerializeField] private DeathMenu _deathMenu;
        [SerializeField] private PauseMenu _pauseMenu;

        private CharacterBase _character;

        public event System.Action ExitLevel
        {
            add
            {
                _deathMenu.Exit += value;
                _pauseMenu.Exit += value;
            }
            remove
            {
                _deathMenu.Exit -= value;
                _pauseMenu.Exit -= value;
            }
        }

        public DeathMenu DeathMenu => _deathMenu;
        public PauseMenu PauseMenu => _pauseMenu;

        public void Initialize(CharacterBase character)
        {
            _character = character;

            _deathMenu.Initialize(character);
            _pauseMenu.Initialize();
        }

        public void Shutdown()
        {
            _deathMenu.Shutdown();
            _pauseMenu.Shutdown();
        }
    }
}
