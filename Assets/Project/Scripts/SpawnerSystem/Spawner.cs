using Project.Scripts.SpawnerSystem.Interfaces;
using UnityEngine;

namespace Project.Scripts.SpawnerSystem
{
    public class Spawner<T> 
        where T : MonoBehaviour, IDespawnable<T>
    {
        private readonly Pool<T> _pool;
        
        public Spawner(T prefab)
        {
            _pool = new Pool<T>(prefab);
        }

        public T Spawn(Vector2 position)
        {
            T template = _pool.Get();
            
            template.gameObject.SetActive(true);
            template.transform.position = position;
            template.OnDespawn += Despawn;

            return template;
        }

        private void Despawn(T template)
        {
            template.OnDespawn -= Despawn;
            
            _pool.Release(template);
        }
    }
}