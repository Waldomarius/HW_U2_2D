using factory2.factory;
using UnityEngine;

namespace factory2
{
    public class WarmUpComponent : MonoBehaviour
    {
        [Header("Factory References")]
        [SerializeField] private CharacterFactory _characterFactory;
        [SerializeField] private LootFactory _lootFactory;
        
        [Header("Warm Up settings")]
        [SerializeField] private Color _warmUpColor = Color.red;
        [SerializeField] private bool _warmUpOnStart = true;
        [SerializeField] private bool _useFactoryColor = true;


        private void Start()
        {
            StartCharacterFactory();
            StartLootFactory();
        }
        
        private void StartCharacterFactory()
        {
            if (!CheckFactory())
            {
                return;
            }
            
            _characterFactory.Initialize();
            
            if (_warmUpOnStart)
            {
                WarmAllCharacters();
            }
        }

        private void StartLootFactory()
        {
            if (!_lootFactory)
            {
                return;
            }
            
            _lootFactory.Initialize();
            
            if (_warmUpOnStart)
            {
                Debug.Log(" ========  Warm Up Creating All Loots  ======== ");
                _lootFactory.CreateAllLoots();
            }
        }

        private void WarmAllCharacters()
        {
            if (!CheckFactory())
            {
                return;
            }
            
            Debug.Log(" ========  Warm Up Creating All Characters  ======== ");

            if (_useFactoryColor)
            {
                _characterFactory.CreateAllCharacters(Color.white);
            }
            else
            {
                _characterFactory.CreateAllCharacters(_warmUpColor);
            }
        }

        public void WarmUpCharacterType(CharacterType type)
        {
            if (!CheckFactory())
            {
                return;
            }
            
            Debug.Log($" ========  Warm Up Creating {type} Characters  ======== ");
            
            if (_useFactoryColor)
            {
                _characterFactory.CreateCharactersOfType(type, Color.white);
            }
            else
            {
                _characterFactory.CreateCharactersOfType(type, _warmUpColor);
            }
        }
        
        public void WarmUpBats() => WarmUpCharacterType(CharacterType.Bat);
        public void WarmUpBees() => WarmUpCharacterType(CharacterType.Bee);
        public void WarmUpBlueBirds() => WarmUpCharacterType(CharacterType.BlueBird);
        public void WarmUpChickens() => WarmUpCharacterType(CharacterType.Chicken);

        private bool CheckFactory()
        {
            return _characterFactory;
        }
    }
}