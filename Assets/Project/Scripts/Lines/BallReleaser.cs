using System.Collections.Generic;
using Project.Scripts.TickerSystem.BallSpawningSystem;

namespace Project.Scripts.Lines
{
    public class BallReleaser
    {
        private readonly LineComparator _lineComparator;

        public BallReleaser(LineComparator lineComparator)
        {
            _lineComparator = lineComparator;
        }

        public void ReleaseBalls()
        {
            List<Ball> matchedBalls = _lineComparator.CheckMatches();

            matchedBalls.ForEach(ball => ball.Despawn());
        }
    }
}