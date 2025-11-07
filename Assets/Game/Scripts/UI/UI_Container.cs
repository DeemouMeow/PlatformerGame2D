using UnityEngine;

namespace Game.UI
{
    using MenusLayer;
    using HUDLayer;
    using Character;

    public class UI_Container : MonoBehaviour
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private Menus _menus;

        private CharacterBase _character;

        public event System.Action ExitLevel
        {
            add
            {
                _menus.ExitLevel -= value;
                _menus.ExitLevel += value;
            }
            remove
            {
                _menus.ExitLevel -= value;
            }
        }

        public void Initialize(CharacterBase character)
        {
            _character = character;

            _hud.Initialize(character);
            _menus.Initialize(character);
        }

        public void Shutdown()
        {
            _menus.Shutdown();
            _hud.Shutdown();
        }
    }
}
