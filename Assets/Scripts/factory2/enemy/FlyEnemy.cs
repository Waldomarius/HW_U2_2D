using System;
using factory2.factory;
using UnityEngine;

namespace factory2.enemy
{
    public class FlyEnemy : BaseCharacter
    {
        [Header("Movement Setting")] 
        [SerializeField] private MovementType _movementType = MovementType.Horizontal;
        [SerializeField] private float _speed = 2.0f;
        [SerializeField] private float _amplitude = 1.0f;
        [SerializeField] private float _frequency = 2.0f;

        private float _startY;
        private Vector2 _movementDirection;

        protected override void Awake()
        {
            base.Awake();
            _startY = transform.position.y;
            InitializeMovement();
        }

        private void Update()
        {
            Move();
        }

        // public override void SetMovement(MovementType newMovementType, float newSpeed, float newAmplitude, float newFrequency)
        // {
        //     _movementType =  newMovementType;
        //     _speed  = newSpeed;
        //     _amplitude  = newAmplitude;
        //     _frequency  = newFrequency;
        //     
        //     InitializeMovement();
        // }
        
        protected void InitializeMovement()
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
                case MovementType.None: 
                    _movementDirection = Vector2.zero; 
                    break;
            }
        }

        protected void Move()
        {
            switch (_movementType)
            {
                case MovementType.Horizontal:
                    transform.Translate(_movementDirection * (_speed * Time.deltaTime));
                    break;
                case MovementType.Vertical:
                    transform.Translate(_movementDirection * (_speed * Time.deltaTime));
                    break;
                case MovementType.Sinusoidal:
                    SinusoidalMovement();
                    break;
            }
        }

        private void SinusoidalMovement()
        {
            Vector2 tempPos = transform.position;
            tempPos.x -= _speed * Time.deltaTime;
            tempPos.y = Mathf.Sin(transform.position.x * _frequency);
            
            // float newX = transform.position.x + _speed * Time.time;
            // float newY = (transform.position.x + _amplitude * Mathf.Sin(Time.time * _frequency));
            // transform.position = new Vector2(newX, newY);
            
            transform.position = tempPos;
        }

        private void CircularMovement()
        {
            float newX = transform.position.x + _amplitude * Mathf.Cos(Time.time * _frequency) * Time.time;
            float newY = transform.position.x + _amplitude * Mathf.Sin(Time.time * _frequency) * Time.time;
            transform.position = new Vector2(newX, newY);
        }


        public override void Attack()
        {
            
        }
        
    }
}