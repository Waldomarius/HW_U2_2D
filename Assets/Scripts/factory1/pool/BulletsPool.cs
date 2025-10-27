using System.Collections;
using System.Collections.Generic;
using bullets;
using UnityEngine;

namespace pool
{
    public class BulletsPool : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private int _initialPoolSize = 20;

        private List<GameObject> _pool = new List<GameObject>();

        private void Start()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                CreateNewObject(Vector2.zero);
            }
        }

        private GameObject CreateNewObject(Vector2  position)
        {
            GameObject newObj = Instantiate(_bulletPrefab, position, Quaternion.identity);
            newObj.SetActive(false);
            _pool.Add(newObj);
            return newObj;
        }

        public GameObject GetOrCreateBullet(Vector2 position)
        {
            foreach (GameObject go in _pool)
            {
                if (!go.activeInHierarchy)
                {
                    go.transform.position = position;
                    go.SetActive(true);
                    return go;
                }
            }

            GameObject newObj = CreateNewObject(position);
            newObj.SetActive(true);
            return newObj;
        }

        public void ReturnBullet(GameObject go)
        {
            BulletMoveComponent component = go.GetComponent<BulletMoveComponent>();
            component.ResetDirection();
            go.SetActive(false);
        }

        public void ReturnBullet(GameObject go, float delay)
        {
            StartCoroutine(DelayReturnBullet(go, delay));
        }

        private IEnumerator DelayReturnBullet(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnBullet(go); 
        }
    }
}