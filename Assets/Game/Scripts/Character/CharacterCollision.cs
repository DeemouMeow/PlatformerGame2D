using UnityEngine;
using System;

namespace Game.Character
{
    using Collision.Triggers;

    public class CharacterCollision : MonoBehaviour
    {
        private event Action _coinPicked;
        private event Action _obstacleCollision;

        public event Action CoinPicked
        {
            add
            {
                _coinPicked -= value;
                _coinPicked += value;
            }
            remove => _coinPicked -= value;
        }

        public event Action ObstacleCollision
        {
            add
            {
                _obstacleCollision -= value;
                _obstacleCollision += value;
            }
            remove => _obstacleCollision -= value;
        }

        public void Initialize()
        {
            enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Coin coin))
            {
                if (!coin.CoinPicked)
                {
                    coin.Pick();

                    _coinPicked?.Invoke();
                }
            }

            if (collision.gameObject.TryGetComponent(out DestructiveObstacle destructive))
            {
                _obstacleCollision?.Invoke();
            }
        }
    }
}
