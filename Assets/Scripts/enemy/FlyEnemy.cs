using factory.enemy;
using spawn;
using UnityEngine;

namespace enemy
{
    public class FlyEnemy: IEnemy
    {
        private float _speed = 3f;

        public Vector2 Move(Vector2 position)
        {
            Vector2 tempPos = position;
            tempPos.x -= _speed * Time.deltaTime;
            tempPos.y = Mathf.Sin(position.x) * 1.2f;
            return tempPos;
        }

        public Vector2 StartPosition()
        {
            return SpawnPosition.GetFlyEnemyPosition();
        }
    }
}