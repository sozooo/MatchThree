using System;
using Project.Scripts.TickerSystem.BallSpawningSystem.BallDatas;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.TickerSystem.BallSpawningSystem
{
    [Serializable]
    public class BallDataProvider
    {
        [SerializeField] private BallSpawnConfig _spawnConfig;

        public BallData GetRandomData() 
            => _spawnConfig.BallDatas[Random.Range(0, _spawnConfig.BallDatas.Count)];
    }
}