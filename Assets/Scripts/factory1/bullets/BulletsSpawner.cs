using System;
using pool;
using UnityEngine;

namespace bullets
{
    public class BulletsSpawner: MonoBehaviour
    {

        private BulletsPool _Pool;
        
        void Awake()
        {
            _Pool = GetComponent<BulletsPool>();
        }

        public void SpawnBullet(Vector2 position)
        {
            GameObject go = _Pool.GetOrCreateBullet(position);
            _Pool.ReturnBullet(go , 3f);
        }

    }
}