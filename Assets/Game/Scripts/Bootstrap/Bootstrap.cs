using UnityEngine;

namespace Game
{
    using UI;
    using Character;
    using Configs;

    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CharacterBase _character;
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private SecretRoom[] _secretRooms;
        [SerializeField] private UI_Container _ui;

        private GameInput _gameInput;

        private void Awake()
        {
            Boot();
        }

        private void Boot()
        {
            _gameConfig.Initialize();

            _gameInput = new GameInput();
            _gameInput.Initialize();

            PauseableBehaviour.Initialize();

            _character.Initialize(_characterConfig);
            _character.Collision.ObstacleCollision += OnCharacterObstacleCollision;

            if (_ui is not null)
            {
                _ui.Initialize(_character);
                _ui.ExitLevel += OnLevelExit;
            }

            if (_secretRooms is not null && _secretRooms.Length != 0)
            {
                foreach (SecretRoom room in _secretRooms)
                    room.Initialize();
            }
        }

        private void Shutdown()
        {
            PauseableBehaviour.Shutdown();

            if (_ui is not null)
                _ui.Shutdown();

            _character.Shutdown();

            _gameInput.Shutdown();
            _gameInput = null;
        }

        private void OnLevelExit()
        {
            _ui.ExitLevel -= OnLevelExit;

            Shutdown();
        }

        private void OnCharacterObstacleCollision()
        {
            _character.Collision.ObstacleCollision -= OnCharacterObstacleCollision;

            _gameInput.Disable();
        }
    }
}
