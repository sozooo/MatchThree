using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.SpawnerSystem.Interfaces;
using UnityEngine;

namespace Project.Scripts.Scopes
{
    public class Effect : MonoBehaviour, IDespawnable<Effect>
    {
        [SerializeField] private ParticleSystem _particle;

        private CancellationTokenSource _cancellationToken;
        
        public event Action<Effect> OnDespawn;
        
        private void OnEnable()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
        
            _particle.Play();
            WaitForParticleAsync(_particle, _cancellationToken.Token).Forget();
        }
        
        private void OnDisable()
        {
            _cancellationToken?.Cancel();
        }
        
        public void Despawn()
        {
            OnDespawn?.Invoke(this);
        }
        
        private async UniTaskVoid WaitForParticleAsync(ParticleSystem particle, CancellationToken token)
        {
            while (particle.IsAlive(true))
            {
                await UniTask.Yield(cancellationToken: token);
            }

            Despawn();
        }
    }
}