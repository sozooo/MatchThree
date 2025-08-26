using System;
using System.Collections;
using Project.Scripts.MessageBrokers;
using Project.Scripts.MessageBrokers.BallMessages;
using Project.Scripts.SpawnerSystem.Interfaces;
using Project.Scripts.TickerSystem.BallSpawningSystem.BallDatas;
using UnityEngine;

namespace Project.Scripts.TickerSystem.BallSpawningSystem
{
    public class Ball : MonoBehaviour, IDespawnable<Ball>
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private float _timeBeforeDespawn;

        private WaitForSeconds _wait;
        private Coroutine _timer;
        private Transform _transform;
        
        public event Action<Ball> OnDespawn;

        public BallColorID ColorID { get; private set; }

        private void Awake()
        {
            _transform = transform;
            _wait = new WaitForSeconds(_timeBeforeDespawn);
        }

        public void Despawn()
        {
            OnDespawn?.Invoke(this);
            
            MessageBrokerHolder
                .Ball
                .Publish(new M_BallDespawned(_transform.position));
        }
        
        public void Store()
        {
            if (_timer != null)
                StopCoroutine(_timer);

            _timer = null;
        }

        public void Initialize(BallData data)
        {
            _sprite.sprite = data.Sprite;
            ColorID = data.ColorID;

            _timer = StartCoroutine(DespawnTimer());
        }

        private IEnumerator DespawnTimer()
        {
            yield return _wait;

            Despawn();
        }
    }
}