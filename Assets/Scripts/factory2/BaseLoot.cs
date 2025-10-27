using factory2.factory;
using UnityEngine;

namespace factory2
{
    public class BaseLoot : MonoBehaviour, ILoot
    {
        [Header("Character Properties")]
        [SerializeField] public string _lootName;
        [SerializeField] public LootType _lootType;

        public string Name => _lootName;
        public LootType Type => _lootType;
        
        public void Spawn(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }
    }
}