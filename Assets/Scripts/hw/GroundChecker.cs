using System;
using Unity.VisualScripting;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Action<bool> OnGroundStateChange; 
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _raycastDistance = 0.6f;
    private bool isGrounded;

    private void Update()
    {
        CheckGround();
    }

    public void CheckGround()
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, _raycastDistance, _groundLayer);
        if (hit != isGrounded)
        {
            isGrounded = hit;
            // Оповестить всех слушателей об изменении состояния земли
            OnGroundStateChange?.Invoke(isGrounded);
        }
        Debug.DrawRay(transform.position, Vector2.down * _raycastDistance, isGrounded ? Color.green : Color.red);
    }
}
