using System;
using Project.Scripts.SpawnerSystem;
using UnityEngine;

namespace Project.Scripts.TickerSystem.BallSpawningSystem
{
    [Serializable]
    public class BallDropper
    {
        [SerializeField] private Ball _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Sprite _defaultSprite;

        private Spawner<Ball> _ballSpawner;

        public void Initialize()
        {
            _ballSpawner = new Spawner<Ball>(_prefab);
        }

        public void Drop()
        {
            Ball ball = _ballSpawner.Spawn(_spawnPoint.position);
            
            ball.Initialize(_defaultSprite);
        }
    }
}