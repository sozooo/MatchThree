using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeTween;
using Project.Scripts.MessageBrokers;
using Project.Scripts.MessageBrokers.BallMessages;
using Project.Scripts.TickerSystem.BallSpawningSystem.BallDatas;
using TMPro;
using UniRx;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private float _duration = 0.1f;

        private float _currentScore;
        private float _targetScore;
        private CancellationTokenSource _cancellationToken;
        private Tween _lerpTween;

        private void Awake()
        {
            _currentScore = 0;
            _scoreText.text = _currentScore.ToString("0");
        }
        
        private void OnEnable()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();

            MessageBrokerHolder
                .Ball
                .Receive<M_BallsReleased>()
                .Subscribe(message => AddScore(message.BallDatas))
                .AddTo(_cancellationToken.Token);
        }
        
        private void OnDisable()
        {
            _cancellationToken?.Cancel();
        }
        
        private void AddScore(List<BallData> balls)
        {
            float score = 0;
            
            balls.ForEach(ball => score += ball.Score);
            
            _targetScore += score;
            
            UpdateScoreText();
        }
        
        private void UpdateScoreText()
        {
            if(_lerpTween.isAlive)
                _lerpTween.Stop();
            
            _lerpTween = Tween.Custom(
                _scoreText, 
                _currentScore, 
                _targetScore, 
                _duration,
                (text, value) =>
                {
                    text.text = value.ToString("0");
                    _currentScore = value;
                },
                ease: Ease.OutCubic);
        }
    }
}