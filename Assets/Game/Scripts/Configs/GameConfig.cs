using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject
    {
        private static GameConfig _instance;

        [SerializeField] private float _gravity;
        [SerializeField] private float _gravityScale;
        [SerializeField] private LayerMask _groundMask;

        public static GameConfig Instance => _instance;

        public float Gravity => _gravity;
        public float GravityScale => _gravityScale;
        public LayerMask GroundMask => _groundMask;

        private void Awake()
        {
            if (_instance is null)
                _instance = this;
        }

        public void Initialize()
        {
            Awake();
        }
    }
}
