using factory.enemy;
using spawn;
using UnityEngine;

namespace enemy
{
    public class FishEnemy : IEnemy
    {
        private float _speed = 2f;

        public Vector2 Move(Vector2 position)
        {
            Vector2 tempPos = position;
            tempPos.x -= _speed * Time.deltaTime;
            return tempPos;
        }

        public Vector2 StartPosition()
        {
            return SpawnPosition.GetFishEnemyPosition();
        }
    }
}