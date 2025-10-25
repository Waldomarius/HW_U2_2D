using System;
using enemy;
using UnityEngine;

namespace factory.enemy
{
    public interface IEnemy
    {
        public Vector2 Move(Vector2 position);
        public Vector2 StartPosition();
        
    }

    public enum EnemyType
    {
        Bloker,
        Fish,
        Fly,
        Poker,
        Slime,
        Snail
    }

    public class EnemyFactory
    {
        public static IEnemy CreateEnemy(EnemyType type)
        {
            return type switch
            {
                EnemyType.Bloker => new BlokerEnemy(),
                EnemyType.Fish => new FishEnemy(),
                EnemyType.Fly => new FlyEnemy(),
                _ => throw new ArgumentException("Unknown type.")
            };
        }
    }


}