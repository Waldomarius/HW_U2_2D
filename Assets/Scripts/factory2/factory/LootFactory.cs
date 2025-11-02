using System;
using System.Collections.Generic;
using UnityEngine;

namespace factory2.factory
{
    public enum LootType
    {
        Apple,
        Melon
    }

    public interface ILoot
    {
        string Name { get;}
        LootType Type { get;}
        void Spawn(Vector2 position);
    }
    
    public class LootFactory : MonoBehaviour
    {
        [Serializable]
        private class LootPrefab
        {
            public LootType type;
            public GameObject prefab;
        }

        [Serializable]
        public class LootSpawnConfig
        {
            public LootType lootType;
            public List<SpawnPointData> spawnPoints = new List<SpawnPointData>();
        }
        
        [Serializable]
        public class SpawnPointData
        {
            public Vector2 position;
        }
        
        [Header("Prefabs")]
        [SerializeField] private List<LootPrefab> _lootPrefabs = new List<LootPrefab>();
        
        [Header("Spawn Points")]
        [SerializeField] private List<LootSpawnConfig> _spawnConfigs = new List<LootSpawnConfig>();
        
        private Dictionary<LootType, GameObject> _prefabDictionary;
        private Dictionary<LootType, List<SpawnPointData>> _spawnPointGroupDictionary;
        private Transform _lootParent;
        
        public void Initialize()
        {
            _prefabDictionary = new Dictionary<LootType, GameObject>();
            _spawnPointGroupDictionary = new Dictionary<LootType, List<SpawnPointData>>();
            
            // Инициализация словарей
            foreach (var lootPrefab in _lootPrefabs)
            {
                if (lootPrefab.prefab != null)
                {
                    _prefabDictionary[lootPrefab.type] = lootPrefab.prefab;
                }
            }

            foreach (LootType type in Enum.GetValues(typeof(LootType)))
            {
                _spawnPointGroupDictionary[type] = new List<SpawnPointData>();
            }
            
            // Заполнение словарей
            foreach (var config in _spawnConfigs)
            {
                _spawnPointGroupDictionary[config.lootType] = new List<SpawnPointData>(config.spawnPoints);
            }
            
            _lootParent = new GameObject("Loot").transform;
            
            Debug.Log($"LootFactory initialized");
        }
        
        public void CreateAllLoots()
        {
            Debug.Log($"Creating all loots.............");

            foreach (LootType type in Enum.GetValues(typeof(LootType)))
            {
                CreateLootsOfType(type);
            }
        }
        
        public void CreateLootsOfType(LootType type)
        {
            if (!_prefabDictionary.ContainsKey(type))
            {
                Debug.LogError($"Loot type {type} does not found");
                return;
            }
            
            var spawnPoints = _spawnPointGroupDictionary[type];
            if (spawnPoints.Count == 0)
            {
                Debug.LogWarning($"No spawn point found for {type}");
                return;
            }
            
            Debug.Log($"Creating {spawnPoints.Count} , {type}");

            foreach (var spawnData in spawnPoints)
            {
                CreateLootFromSpawnPointData(type, spawnData);
            }
        }
        
        private void CreateLootFromSpawnPointData(LootType type, SpawnPointData spawnData)
        {
            GameObject lootObject = Instantiate(
                _prefabDictionary[type],
                spawnData.position,
                Quaternion.identity,
                _lootParent);
            
            ILoot loot = lootObject.GetComponent<ILoot>();
            if (loot == null)
            {
                Debug.LogError($"Prefab doesn't implement ILoot interface! Type {type}");
                Destroy(lootObject);
                return;
            }
            SpriteRenderer sr = lootObject.GetComponent<SpriteRenderer>();
            if (sr == null)
            {
                Debug.LogError($"No SpriteRenderer found on prefab for {type}!");
            }
            
            loot.Spawn(spawnData.position);
        }
    }
}