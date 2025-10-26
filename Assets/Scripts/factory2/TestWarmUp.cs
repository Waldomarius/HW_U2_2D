using System;
using factory2.factory;
using UnityEngine;

namespace factory2
{
    public class TestWarmUp : MonoBehaviour
    {
        [SerializeField] private WarmUpComponent _warmUpComponent;

        private void Update()
        {
            // Спавн объектов по клавише
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _warmUpComponent.WarmUpBats();
            }
            
            // Спавн объектов по клавише
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _warmUpComponent.WarmUpBees();
            }
            
            // Спавн объектов по клавише
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _warmUpComponent.WarmUpBlueBirds();
            }
            
            // Спавн объектов по клавише
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _warmUpComponent.WarmUpChickens();
            }
        }
    }
}