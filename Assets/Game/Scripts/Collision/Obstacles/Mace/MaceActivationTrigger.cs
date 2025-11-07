using UnityEngine;

namespace Game.Obstacles.Mace
{
    using Character;

    public class MaceActivationTrigger : MonoBehaviour
    {
        private MaceVisual _visual;

        public void Initialize(MaceVisual visual)
        {
            _visual = visual;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterBase character))
            {
                _visual.PlayAttack();
            }
        }
    }
}
