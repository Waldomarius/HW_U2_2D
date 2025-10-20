using bullets;
using UnityEngine;

namespace Scripts.hw
{
    public class MovementController : MonoBehaviour
    {
        // public Action<bool> OnBullutsSpawn; 
        
        [Header("Movement")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _jumpForce = 5f;
        private Vector2 _movement;
        private GroundChecker _groundChecker;
        
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _sprite;
        private BulletsSpawner _bulletsSpawner;
        
        private bool _isGrounded; 
        private bool _isFacingRight = true;
        
        private Animator _animator;
        public bool freezeMove;

        private void OnEnable()
        {
            // Подписка на слушателя
            _groundChecker.OnGroundStateChange += HandleGroundStateChanged;
        }

        private void HandleGroundStateChanged(bool grounded)
        {
            _isGrounded = grounded;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite  = GetComponentInChildren<SpriteRenderer>();
            _groundChecker = GetComponentInChildren<GroundChecker>();
            _animator = GetComponent<Animator>();
            _bulletsSpawner = GetComponentInChildren<BulletsSpawner>();
        }

        private void Update()
        {
            _movement.x = freezeMove ? 0 : Input.GetAxis("Horizontal");
            _rigidbody.velocity = new Vector2(_movement.x * _speed, _rigidbody.velocity.y);

            _animator.SetBool("Jump", !_isGrounded);
            _animator.SetFloat("Move", Mathf.Abs(_movement.x));
        
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _animator.SetBool("Jump", true);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            }
            
            if (_movement.x > 0 && !_isFacingRight || _movement.x < 0 && _isFacingRight)
            {
                Flip();
            }
            
            if (Input.GetButtonDown("Fire1"))
            {
                _bulletsSpawner.SpawnBullet(transform.position);
            }
        }

        private void Flip()
        {
            _sprite.flipX = _isFacingRight;
            _isFacingRight = !_isFacingRight;
        }

        private void OnDisable()
        {
            // Отписка от слушателя
            _groundChecker.OnGroundStateChange -= HandleGroundStateChanged;
        }
    }
}