using UnityEngine;

namespace Game.Character
{
    using Configs;

    public class CharacterBase : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private CharacterVisual _visual;
        [SerializeField] private CharacterCollision _collision;

        public CharacterCollision Collision => _collision;

        public void Initialize(CharacterConfig config)
        {
            _visual.Initialize();

            _movement ??= GetComponent<CharacterMovement>() ?? gameObject.AddComponent<CharacterMovement>();
            _movement.Initialize(config, _visual);

            _collision.Initialize();
            _collision.ObstacleCollision += OnObstacleCollision;
        }

        public void Shutdown()
        {
            _movement.Shutdown();
            _visual.Shutdown();

            _collision.ObstacleCollision -= OnObstacleCollision;
        }

        private void OnObstacleCollision()
        {
            Destroy(gameObject);
        }
    }
}
