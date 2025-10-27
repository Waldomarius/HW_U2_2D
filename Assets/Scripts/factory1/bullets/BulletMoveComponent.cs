using Scripts.hw;
using UnityEngine;

namespace bullets
{
    public class BulletMoveComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 7;
        
        private GameObject _player;
        private MovementController _movementController;
        private bool _leftDirection;
        private bool _rightDirection;

        private void Awake()
        {
            _player = GameObject.Find("Player");
            _movementController = _player.GetComponent<MovementController>();
        }

        private void Update()
        {

            if (_leftDirection)
            {
                transform.Translate(Vector3.left * (Time.deltaTime * _speed));
                return;
            }
            
            if (_rightDirection)
            {
                transform.Translate(Vector3.right * (Time.deltaTime * _speed));
                return;
            }
            
            if (_movementController.GetFlipState())
            {
                _rightDirection = true;
                transform.Translate(Vector3.right * (Time.deltaTime * _speed));
            }
            else if (!_movementController.GetFlipState())
            {
                _leftDirection = true;
                transform.Translate(Vector3.left * (Time.deltaTime * _speed));
            }
        }

        public void ResetDirection()
        {
            _leftDirection = false;
            _rightDirection = false; 
        }
    }
}