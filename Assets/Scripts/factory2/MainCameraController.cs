using System;
using UnityEngine;

namespace factory2
{
    public class MainCameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private float _distance;
        private Vector3 _offset;
        
        void Start () 
        {        
            _offset = transform.position;
        }
        private void LateUpdate()
        {
            _offset.x = _player.transform.position.x + _distance;
            transform.position = _offset;
         }
    }
}