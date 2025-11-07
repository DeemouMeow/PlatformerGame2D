using UnityEngine;

namespace Game
{
    public class Saw : DestructiveObstacle
    {
        [SerializeField] private float _degreesPerSeconds;

        private const float k_maxRotation = 360f;

        private float _currentRotation;

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            _currentRotation += _degreesPerSeconds * Time.deltaTime;
            _currentRotation %= k_maxRotation;

            Quaternion newRotation = Quaternion.Euler(0f, 0f, _currentRotation);

            transform.rotation = newRotation;
        }
    }
}
