using System.Collections.Generic;
using Project.Scripts.TickerSystem.BallSpawningSystem;
using Project.Scripts.TickerSystem.BallSpawningSystem.BallDatas;

namespace Project.Scripts.MessageBrokers.BallMessages
{
    public struct M_BallsReleased
    {
        public M_BallsReleased(List<Ball> balls)
        {
            Balls = balls;
        }

        public List<Ball> Balls { get; }
    }
}