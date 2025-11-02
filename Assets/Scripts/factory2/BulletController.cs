using factory2.factory;
using Scripts.hw;
using UnityEngine;

namespace factory2
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 2f;

        private IBullet _bulletData;
        private Vector3 _direction;
        private float _timer;
        
        private GameObject _player;
        private GameObject _bulletSpawner;
        private MovementController _movementController;
        private BulletComponent _bulletComponent;
        private bool _leftDirection;
        private bool _rightDirection;

        private void Awake()
        {
            _player = GameObject.Find("Player");
            _bulletSpawner = GameObject.Find("BulletSpawner");
            _movementController = _player.GetComponent<MovementController>();
            _bulletComponent = _bulletSpawner.GetComponent<BulletComponent>();
            
        }
        
        public void Initialize(BaseBullet baseBullet, Vector3 direction)
        {
            _bulletData = baseBullet;
            _direction = direction;
            _timer = lifeTime;

            // TODO Работает только для 3D
            // var _rb = GetComponent<Rigidbody>();
            // if (_rb != null)
            // {
            //     _rb.velocity = _direction * _bulletData.Velocity;
            // }
        }

        private void Update()
        {
            Move();
            
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Destroy(gameObject);
                _bulletComponent.ReturnBullet(_bulletData);
            }
        }

        private void Move()
        {

            if (_leftDirection)
            {
                transform.Translate(Vector3.left * (Time.deltaTime * _bulletData.Velocity));
                return;
            }
            
            if (_rightDirection)
            {
                transform.Translate(Vector3.right * (Time.deltaTime * _bulletData.Velocity));
                return;
            }
            
            if (_movementController.GetFlipState())
            {
                _rightDirection = true;
                transform.Translate(Vector3.right * (Time.deltaTime * _bulletData.Velocity));
            }
            else if (!_movementController.GetFlipState())
            {
                _leftDirection = true;
                transform.Translate(Vector3.left * (Time.deltaTime * _bulletData.Velocity));
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject go = collision.gameObject;
        
            if (go.tag is "Enemy" or "Ground")
            {
                Destroy(gameObject);
            }
        }
    }
}
