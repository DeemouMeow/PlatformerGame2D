using UnityEngine;

namespace Game.Obstacles.Mace
{
    public class Mace : MonoBehaviour
    {
        [SerializeField] private MaceActivationTrigger _trigger;
        [SerializeField] private MaceVisual _visual;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _visual.Initialize(_animator);
            _trigger.Initialize(_visual);
        }

        public void AttackAnimationEventTrigger()
        {
            _visual.PlayIdle();
        }
    }
}
