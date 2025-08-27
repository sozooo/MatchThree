using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBrokers;
using Project.Scripts.MessageBrokers.BallMessages;
using Project.Scripts.SpawnerSystem;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Project.Scripts.WorkObjects.Effects
{
    public class ExplosionEffectsSpawner : Spawner<Effect>, IInitializable, IDisposable
    {
        private CancellationTokenSource _cancellationToken;
        
        public ExplosionEffectsSpawner(Effect prefab) : base(prefab)
        {
        }
        
        public void Initialize()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();

            MessageBrokerHolder
                .Ball
                .Receive<M_BallDespawned>()
                .Subscribe(message => Spawn(message.Position))
                .AddTo(_cancellationToken.Token);
        }

        public void Dispose()
        {
            _cancellationToken?.Cancel();
        }
    }
}