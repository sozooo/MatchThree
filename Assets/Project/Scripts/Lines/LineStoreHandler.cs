using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBrokers;
using Project.Scripts.MessageBrokers.BallMessages;
using Project.Scripts.TickerSystem.BallSpawningSystem;
using UniRx;
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
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
            
            foreach (Line line in _lines)
            {
                line.OnBallStored += StoreBall;
            }

            MessageBrokerHolder
                .Ball
                .Receive<M_BallsReleased>()
                .Subscribe(OnBallsReleased)
                .AddTo(_cancellationToken.Token);
        }

        private void OnDisable()
        {
            _cancellationToken?.Cancel();
            
            foreach (Line line in _lines)
            {
                line.OnBallStored -= StoreBall;
            }
        }

        private void StoreBall(Line line, Ball ball)
        {
            (int line, int index) ballIndex = FindBallIndex(_lines.IndexOf(line), null);

            if(_ballsGrid[ballIndex.line, ballIndex.index] == true)
                return;
                
            _ballsGrid[ballIndex.line, ballIndex.index] = ball;

            ReleasingBalls(_cancellationToken.Token).Forget();
        }

        private void OnBallsReleased(M_BallsReleased message)
        {
            for (int i = 0; i < _ballsGrid.GetLength(HorizontalDimension); i++)
                for (int j = 0; j < _ballsGrid.GetLength(VerticalDimension); j++)
                    if (message.Balls.Contains(_ballsGrid[i, j]))
                        _ballsGrid[i, j] = null;

            RelocateBalls();
        }

        private (int, int) FindBallIndex(int lineIndex, Ball ball)
        {
            var ballPosition = Enumerable
                .Range(StartPosition, _ballsGrid.GetLength(VerticalDimension))
                .FirstOrDefault(position => _ballsGrid[lineIndex, position] == ball);

            return (lineIndex, ballPosition);
        }

        private void RelocateBalls()
        {
            for (int line = 0; line < _ballsGrid.GetLength(HorizontalDimension); line++)
            {
                (int line, int index) ballIndex = FindBallIndex(line, null);
            
                if (_ballsGrid[ballIndex.line, ballIndex.index] == true)
                    return;
            
                for (int column = ballIndex.index; column < _ballsGrid.GetLength(VerticalDimension) - 1; column++)
                    _ballsGrid[ballIndex.line, column] = _ballsGrid[ballIndex.line, column + 1];
                
                _ballsGrid[ballIndex.line, _ballsGrid.GetLength(VerticalDimension) - 1] = null;
            }
        }
        
        private async UniTaskVoid ReleasingBalls(CancellationToken token)
        {
            await _ballReleaser.ReleaseBalls(token);
            
            if (token.IsCancellationRequested)
                return;
            
            for (int i = 0; i < _ballsGrid.GetLength(HorizontalDimension); i++)
                for (int j = 0; j < _ballsGrid.GetLength(VerticalDimension); j++)
                    if (_ballsGrid[i, j] == false)
                        return;
            
            MessageBrokerHolder
                .Game
                .Publish(default(M_GameOver));
        }
    }
}