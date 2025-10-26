using factory2.factory;
using pool;
using UnityEngine;

namespace bullets
{
    public class BulletsSpawner: MonoBehaviour
    {
        [SerializeField] private CharacterFactory _characterFactory;
        
        private BulletsPool _Pool;
        
        void Awake()
        {
            _Pool = GetComponent<BulletsPool>();
        }

        public void SpawnBullet(Vector2 position)
        {
            GameObject go = _Pool.GetOrCreateBullet(position);
            _Pool.ReturnBullet(go , 1.5f);
        }
        
        private void Update()
        {
            GameObject go = _characterFactory.TriggeredGameObject();
            if (go != null)
            {
                _Pool.ReturnBullet(go);
            }
        }

    }
}