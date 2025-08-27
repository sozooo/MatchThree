using Project.Scripts.Scopes;
using Project.Scripts.WorkObjects.Effects;
using UnityEngine;

namespace Project.Scripts.WorkObjects
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Effect _explosionEffect;
        
        private ExplosionEffectsSpawner explosionEffectsSpawner;
        
        private void OnEnable()
        {
            explosionEffectsSpawner = new ExplosionEffectsSpawner(_explosionEffect);
            
            explosionEffectsSpawner.Initialize();
        }

        private void OnDisable()
        {
            explosionEffectsSpawner.Dispose();
        }
    }
}