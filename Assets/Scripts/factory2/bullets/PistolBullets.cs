using factory2.factory;
using UnityEngine;

namespace factory2.bullets
{
    [CreateAssetMenu(fileName = "PistolBullets", menuName = "Bullets/Pistol Bullets", order = 0)]
    public class PistolBullets : BaseBullet
    {
        [SerializeField] private string _type = "Pistol";
        [SerializeField] private float _damage = 10f;
        [SerializeField] private float _velocity = 10f;
        [SerializeField] private float _penetration = 5f;
        [SerializeField] private GameObject _bulletPrefab;

        public override string Type => _type;
        public override float Damage => _damage;
        public override float Velocity => _velocity;
        public override float Penetration => _penetration;
        public override GameObject BulletPrefab => _bulletPrefab;
    }
}