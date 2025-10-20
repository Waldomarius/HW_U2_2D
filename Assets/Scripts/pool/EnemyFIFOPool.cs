using System.Collections.Generic;
using UnityEngine;

namespace pool
{
    public class EnemyFIFOPool : MonoBehaviour
    {
        [Header("Set in Inspector")]
        public GameObject[] prefabEnemies;
        [SerializeField] private int _initialPoolSize = 10;

        private Queue<GameObject> _pool = new Queue<GameObject>();
        private List<GameObject> _activeEnemies = new List<GameObject>();

        private void Start()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                CreateNewObject();
            }
        }

        private GameObject CreateNewObject()
        {
            // Выбрать случайный шаблон Enemy для создания объекта.
            int randomEnemy = Random.Range(0, prefabEnemies.Length);
            GameObject newObj = Instantiate(prefabEnemies[randomEnemy]);
            newObj.SetActive(false);
            _pool.Enqueue(newObj);
            return newObj;
        }

        public GameObject GetOrCreateEnemy()
        {
            if (_pool.Count == 0)
            {
                CreateNewObject();
            }

            GameObject newObj = _pool.Dequeue();
            newObj.SetActive(true);
            _activeEnemies.Add(newObj);
            return newObj;
        }

        public void ReturnEnemy()
        {
            foreach (GameObject go in new List<GameObject>(_activeEnemies))
            {
                EnemyMovementController controller = go.GetComponent<EnemyMovementController>();
            
                if (controller.IsDestroyEnemy() || controller.OnDamage())
                {
                    go.SetActive(false);
                    controller.InitStartPosition();
                    _pool.Enqueue(go);
                    _activeEnemies.Remove(go);
                }
            }
        }

    }
}