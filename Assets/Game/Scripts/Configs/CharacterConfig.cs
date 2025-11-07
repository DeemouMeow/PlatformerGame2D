using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "CharacterBase Config", menuName = "Configs/CharacterBase Config")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxFallSpeed;
        [SerializeField] private float _velocityLerpFactor;
        [SerializeField] private float _jumpStrength;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _fallGravityMiltiplier;
        [SerializeField] private float _airGravityMiltiplier;
        [SerializeField] private float _airTime;
        [SerializeField] private float _afterJumpGroundCheckDelay;
        [SerializeField] private float _afterJumpGravityApplyDelay;

        public float MaxSpeed => _maxSpeed;
        public float MaxFallSpeed => _maxFallSpeed;
        public float VelocityLerpFactor => _velocityLerpFactor;
        public float JumpStrength => _jumpStrength;
        public float JumpHeight => _jumpHeight;
        public float FallGravityMultiplier => _fallGravityMiltiplier;
        public float AirGravityMultiplier => _airGravityMiltiplier;
        public float AirTime => _airTime;
        public float AfterJumpGroundCheckDelay => _afterJumpGroundCheckDelay;
        public float AfterJumpGravityApplyDelay => _afterJumpGravityApplyDelay;
    }
}
