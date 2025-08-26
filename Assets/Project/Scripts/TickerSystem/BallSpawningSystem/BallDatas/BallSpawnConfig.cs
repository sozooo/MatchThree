using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.TickerSystem.BallSpawningSystem.BallDatas
{
    [CreateAssetMenu(fileName = "BallSpawnConfig", menuName = "Ball/New Ball Spawn Config", order = 51)]
    public class BallSpawnConfig : ScriptableObject
    {
        [field: SerializeField] public List<BallData> BallDatas { get; private set; }
    }
}