using UnityEngine;

namespace Game.Collision.Triggers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Coin : MonoBehaviour
    {
        private const float k_upForce = 200f;

        private Rigidbody2D _rigidbody;

        private bool _coinPocked;

        public bool CoinPicked => _coinPocked;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _rigidbody.bodyType = RigidbodyType2D.Static;

            _coinPocked = false;
        }

        public void ApplyUpForce()
        {
            _rigidbody.AddForce(Vector2.up * k_upForce);
        }

        public void MakeDynamic()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void Pick()
        {
            _coinPocked = true;

            Destroy(gameObject);
        }
    }
}
