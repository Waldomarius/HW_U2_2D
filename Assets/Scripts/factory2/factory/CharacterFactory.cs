using System;
using System.Collections.Generic;
using UnityEngine;

namespace factory2.factory
{
    public enum CharacterType
    {
        Fly,
        Saw,
        Boss
    }

    public interface ICharacterFactory
    {
        void CreateCharacterOfType(CharacterType type, Color color);
        void CreateAllCharacters(Color color);
    }

    public interface ICharacter
    {
        CharacterType  Type { get; }
        void Spawn(Vector2 position);
        void ChangeColor(Color color);
        void Attack();
    }
    
    public class CharacterFactory : MonoBehaviour, ICharacterFactory 
    {
        [Serializable]
        private class CharacterPrefab
        {
            public CharacterType type;
            public GameObject prefab;
        }
        
        [Serializable]
        private class SpawnPointGroup
        {
            public CharacterType type;
            public List<Vector2> spawnPoints = new List<Vector2>();
        }
        
        [Header("Prefabs")]
        [SerializeField] private List<CharacterPrefab> _characterPrefabs = new List<CharacterPrefab>();
        
        [Header("Spawn Points")]
        [SerializeField] private List<SpawnPointGroup> _spawnPointGroups = new List<SpawnPointGroup>();
        
        
        private Dictionary<CharacterType, GameObject> _prefabDictionary;
        private Dictionary<CharacterType, List<Vector2>> _spawnPointGroupDictionary;
        private Transform _charactersParent;

        public void Initialize()
        {
            _prefabDictionary = new Dictionary<CharacterType, GameObject>();
            _spawnPointGroupDictionary = new Dictionary<CharacterType, List<Vector2>>();
            
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
                _spawnPointGroupDictionary[type] = new List<Vector2>();
            }
            
            // Заполнение словарей
            foreach (var group in _spawnPointGroups)
            {
                _spawnPointGroupDictionary[group.type] = new List<Vector2>(group.spawnPoints);
            }
            
            _charactersParent = GameObject.Find("Characters").transform;
            
            Debug.Log($"CharactersFactory initialized");
            Debug.Log($"Prefabs: {_characterPrefabs.Count}");
            Debug.Log($"Spawn points: {GetTotalSpawnPointsCount()}");
        }
            
        public void CreateCharacterOfType(CharacterType type, Color color)
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
            
            Debug.Log($"Creating {spawnPoints.Count} , {type} with color {color}");

            foreach (Vector2 spawnPoint in spawnPoints)
            {
                CreateSingleCharacter(type, color, spawnPoint);
            }
            
            Debug.Log($" Successfully created {spawnPoints.Count} , {type} characters");
        }

        public void CreateAllCharacters(Color color)
        {
            Debug.Log($"Creating all characters.............");

            foreach (CharacterType type in Enum.GetValues(typeof(CharacterType)))
            {
                CreateCharacterOfType(type, color);
            }
        }
        
        private ICharacter CreateSingleCharacter(CharacterType type, Color color, Vector2 spawnPoint)
        {
            GameObject characterObject = 
                Instantiate(_prefabDictionary[type], spawnPoint, Quaternion.identity, _charactersParent);
            ICharacter character = characterObject.GetComponent<ICharacter>();
            if (character == null)
            {
                Debug.LogError($"Prefab doesn't implement ICharacter interface! Type {type}");
                Destroy(characterObject);
                return null;
            }
            
            character.ChangeColor(color);
            character.Spawn(spawnPoint);
            
            return character;
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

        private void CreateCharactersOfType(CharacterType type, Color color)
        {
        }
    }
}