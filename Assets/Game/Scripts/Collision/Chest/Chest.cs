using System.Collections;
using UnityEngine;

namespace Game.Interactable
{
    using Character;
    using Collision.Triggers;

    public class Chest : MonoBehaviour
    {
        [SerializeField] private ChestVisual _visual;
        [SerializeField] private Transform _coinsSpawnPoint;
        [SerializeField] private Coin _prefab;
        [SerializeField] private int _coinsCount;
        [SerializeField] private float _coinsSpawnRate;

        private bool _isOpened;

        private void Awake()
        {
            _visual.Initialize();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isOpened)
                return;

            if (collision.gameObject.TryGetComponent(out CharacterBase character))
            {
                GameInput.Instance.Interact += OnInteractionPressed;

                _visual.Highlight();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (_isOpened)
                return;

            if (collision.gameObject.TryGetComponent(out CharacterBase character))
            {
                GameInput.Instance.Interact -= OnInteractionPressed;

                _visual.SetDefault();
            }
        }

        private void OnInteractionPressed()
        {
            GameInput.Instance.Interact -= OnInteractionPressed;

            _visual.PlayOpen();
            _visual.SetDefault();

            _isOpened = true;

            StartCoroutine(SpawnCoins());
        }

        private IEnumerator SpawnCoins()
        {
            for (int i = 0; i < _coinsCount; i++)
            {
                Coin coin = Instantiate(_prefab, _coinsSpawnPoint.position, Quaternion.identity);

                coin.MakeDynamic();
                coin.ApplyUpForce();

                yield return new WaitForSeconds(_coinsSpawnRate);
            }
        }
    }
}
