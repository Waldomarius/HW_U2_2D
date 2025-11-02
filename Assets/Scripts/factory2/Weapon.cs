using factory2;
using UnityEngine;

namespace DefaultNamespace
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private string _iBulletType = "Pistol";
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _fireRate;
        
        [SerializeField] private BulletComponent _bulletComponent;
        
        private float _nextFire;

        private void Start()
        {
            _bulletComponent.Initialize();
        }

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time > _nextFire)
            {
                Shoot();
                _nextFire = Time.time + _fireRate;
            }
        }

        private void Shoot()
        {
            _bulletComponent.ShootBullet(_iBulletType, _firePoint.position, _firePoint.forward);
        }
    }
}   