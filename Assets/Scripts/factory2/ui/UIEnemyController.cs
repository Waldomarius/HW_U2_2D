using factory2.factory;
using TMPro;
using UnityEngine;

namespace factory2
{
    public class UIEnemyController : MonoBehaviour
    {
        [SerializeField] private GameObject _characterFactory;
        
        private CharacterFactory _factory;
        private int _lootCount;
        private TextMeshProUGUI _scoreGo;

        private void OnEnable()
        {
            // Подписка на слушателя
            _factory.enemyUpdate += HandleEnemyUpdate;
        }
        
        private void HandleEnemyUpdate(int count)
        {
            _lootCount += count;
            _scoreGo.text = _lootCount.ToString();
        }
        
        private void Awake()
        {
            _factory = _characterFactory.GetComponent<CharacterFactory>();
            _scoreGo = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            _scoreGo.text = _lootCount.ToString();
        }

        private void OnDisable()
        {
            // Отписка от слушателя
            _factory.enemyUpdate -= HandleEnemyUpdate;
        }
    }
}