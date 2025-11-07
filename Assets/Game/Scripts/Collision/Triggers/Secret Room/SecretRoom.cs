using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    using Character;

    [RequireComponent(typeof(Tilemap))]
    public class SecretRoom : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float _triggerEnterAlpha;
        [SerializeField, Range(0f, 1f)] private float _appearIntencity;

        private Tilemap _tilemap;

        private float _lerpFactor;
        private float _currentAlpha;
        private float _targetAlpha;
        private float _baseAlpha;
        private bool _canLerp;

        public void Initialize()
        {
            _tilemap = GetComponent<Tilemap>();

            _lerpFactor = Time.deltaTime * _appearIntencity;
            _baseAlpha = _tilemap.color.a;
            _currentAlpha = _baseAlpha;

            _canLerp = false;
        }

        private void Update()
        {
            LerpAlpha();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterBase character))
            {
                _targetAlpha = _triggerEnterAlpha;

                print("Target alpha: " + _targetAlpha);

                _canLerp = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterBase character))
            {
                _targetAlpha = _baseAlpha;

                _canLerp = true;
            }
        }

        private void LerpAlpha()
        {
            if (!_canLerp || _currentAlpha == _targetAlpha)
                return;

            Color newColor = _tilemap.color;

            if (Mathf.Abs(_targetAlpha - _currentAlpha) <= 0.01)
            {
                _currentAlpha = _targetAlpha;

                _canLerp = false;
            }

            _currentAlpha = Mathf.Lerp(_currentAlpha, _targetAlpha, _lerpFactor);

            newColor.a = _currentAlpha;

            _tilemap.color = newColor;
        }
    }
}
