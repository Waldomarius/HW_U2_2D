using pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyFIFOPool _fifoPool;

        void Awake()
        {
            _fifoPool = GetComponent<EnemyFIFOPool>();
            Invoke(nameof(SpawnEnemy), 1f);
        }
        
        private void SpawnEnemy()
        {
            _fifoPool.GetOrCreateEnemy();
            int randomInvoke = Random.Range(1, 2);
            Invoke(nameof(SpawnEnemy), randomInvoke);
        }

        private void Update()
        {
            _fifoPool.ReturnEnemy();
        }
    }
}