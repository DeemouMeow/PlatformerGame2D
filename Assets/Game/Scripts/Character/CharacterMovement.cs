using System.Collections;
using UnityEngine;

namespace Game.Character
{
    using Configs;
    using Utils;

    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : PauseableBehaviour
    {
        [SerializeField] private Transform _foot;
        [SerializeField] private SlopeChecker _slopeChecker;

        private CharacterVisual _visual;
        private Rigidbody2D _rigidbody;
        private VelocityLerper _lerper;
        private CharacterConfig _config;

        private Vector2 _groundCheckColliderSize;
        private Vector2 _velocity;
        private Vector2 _moveDirection;
        private Vector3 _pauseSaveVelocity;

        private float _afterJumpGroundCheckDelay;
        private float _afterJumpGravityApplyDelay;
        private float _airTime;
        private float _linearMaxSpeed;
        private float _fallMaxSpeed;
        private float _fallGravityMultiplier;
        private float _jumpStrength;
        private float _jumpHeight;
        private float _velocityLerpFactor;
        private float _baseGravity;
        private float _currentGravity;
        private float _maxHeight;

        private bool _canGroundCheck;
        private bool _canApplyGravity;
        private bool _isGrounded;
        private bool _isJump;
        private bool _canJump;
        private bool _jumpMaxPointReached;
        private bool _facingRight;

        private bool IsStanding => _moveDirection.x == 0;

        private void Update()
        {
            CheckGround();
        }

        private void FixedUpdate()
        {
            _slopeChecker.CheckSlope();
            SetMaterial();
            ApplyGravity();
            ApplyVelocity();
        }

        public void Initialize(CharacterConfig config, CharacterVisual visual)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _lerper = new();
            _config = config;

            _slopeChecker.Initialize(_foot, this);

            _linearMaxSpeed = config.MaxSpeed;
            _fallMaxSpeed = config.MaxFallSpeed;
            _fallGravityMultiplier = config.FallGravityMultiplier;
            _jumpStrength = config.JumpStrength;
            _jumpHeight = config.JumpHeight;
            _afterJumpGroundCheckDelay = config.AfterJumpGroundCheckDelay;
            _afterJumpGravityApplyDelay = config.AfterJumpGravityApplyDelay;
            _airTime = config.AirTime;
            _velocityLerpFactor = config.VelocityLerpFactor;

            _facingRight = true;
            _baseGravity = -GameConfig.Instance.Gravity * GameConfig.Instance.GravityScale;
            _currentGravity = _baseGravity * _fallGravityMultiplier;

            _groundCheckColliderSize = new Vector2(1, 0.035f);

            _canGroundCheck = true;
            _canApplyGravity = true;

            _visual = visual;

            GameInput.Instance.Move += OnMove;
            GameInput.Instance.Jump += OnJump;

            PauseStateChanged += OnPauseStateChanged;
        }

        public new void Shutdown()
        {
            GameInput.Instance.Move -= OnMove;
            GameInput.Instance.Jump -= OnJump;

            PauseStateChanged -= OnPauseStateChanged;

            _rigidbody = null;
        }

        private bool CheckGround()
        {
            if (!_canGroundCheck)
                return _isGrounded;

            _isGrounded = Physics2D.OverlapCapsuleNonAlloc(_foot.position, _groundCheckColliderSize, CapsuleDirection2D.Horizontal, 0f, new Collider2D[10], GameConfig.Instance.GroundMask) != 0;

            return _isGrounded;
        }

