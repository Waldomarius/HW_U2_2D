using factory2.factory;
using UnityEngine;

namespace factory2.bullets
{
    [CreateAssetMenu(fileName = "RifleBullets", menuName = "Bullets/Rifle Bullets", order = 0)]
    public class RifleBullets : BaseBullet
    {
        [SerializeField] private string _type = "Pistol";
        [SerializeField] private float _damage = 50f;
        [SerializeField] private float _velocity = 400f;
        [SerializeField] private float _penetration = 25f;
        [SerializeField] private GameObject _bulletPrefab;

        public override string Type => _type;
        public override float Damage => _damage;
        public override float Velocity => _velocity;
        public override float Penetration => _penetration;
        public override GameObject BulletPrefab => _bulletPrefab;
    }
}