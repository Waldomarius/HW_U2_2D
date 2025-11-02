using UnityEngine;

namespace factory2.factory
{
    public class Interfaces
    {
        
    }

    public interface IBullet
    {
        string Type { get; }
        float Damage { get; }
        float Velocity { get; }
        float Penetration { get; }
        GameObject BulletPrefab { get; }
        void Use(Vector3 position, Vector3 direction);
    }

    public interface IBulletPool
    {
        IBullet GetBullet(string bulletType);
        void ReturnBullet(IBullet bullet);
        int GetAvailableCount(string bulletType);
        void Preload(string bulletType, int count);
    }

    public interface IBulletFactory
    {
        IBullet CreateBullet(string bulletType);
    }
}