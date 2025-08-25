using System.Collections.Generic;
using Project.Scripts.SpawnerSystem.Interfaces;
using UnityEngine;

namespace Project.Scripts.SpawnerSystem
{
    public class Pool<T>
        where T : MonoBehaviour, IDespawnable<T>
    {
        private readonly T _prefab;
        private readonly Stack<T> _pool;
        
        public Pool(T prefab)
        {
            _prefab = prefab;
            _pool = new Stack<T>();
        }

        public T Get()
        {
            if (_pool.TryPop(out T template) == false)
                template = Object.Instantiate(_prefab);
            
            template.gameObject.SetActive(false);

            return template;
        }

        public void Release(T template)
        {
            template.gameObject.SetActive(false);
            
            _pool.Push(template);
        }
    }
}