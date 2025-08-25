using System;
using UnityEngine;

namespace Project.Scripts.SpawnerSystem.Interfaces
{
    public interface IDespawnable<T>
        where T : MonoBehaviour, IDespawnable<T>
    {
        public event Action<T> OnDespawn; 
        
        public void Despawn();
    }
}