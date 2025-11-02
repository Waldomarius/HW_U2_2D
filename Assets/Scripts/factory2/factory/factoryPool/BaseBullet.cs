using UnityEngine;

namespace factory2.factory
{
    public abstract class BaseBullet : ScriptableObject, IBullet
    {
        public abstract string Type { get; }
        public abstract float Damage { get; }
        public abstract float Velocity { get; }
        public abstract float Penetration { get; }
        public abstract GameObject BulletPrefab { get; }
        
        public virtual void Use(Vector3 position, Vector3 direction)
        {
            if (BulletPrefab != null)
            {   
                var bulletObj = Instantiate(BulletPrefab, position, Quaternion.identity);
                var bulletComponent = bulletObj.GetComponent<BulletController>();
                if (bulletComponent != null)
                {
                    bulletComponent.Initialize(this, direction);
                }
            }
        }
    }
}