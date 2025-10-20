using System;
using System.Collections.Generic;
using UnityEngine;

namespace pool
{
    public class FIFOPool<T> : MonoBehaviour where T : Component
    {
        [Header("Pool Settings")]
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _poolParent;
        [SerializeField] private int _initialSize;
        [SerializeField] private int _maxSize;

        private Queue<T> _avaliableObjects = new Queue<T>();
        private List<T> _allObjects = new List<T>();
        private int _activeCount = 0;
        
        private void Start()
        {
            if (_poolParent == null)
            {
                _poolParent = transform;
            }
            
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                CreateNewObject();
            }
        }

        private T CreateNewObject()
        {
            T newObj = Instantiate(_prefab, _poolParent);
            newObj.gameObject.SetActive(false);
            var pooledComponent = newObj.GetComponent<PooledObject>();

            if (pooledComponent == null)
            {
                pooledComponent = newObj.gameObject.AddComponent<PooledObject>();
            }

            pooledComponent.SetPool(this);
            _avaliableObjects.Enqueue(newObj);
            _allObjects.Add(newObj);
            
            return newObj;
        }

        public T GetObject()
        {
            T obj = default(T);

            if (_avaliableObjects.Count > 0 )
            {
                obj = _avaliableObjects.Dequeue();
            }
            else if (_allObjects.Count < _maxSize)  
            {
                obj = CreateNewObject();
            }
            else
            {
                Debug.LogWarning($"Pool<{typeof(T).Name}> is full");
                return null;
            }

            if (obj != null)
            {
                obj.gameObject.SetActive(true);
                _activeCount++;
            }
            return obj;
        }

        public T GetObject(Vector3 position, Quaternion rotation)
        {
            T obj = GetObject();
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
            }
            return obj;
        }

        public void ReturnObject(T obj)
        {
            if (obj == null || !_allObjects.Contains(obj))
            {
                return;
            }
            
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_poolParent);
            _avaliableObjects.Enqueue(obj);
            _activeCount--;
        }

        public void PrintStatus()
        {
            Debug.Log($"Pool<{typeof(T).Name}> Total: {_allObjects.Count}, Available: {_avaliableObjects.Count}");
        }
        
        public int TotalCount() => _allObjects.Count;
        public int ActiveCount() => _activeCount;
        public int AvailableCount() => _avaliableObjects.Count;
        
        
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }


    public class PooledObject : MonoBehaviour
    {
        private object _pool;

        public void SetPool<T>(FIFOPool<T> pool) where T : Component
        {
            _pool = pool;
        }

        void OnDisable()
        {
            if (_pool != null)
            {
                var method = _pool.GetType().GetMethod("ReturnObject");
                var component = GetComponent(_pool.GetType().GetGenericArguments()[0]);
                method?.Invoke(_pool, new object[] { component });
            }
        }
    }

    public class PoolManager : MonoBehaviour
    {
        public static PoolManager _instance;
        public static PoolManager Instance => _instance;
        
        private Dictionary<string, object> _pools = new Dictionary<string, object>();
        
        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public FIFOPool<T> CreatePool<T>(T prefab, Transform parent, int initialSize = 10, int maxSize = 50)
            where T : Component
        {
            string poolKey = $"{typeof(T).Name}_{prefab.GetInstanceID()}";

            if (!_pools.ContainsKey(poolKey))
            {
                // var pool = new FIFOPool<T>(prefab, parent, initialSize, maxSize);
                // _pools.Add(poolKey, pool);
            }
            
            return (FIFOPool<T>)_pools[poolKey];
        }
        
        public FIFOPool<T> GetPool<T>(T prefab) where T : Component
        {
            string poolKey = $"{typeof(T).Name}_{prefab.GetInstanceID()}";
            return _pools.ContainsKey(poolKey) ? (FIFOPool<T>)_pools[poolKey] : null;
        }
    }
}