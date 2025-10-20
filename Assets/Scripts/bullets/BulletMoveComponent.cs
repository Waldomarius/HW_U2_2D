using System;
using UnityEngine;

namespace bullets
{
    public class BulletMoveComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 7;

        private void Update()
        {
            transform.Translate(Vector3.right * (Time.deltaTime * _speed));
        }
    }
}