        private void ApplyVelocity()
        {
            if (_isPaused)
                return;

            _velocity.x = _moveDirection.x * (_linearMaxSpeed * Time.fixedDeltaTime);

            if (_isGrounded && _slopeChecker.IsOnSlope)
            {
                _velocity.x = _moveDirection.x * -_slopeChecker.SlopeNormalPerpendicular.x * (_linearMaxSpeed * Time.fixedDeltaTime);
                _velocity.y = _moveDirection.x * -_slopeChecker.SlopeNormalPerpendicular.y * (_linearMaxSpeed * Time.fixedDeltaTime);
            }

            if (_isJump && _canJump)
            {
                _canJump = false;

                _velocity.y = _jumpStrength;

                StartCoroutine(WaitUntilJumpMaxPointReached());
            }

            Vector2 currentVelocity = _rigidbody.velocity;
            Vector2 targetVelocity = _lerper.Lerp(currentVelocity, _velocity, _velocityLerpFactor);

            _rigidbody.velocity = targetVelocity;
        }

        private void ApplyGravity()
        {
            if (_isGrounded)
            {
                _velocity.y = 0f;
                _isJump = false;
                _canJump = true;
                _jumpMaxPointReached = false;
                _rigidbody.gravityScale = GameConfig.Instance.GravityScale;
                _currentGravity = _baseGravity * _fallGravityMultiplier;

                return;
            }

            if (!_canApplyGravity)
                return;

            if (_jumpMaxPointReached)
            {
                _jumpMaxPointReached = false;
                _isJump = false;

                _velocity.y = 0;

                StartCoroutine(SetJumpAirGravity());
            }

            _velocity.y += _currentGravity * Time.fixedDeltaTime;
            _velocity.y = _velocity.y < -_fallMaxSpeed ? -_fallMaxSpeed : _velocity.y;
        }

        private void Flip()
        {
            if (_moveDirection == Vector2.zero)
                return;

            if ((_moveDirection == Vector2.right && !_facingRight) || (_moveDirection == Vector2.left && _facingRight))
            {
                _facingRight = !_facingRight;

                transform.Rotate(Vector3.up, 180);
            }
        }

        private void SetMaterial()
        {
            if (_slopeChecker.IsOnSlope && IsStanding && _slopeChecker.CanWalkOnSlope)
                _rigidbody.sharedMaterial = _slopeChecker.FullFrictionMaterial;
            else
                _rigidbody.sharedMaterial = _slopeChecker.NoFrictionMaterial;
        }

        private void OnMove(Vector2 direction)
        {
            _moveDirection = direction;

            if (_isPaused)
                return;

            if (_moveDirection != Vector2.zero)
                _visual.PlayRun();
            else
                _visual.PlayIdle();

            Flip();
        }

        private void OnJump()
        {
            if (!_isGrounded || !_canJump || _isPaused)
                return;

            _isJump = true;
            _isGrounded = false;

            _maxHeight = transform.position.y + _jumpHeight;

            StartCoroutine(WaitJumpGravityApllyDelay());
            StartCoroutine(WaitGroundCheckDelay());
        }

        private void OnPauseStateChanged(bool state)
        {
            enabled = !state;

            if (enabled)
            {
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;

                OnMove(_moveDirection);
            }
            else
                _rigidbody.bodyType = RigidbodyType2D.Static;
        } 

        private IEnumerator WaitGroundCheckDelay()
        {
            _canGroundCheck = false;

            yield return new WaitForSeconds(_afterJumpGroundCheckDelay);

            _canGroundCheck = true;
        }

        private IEnumerator WaitJumpGravityApllyDelay()
        {
            _canApplyGravity = false;

            yield return new WaitForSeconds(_afterJumpGravityApplyDelay);

            _canApplyGravity = true;
        }

        private IEnumerator WaitUntilJumpMaxPointReached()
        {
            _jumpMaxPointReached = false;

            yield return new WaitUntil(() => _velocity.y < 0 || transform.position.y >= _maxHeight);

            _jumpMaxPointReached = true;
        }

        private IEnumerator SetJumpAirGravity()
        {
            _currentGravity = _baseGravity * _fallGravityMultiplier * _config.AirGravityMultiplier;

            yield return new WaitForSeconds(_airTime);

            _currentGravity = _baseGravity * _fallGravityMultiplier;
        }
    }
}