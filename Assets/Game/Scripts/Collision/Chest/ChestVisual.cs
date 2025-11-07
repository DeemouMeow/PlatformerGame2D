using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class ChestVisual : MonoBehaviour
    {
        private const string k_openAnimKey = "chest_open";

        [SerializeField] private Color _highlightColor;

        private Animator _animator;
        private SpriteRenderer _renderer;
        private Color _baseColor;

        public void Initialize()
        {
            _animator = GetComponent<Animator>();
            _renderer = GetComponent<SpriteRenderer>();

            _baseColor = _renderer.color;
        }

        public void PlayOpen()
        {
            _animator.Play(k_openAnimKey);
        }

        public void Highlight() 
        {
            _renderer.color = _highlightColor;
        }

        public void SetDefault()
        {
            _renderer.color = _baseColor;
        }
    }
}
