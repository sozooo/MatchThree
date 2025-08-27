using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBrokers;
using Project.Scripts.UI;
using Project.Scripts.UI.Enums;
using UniRx;
using UnityEngine;
using VContainer;

namespace Project.Scripts.WorkObjects
{
    public class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private CanvasManager _canvasManager;
        [Inject] private PlayerInput _playerInput;

        private CancellationTokenSource _cancellationToken;
        
        private void OnEnable()
        {
            Time.timeScale = 1;
            
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();

            MessageBrokerHolder
                .Game
                .Receive<M_GameOver>()
                .Subscribe(_ => OnGameOver())
                .AddTo(_cancellationToken.Token);
        }
        
        private void OnDisable()
        {
            _cancellationToken?.Cancel();
        }

        private void OnGameOver()
        {
            _canvasManager.ChangeCanvas(GameCanvasType.GameOver);
            
            Time.timeScale = 0;
            _playerInput.Disable();
        }
    }
}