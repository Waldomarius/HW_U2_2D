using factory2.factory;
using UnityEngine;

namespace factory2
{
    public abstract class BaseCharacter : MonoBehaviour, ICharacter
    {
        [Header("Character Properties")]
        [SerializeField] public string _characterName;
        [SerializeField] public CharacterType _characterType;

        public string Name => _characterName;
        public CharacterType Type => _characterType;
        
        protected SpriteRenderer _spriteRenderer;
        public bool isDestroyed;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _spriteRenderer.color;
            }
        }
        public void Spawn(Vector2 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void ChangeColor(Color color)
        {
            if (_spriteRenderer  != null)
            {
                _spriteRenderer.color = color;
                _spriteRenderer.enabled = false;
                _spriteRenderer.enabled = true;
            }
        }

        public abstract void Attack();

        public virtual void SetMovement(CharacterFactory.SpawnPointData spawnPointData)
        {
            
        }
        
        

        public void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject go = collision.gameObject;
        
            if (go.tag == "Bullet")
            {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }
}