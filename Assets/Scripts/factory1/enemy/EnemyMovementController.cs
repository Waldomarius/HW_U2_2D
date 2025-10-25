using System;
using factory.enemy;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField]private EnemyType type;
    private IEnemy _enemy;
    private Vector2 _startPosition;
    private bool _onDamage; 
    void Awake()
    {
        _enemy = EnemyFactory.CreateEnemy(type);
        InitStartPosition();
    }

    public void InitStartPosition()
    {
        transform.position = _enemy.StartPosition();
    }
    private void Update()
    {
        transform.position = _enemy.Move(transform.position);
    }

    public bool IsDestroyEnemy()
    {
        return transform.position.x < -8;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        
        if (go.tag == "Bullet")
        {
            _onDamage = true;
        }
    }

    public bool OnDamage()
    {
        return _onDamage;
    }
}
