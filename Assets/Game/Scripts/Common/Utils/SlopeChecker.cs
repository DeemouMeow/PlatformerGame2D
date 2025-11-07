using UnityEngine;

namespace Game.Utils
{
    using Character;
    using Configs;

    [System.Serializable]
    public class SlopeChecker
    {
        [SerializeField] private Transform _checkTransform;
        [SerializeField] private PhysicsMaterial2D _noFrictionMaterial;
        [SerializeField] private PhysicsMaterial2D _fullFrictionMaterial;
        [SerializeField] private float _checkDistance;
        [SerializeField] private float _maxSlopeAngle;

        private CharacterMovement _movement;

        private LayerMask _groundMask;
        private Vector2 _checkPosition;
        private Vector2 _slopeNormalPerpendicular;

        private float _slopeDownAngle;
        private float _slopeSideAngle;
        private bool _isOnSlope;
        private bool _canWalkOnSlope;

        public PhysicsMaterial2D NoFrictionMaterial => _noFrictionMaterial;
        public PhysicsMaterial2D FullFrictionMaterial => _fullFrictionMaterial;
        public Vector2 SlopeNormalPerpendicular => _slopeNormalPerpendicular;
        public bool IsOnSlope => _isOnSlope;
        public bool CanWalkOnSlope => _canWalkOnSlope;

        public void Initialize(Transform checkTransform, CharacterMovement movement)
        {
            _checkTransform ??= checkTransform;
            _movement = movement;
            _groundMask = GameConfig.Instance.GroundMask;
        }

        public (bool Vertical, bool Horizontal) CheckSlope() 
        {
            _checkPosition = _checkTransform.position;

            bool isVerticalSlope = CheckSlopeVertical(_checkPosition);
            bool isHorizontalSlope = CheckSlopeHorizontal(_checkPosition);

            _isOnSlope = isVerticalSlope || isHorizontalSlope;
            _canWalkOnSlope = isVerticalSlope && _slopeDownAngle <= _maxSlopeAngle;

            return (Vertical: isVerticalSlope, Horizontal: isHorizontalSlope);
        }

        private bool CheckSlopeVertical(Vector2 checkPosition) 
        {
            RaycastHit2D hit = Physics2D.Raycast(checkPosition, Vector2.down, _checkDistance, _groundMask);

            bool isVerticalSlope = false;

            if (hit)
            {
                _slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
                _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                Debug.DrawRay(hit.point, hit.normal, Color.green);
                Debug.DrawRay(hit.point, _slopeNormalPerpendicular, Color.red);

                isVerticalSlope = _slopeDownAngle != 0f;
            }

            return isVerticalSlope;
        }

        private bool CheckSlopeHorizontal(Vector2 checkPosition)
        {
            RaycastHit2D forwardRayCast = Physics2D.Raycast(checkPosition, _movement.transform.right, _checkDistance, _groundMask);
            RaycastHit2D backwardRayCast = Physics2D.Raycast(checkPosition, -_movement.transform.right, _checkDistance, _groundMask);

            Vector2 surfaceNormal = forwardRayCast ? forwardRayCast.normal : backwardRayCast ? backwardRayCast.normal : Vector2.zero;

            bool isHorizontalSlope = forwardRayCast || backwardRayCast;
            _slopeSideAngle = Vector2.Angle(surfaceNormal.normalized, Vector2.up);

            return isHorizontalSlope;
        }
    }
}
