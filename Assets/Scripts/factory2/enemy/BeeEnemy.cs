using factory2.factory;
using UnityEngine;

namespace factory2.enemy
{
    public class BeeEnemy : BaseCharacter
    {
        [Header("Movement Setting")] 
        [SerializeField] private MovementType _movementType = MovementType.Horizontal;
        [SerializeField] private float _speed;
        [SerializeField] private float _amplitude;
        [SerializeField] private float _frequency;
        [SerializeField] private float _distanceX;
        [SerializeField] private float _distanceY;
        
        private Vector2 _spawnPosition;
        private bool _isMovingRight = true;
        private bool _isMovingUp = true;
        
        private float _startY;
        private Vector2 _movementDirection;

        private void Update()
        {
            Move();
        }

        public override void SetMovement(CharacterFactory.SpawnPointData spawnPointData)
        {
            _movementType =  spawnPointData.movementType;
            _speed  = spawnPointData.moveSpeed;
            _amplitude  = spawnPointData.amplitude;
            _frequency  = spawnPointData.frequency;
            _distanceX = spawnPointData.distanceX;
            _distanceY = spawnPointData.distanceY;
            _spawnPosition = spawnPointData.position;
            
            InitializeMovement();
        }
        
        private void InitializeMovement()
        {
            switch (_movementType)
            {
                case MovementType.Horizontal:
                    _movementDirection = Vector2.right;
                    break;
                case MovementType.Vertical:
                    _movementDirection = Vector2.up;
                    break;
                case MovementType.Sinusoidal:
                    _movementDirection = Vector2.right;
                    break;
                case MovementType.Circular:
                    _movementDirection = Vector2.up;
                    break;
                case MovementType.None: 
                    _movementDirection = Vector2.zero; 
                    break;
            }
        }

        private void Move()
        {
            switch (_movementType)
            {
                case MovementType.Horizontal:
                    HorizontalMovement();
                    break;
                case MovementType.Vertical:
                    VerticalMovement();
                    break;
                case MovementType.Sinusoidal:
                    SinusoidalMovement();
                    break;
                case MovementType.Circular:
                    CircularMovement();
                    break;
            }
        }


        private void HorizontalMovement()
        {
            Vector2 tempPos = MovementHorizontalX(transform.position);
            transform.position = tempPos;
        }
        
        
        private void VerticalMovement()
        {
            Vector2 tempPos = MovementVerticalY(transform.position);
            transform.position = tempPos;
        }
        

        private void SinusoidalMovement()
        {
            Vector2 tempPos = MovementHorizontalX(transform.position);
            tempPos.y = Mathf.Sin(transform.position.x * _frequency);
            transform.position = tempPos;
        }

        private Vector2 MovementHorizontalX(Vector2 tempPos)
        {
            float posX = transform.position.x;

            if (_isMovingRight)
            {
                tempPos.x += _speed * Time.deltaTime;
            }
            else
            {
                tempPos.x -= _speed * Time.deltaTime;
            }
            
            
            if (posX <= _spawnPosition.x - _distanceX)
            {
                _isMovingRight = true;
            }
            else if (posX >= _spawnPosition.x + _distanceX)
            {
                _isMovingRight = false;
            }

            return tempPos;
        }
        
        private Vector2 MovementVerticalY(Vector2 tempPos)
        {
            float posY = transform.position.y;

            if (_isMovingUp)
            {
                tempPos.y += _speed * Time.deltaTime;
            }
            else
            {
                tempPos.y -= _speed * Time.deltaTime;
            }
            
            
            if (posY <= _spawnPosition.y - _distanceY)
            {
                _isMovingUp = true;
            }
            else if (posY >= _spawnPosition.y + _distanceY)
            {
                _isMovingUp = false;
            }

            return tempPos;
        }

        private void CircularMovement()
        {
            float posX = MovementCircularHorizontalX(transform.position).x;
            float posY = MovementCircularVerticalY(transform.position).y;
            transform.position = new Vector2(posX, posY);
        }

        private Vector2 MovementCircularHorizontalX(Vector2 tempPos)
        {
            float posX = transform.position.x;

            if (_isMovingRight)
            {
                tempPos.x += _amplitude * Mathf.Cos(Time.time * _frequency) * Time.deltaTime;
            }
            else
            {
                tempPos.x -= _amplitude * Mathf.Cos(Time.time * _frequency) * Time.deltaTime;
            }
            
            
            if (posX <= _spawnPosition.x - _distanceX)
            {
                _isMovingRight = true;
            }
            else if (posX >= _spawnPosition.x + _distanceX)
            {
                _isMovingRight = false;
            }

            return tempPos;
        }
        
        private Vector2 MovementCircularVerticalY(Vector2 tempPos)
        {
            float posY = transform.position.y;

            if (_isMovingUp)
            {
                tempPos.y += _amplitude * Mathf.Sin(Time.time * _frequency) * Time.deltaTime;
            }
            else
            {
                tempPos.y -= _amplitude * Mathf.Sin(Time.time * _frequency) * Time.deltaTime;
            }
            
            
            if (posY <= _spawnPosition.y - _distanceY)
            {
                _isMovingUp = true;
            }
            else if (posY >= _spawnPosition.y + _distanceY)
            {
                _isMovingUp = false;
            }

            return tempPos;
        }

        public override void Attack()
        {
            
        }

    }
}