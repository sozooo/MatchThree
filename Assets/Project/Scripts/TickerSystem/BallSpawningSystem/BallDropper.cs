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
        
        private BallData _currentBallData;

        public void Initialize()
        {
            _ballSpawner = new Spawner<Ball>(_prefab);

            UpdatePreview();
        }

        public void Drop()
        {
            Ball ball = _ballSpawner.Spawn(_spawnPoint.position);
            ball.Initialize(_currentBallData);

            UpdatePreview();
        }

        private void UpdatePreview()
        {
            _currentBallData = _dataProvider.GetRandomData();
            _ballPreview.sprite = _currentBallData.Sprite;
        }
    }
}