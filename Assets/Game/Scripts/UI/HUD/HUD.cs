using UnityEngine;

namespace Game.UI.HUDLayer
{
    using Character;

    public class HUD : MonoBehaviour
    {
        [SerializeField] private CoinsView _coinsView;

        private CharacterBase _character;

        public void Initialize(CharacterBase character)
        {
            _coinsView.Initialize();

            _character = character;

            _character.Collision.CoinPicked += AddCoin;
        }

        public void Shutdown()
        {
            _character.Collision.CoinPicked -= AddCoin;

            _coinsView.Shutdown();
        }

        private void AddCoin() => _coinsView.UpdateValue(_coinsView.CurrentCoins + 1);
    }
}
