using System.Collections.Generic;
using Project.Scripts.TickerSystem.BallSpawningSystem;

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