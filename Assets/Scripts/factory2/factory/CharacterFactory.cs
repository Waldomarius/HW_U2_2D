using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace factory2.factory
{
    public enum CharacterType
    {
        Bat,
        Bee,
        BlueBird,
        Chicken
    }

    public enum MovementType
    {
        None,
        Horizontal,
        Vertical,
        Sinusoidal,
        Circular
    }

    
    public interface ICharacter
    {
        string Name { get;}
        CharacterType  Type { get; }
        void Spawn(Vector2 position);
        void ChangeColor(Color color);
        void Attack();
    }
    
    public class CharacterFactory : MonoBehaviour
    {
        [Serializable]
        private class CharacterPrefab
        {
            public CharacterType type;
            public GameObject prefab;
        }
        
        [Serializable]
        public class SpawnPointData
        {
            public Vector2 position;
            public Color color = Color.white;
            
            public MovementType movementType = MovementType.Horizontal;
            public float moveSpeed = 2f;
            public float amplitude = 1f;
            public float frequency = 2f;
            public float distanceX = 3f;
            public float distanceY = 2f;
        }

        [Serializable]
        public class CharacterSpawnConfig
        {
            public CharacterType characterType;
            public List<SpawnPointData> spawnPoints = new List<SpawnPointData>();
        }
        
        
        [Header("Prefabs")]
        [SerializeField] private List<CharacterPrefab> _characterPrefabs = new List<CharacterPrefab>();
        
        [Header("Spawn Points")]
        [SerializeField] private List<CharacterSpawnConfig> _spawnConfigs = new List<CharacterSpawnConfig>();
        
        public Action<int> enemyUpdate;
        // Список заспавненых врагов
        private List<ICharacter> _spawnedObjects = new List<ICharacter>();
        private Dictionary<CharacterType, GameObject> _prefabDictionary;
        private Dictionary<CharacterType, List<SpawnPointData>> _spawnPointGroupDictionary;
        private Transform _charactersParent;

        public void Initialize()
        {
            _prefabDictionary = new Dictionary<CharacterType, GameObject>();
            _spawnPointGroupDictionary = new Dictionary<CharacterType, List<SpawnPointData>>();
            
            // Инициализация словарей
            foreach (var characterPrefab in _characterPrefabs)
            {
                if (characterPrefab.prefab != null)
                {
                    _prefabDictionary[characterPrefab.type] = characterPrefab.prefab;
                }
            }

            foreach (CharacterType type in Enum.GetValues(typeof(CharacterType)))
            {
                _spawnPointGroupDictionary[type] = new List<SpawnPointData>();
            }
            
            // Заполнение словарей
            foreach (var config in _spawnConfigs)
            {
                _spawnPointGroupDictionary[config.characterType] = new List<SpawnPointData>(config.spawnPoints);
            }
            
            _charactersParent = new GameObject("Enemy").transform;
            
            Debug.Log($"CharactersFactory initialized");
        }
            
        public void CreateCharactersOfType(CharacterType type, Color colorDefault)
        {
            if (!_prefabDictionary.ContainsKey(type))
            {
                Debug.LogError($"Character type {type} does not found");
                return;
            }
            
            var spawnPoints = _spawnPointGroupDictionary[type];
            if (spawnPoints.Count == 0)
            {
                Debug.LogWarning($"No spawn point found for {type}");
                return;
            }
            
            Debug.Log($"Creating {spawnPoints.Count} , {type} with color {colorDefault}");

            foreach (var spawnData in spawnPoints)
            {
                Color characterColor = spawnData.color != Color.white ? spawnData.color : colorDefault;
                CreateCharacterFromSpawnPointData(type, spawnData, characterColor);
            }
        }

        private void CreateCharacterFromSpawnPointData(CharacterType type, SpawnPointData spawnData, Color characterColor)
        {
            GameObject characterObject = Instantiate(
                _prefabDictionary[type],
                spawnData.position,
                Quaternion.identity,
                _charactersParent);
            
            ICharacter character = characterObject.GetComponent<ICharacter>();
            if (character == null)
            {
                Debug.LogError($"Prefab doesn't implement ICharacter interface! Type {type}");
                Destroy(characterObject);
                return;
            }
            SpriteRenderer sr = characterObject.GetComponent<SpriteRenderer>();
            if (sr == null)
            {
                Debug.LogError($"No SpriteRenderer found on prefab for {type}!");
            }
            else
            {
                Debug.Log($"SpriteRenderer found. Current color: {sr.color}, new color: {characterColor}");
            }

            if (character is BaseCharacter baseCharacter)
            {
                baseCharacter.SetMovement(spawnData);
            }
            
            character.ChangeColor(characterColor);
            character.Spawn(spawnData.position);
            

            _spawnedObjects.Add(character);
        }

        public void CreateAllCharacters(Color colorDefault)
        {
            Debug.Log($"Creating all characters.............");

            foreach (CharacterType type in Enum.GetValues(typeof(CharacterType)))
            {
                CreateCharactersOfType(type, colorDefault);
            }
        }

        public int GetSpawnPointCount(CharacterType type)
        {
            return _spawnPointGroupDictionary.ContainsKey(type) ? _spawnPointGroupDictionary[type].Count : 0;
        }

        public int GetTotalSpawnPointsCount()
        {
            int total = 0;
            foreach (var points in _spawnPointGroupDictionary.Values)
            {
                total += points.Count;
            }

            return total;
        }

        


        
        public void Update()
        {
            foreach (ICharacter character in _spawnedObjects)
            {
                if (character is BaseCharacter characterBase && characterBase.isDestroyed)
                {
                    enemyUpdate?.Invoke(1);
                    characterBase.isDestroyed = false;
                }
            }
        }
    }
}