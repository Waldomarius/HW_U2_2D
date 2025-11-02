using factory2.factory;
using factory2.pool;
using UnityEngine;

namespace factory2
{
    public class BulletComponent : MonoBehaviour
    {
        private IBulletFactory _bulletFactory;
        private IBulletPool _bulletPool;
        
        public IBullet GetBullet(string bulletType) => _bulletPool.GetBullet(bulletType);
        public void ReturnBullet(IBullet bullet) => _bulletPool.ReturnBullet(bullet);

        public void Initialize()
        {
            _bulletFactory = new BulletFactory();
            _bulletPool = new BulletPool(_bulletFactory);
            
            _bulletPool.Preload("Pistol", 10);
            _bulletPool.Preload("Rifle", 5);
        }

        public void ShootBullet(string bulletType, Vector3 position, Vector3 direction)
        {
            var bullet = GetBullet(bulletType);
            bullet.Use(position, direction);
        }
    }
}