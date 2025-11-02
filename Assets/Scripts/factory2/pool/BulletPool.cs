using System.Collections.Generic;
using factory2.factory;

namespace factory2.pool
{
    public class BulletPool : IBulletPool
    {
        private readonly IBulletFactory _bulletFactory;
        private readonly Dictionary<string, Queue<IBullet>> _pool;
        private readonly Dictionary<string, int> _maxPoolSize;

        public BulletPool(IBulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
            _pool = new Dictionary<string, Queue<IBullet>>();
            _maxPoolSize = new Dictionary<string, int>()
            {
                {"Pistol", 50},
                {"Rifle", 20}
            };
        }
        
        
        public IBullet GetBullet(string bulletType)
        {
            if (!_pool.ContainsKey(bulletType))
            {
                _pool[bulletType] = new Queue<IBullet>();
            }
            
            var queue = _pool[bulletType];

            if (queue.Count > 0)
            {
                return queue.Dequeue();
            }
            
            return _bulletFactory.CreateBullet(bulletType);
        }

        public void ReturnBullet(IBullet bullet)
        {
            if (bullet == null)
            {
                return;
            }
            
            var bulletType = bullet.Type;
            if (!_pool.ContainsKey(bulletType))
            {
                _pool[bulletType] = new Queue<IBullet>();
            }
            
            var queue = _pool[bulletType];

            if (queue.Count < _maxPoolSize.GetValueOrDefault(bulletType, 20))
            {
                queue.Enqueue(bullet);
            }
        }

        public int GetAvailableCount(string bulletType)
        {
            return _pool.ContainsKey(bulletType) ? _pool[bulletType].Count : 0;
        }

        public void Preload(string bulletType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var bullet = _bulletFactory.CreateBullet(bulletType);
                
                ReturnBullet(bullet);
            }
        }
    }
}