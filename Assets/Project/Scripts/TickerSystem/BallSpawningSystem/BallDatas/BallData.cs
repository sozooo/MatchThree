using System;
using UnityEngine;

namespace Project.Scripts.TickerSystem.BallSpawningSystem.BallDatas
{
    [Serializable]
    public struct BallData
    {
        [field: SerializeField] public BallColorID ColorID { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}