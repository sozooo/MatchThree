using System;
using Project.Scripts.SpawnerSystem;
using Project.Scripts.TickerSystem.BallSpawningSystem.BallDatas;
using UnityEngine;

namespace Project.Scripts.TickerSystem.BallSpawningSystem
{
    [Serializable]
    public class BallDropper
    {
        [SerializeField] private Ball _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private SpriteRenderer _ballPreview;
        [SerializeField] private BallDataProvider _dataProvider;

        private Spawner<Ball> _ballSpawner;

        public void Initialize()
        {
            _ballSpawner = new Spawner<Ball>(_prefab);
        }

        public void Drop()
        {
            Ball ball = _ballSpawner.Spawn(_spawnPoint.position);
            BallData data = _dataProvider.GetRandomData();
            
            ball.Initialize(data);
        }
    }
}