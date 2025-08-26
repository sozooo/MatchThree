using Project.Scripts.Lines;
using Project.Scripts.TickerSystem.BallSpawningSystem;

namespace Project.Scripts.MessageBrokers.BallMessages
{
    public struct M_BallStored
    {
        public M_BallStored(Line line, Ball ball)
        {
            Line = line;
            Ball = ball;
        }

        public Line Line { get; }
        public Ball Ball { get;  }
    }
}