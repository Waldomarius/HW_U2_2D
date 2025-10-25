using UnityEngine;

namespace spawn
{
    public class SpawnPosition
    {
        public static Vector2 GetBlokerEnemyPosition()
        {
            return new Vector2(8f, -2.64f);
        }
        
        public static Vector2 GetFishEnemyPosition()
        {
            return new Vector2(8f, -0.64f);
        }
        
        public static Vector2 GetFlyEnemyPosition()
        {
            return new Vector2(8f, 1f);
        }
    }
}