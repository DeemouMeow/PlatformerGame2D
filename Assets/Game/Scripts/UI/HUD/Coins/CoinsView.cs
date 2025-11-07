using UnityEngine;
using TMPro;

namespace Game.UI.HUDLayer
{
    public class CoinsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;

        private int _currentCoins;

        public int CurrentCoins => _currentCoins;

        public void Initialize()
        {
            _currentCoins = 0;
            UpdateValue(_currentCoins);
        }

        public void Shutdown()
        {

        }

        public void UpdateValue(int newValue)
        {
            if (newValue < 0)
                newValue = 0;

            _countText.text = newValue.ToString();
            _currentCoins = newValue;
        }
    }
}
