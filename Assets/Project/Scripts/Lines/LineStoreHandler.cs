using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBrokers;
using Project.Scripts.TickerSystem.BallSpawningSystem;
using UnityEngine;

namespace Project.Scripts.Lines
{
    public class LineStoreHandler : MonoBehaviour
    {
        private const int StartPosition = 0;
        private const int HorizontalDimension = 0;
        private const int VerticalDimension = 1;
        
        [SerializeField] private List<Line> _lines;

        private Ball[,] _ballsGrid;
        private BallReleaser _ballReleaser;
        private CancellationTokenSource _cancellationToken;
        
        private void Awake()
        {
            _ballsGrid = new Ball[_lines.Count, _lines.Count];

            var lineComparator = new LineComparator(_ballsGrid);
            lineComparator.GenerateLines(_lines.Count);

            _ballReleaser = new BallReleaser(lineComparator);
        }

        private void OnEnable()
        {
            foreach (Line line in _lines)
            {
                line.OnBallStored += StoreBall;
                line.OnBallRelease += ReleaseBall;
            }
        }

        private void OnDisable()
        {
            _cancellationToken?.Cancel();
            
            foreach (Line line in _lines)
            {
                line.OnBallStored -= StoreBall;
                line.OnBallRelease -= ReleaseBall;
            }
        }

        private void StoreBall(Line line, Ball ball)
        {
            (int line, int index) ballIndex = FindBallIndex(line, null);

            if(_ballsGrid[ballIndex.line, ballIndex.index] != null)
                return;
                
            _ballsGrid[ballIndex.line, ballIndex.index] = ball;

            ReleaseBall();
        }

        private void ReleaseBall(Line line, Ball ball)
        {
            (int line, int index) ballIndex = FindBallIndex(line, ball);

            if(_ballsGrid[ballIndex.line, ballIndex.index] != ball)
                return;

            for (int column = ballIndex.index; column < _ballsGrid.GetLength(VerticalDimension) - 1; column++)
                _ballsGrid[ballIndex.line, column] = _ballsGrid[ballIndex.line, column + 1];

            _ballsGrid[ballIndex.line, _ballsGrid.GetLength(VerticalDimension) - 1] = null;
            
            ReleaseBall();
        }

        private (int, int) FindBallIndex(Line line, Ball ball)
        {
            int lineIndex = _lines.IndexOf(line);
            
            var ballPosition = Enumerable
                .Range(StartPosition, _ballsGrid.GetLength(VerticalDimension))
                .FirstOrDefault(position => _ballsGrid[lineIndex, position] == ball);

            return (lineIndex, ballPosition);
        }

        private void RelocateBalls()
        {
            
        }

        private void ReleaseBall()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
            
            ReleasingBalls(_cancellationToken.Token).Forget();
        }
        
        private async UniTaskVoid ReleasingBalls(CancellationToken token)
        {
            await _ballReleaser.ReleaseBalls(token);
            
            if(token.IsCancellationRequested)
                return;
            
            for (int i = 0; i < _ballsGrid.GetLength(HorizontalDimension); i++)
                for (int j = 0; j < _ballsGrid.GetLength(VerticalDimension); j++)
                    if (_ballsGrid[i, j] == null)
                        return;
            
            MessageBrokerHolder
                .Game
                .Publish(default(M_GameOver));
        }
    }
}