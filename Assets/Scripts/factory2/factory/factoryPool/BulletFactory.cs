using System.Collections.Generic;
using UnityEngine;

namespace factory2.factory
{
    public class BulletFactory : IBulletFactory
    {
        private readonly Dictionary<string, BaseBullet> _bulletPrototypes;
        
        public BulletFactory()
        {
            _bulletPrototypes = new Dictionary<string, BaseBullet>();
            LoadBulletPrototypes();
        }
        
        private void LoadBulletPrototypes()
        {
            var bulletTypes = Resources.LoadAll<BaseBullet>("Bullets");
            
            foreach (var bullet in bulletTypes)
            {
                _bulletPrototypes[bullet.Type] = bullet;
            }
        }
        
        public IBullet CreateBullet(string bulletType)
        {
            if (_bulletPrototypes.TryGetValue(bulletType, out var prototype))
            {
                return prototype;
            }
            
            return null;
        }

        
        // private Dictionary<string, BaseBullet> _bulletPrototypes;
        // private Dictionary<string, System.Type> _bulletTypes = new Dictionary<string, System.Type>();
        //
        // public BulletFactory()
        // {
        //     _bulletPrototypes = new Dictionary<string, BaseBullet>();
        //     LoadBulletPrototypes();
        // }
        //
        //
        // private void LoadBulletPrototypes()
        // {
        //     var types = BulletTypeLoader.GetAllBulletTypes();
        //     foreach (var type in types)
        //     {
        //         var tempInstance = (IBullet) Activator.CreateInstance(type);
        //         _bulletTypes[tempInstance.Type] = type;
        //     }
        // }
        //
        // public IBullet CreateBullet(string bulletType)
        // {
        //     if (_bulletPrototypes.TryGetValue(bulletType, out var prototype))
        //     {
        //         return prototype;
        //     }
        //     
        //     return null;
        // }
    }
}