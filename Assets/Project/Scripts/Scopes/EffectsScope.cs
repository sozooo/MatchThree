using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Scripts.Scopes
{
    public class EffectsScope : LifetimeScope
    {
        [SerializeField] private Effect _explosionEffect;
        
        protected override void Configure(IContainerBuilder builder)
        {
            var explosionEffectsSpawner = new ExplosionEffectsSpawner(_explosionEffect);
            
            builder
                .RegisterInstance(explosionEffectsSpawner)
                .WithParameter(_explosionEffect)
                .As<IInitializable>()
                .As<IDisposable>();
        }
    }
}