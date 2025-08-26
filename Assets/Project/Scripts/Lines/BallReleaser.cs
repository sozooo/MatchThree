using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBrokers;
using Project.Scripts.MessageBrokers.BallMessages;
using Project.Scripts.TickerSystem.BallSpawningSystem;

namespace Project.Scripts.Lines
{
    public class BallReleaser
    {
        private const float DespawnDelay = 0.1f;
        
        private readonly LineComparator _lineComparator;

        public BallReleaser(LineComparator lineComparator)
        {
            _lineComparator = lineComparator;
        }

        public async UniTask ReleaseBalls(CancellationToken token)
        {
            List<Ball> matchedBalls = _lineComparator.CheckMatches();

            foreach (Ball ball in matchedBalls)
            {
                await UniTask.WaitForSeconds(DespawnDelay, cancellationToken: token);
                
                if(token.IsCancellationRequested)
                    return;
                
                ball.Despawn();
            }
            
            MessageBrokerHolder
                .Ball
                .Publish(new M_BallsReleased(matchedBalls.Select(ball => ball.Data).ToList()));
        }
    }
}