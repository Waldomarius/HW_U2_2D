using Scripts.hw;
using TMPro;
using UnityEngine;

namespace factory2
{
    public class ScoreCounterController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        
        private MovementController _movementController;
        private int _lootCount;
        private TextMeshProUGUI _scoreGo;
        private const string ScoreText = "Score: ";

        private void OnEnable()
        {
            // Подписка на слушателя
            _movementController.lootUpdate += HandleLootUpdate;
        }
        
        private void HandleLootUpdate(int count)
        {
            _lootCount += count;
            _scoreGo.text = ScoreText + _lootCount;
        }
        
        private void Awake()
        {
            _movementController = _player.GetComponent<MovementController>();
            _scoreGo = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            _scoreGo.text = ScoreText + _lootCount;
        }

        private void OnDisable()
        {
            // Отписка от слушателя
            _movementController.lootUpdate -= HandleLootUpdate;
        }
    }
}