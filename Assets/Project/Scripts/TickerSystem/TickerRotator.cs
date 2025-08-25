using System;
using PrimeTween;
using UnityEngine;

namespace Project.Scripts.TickerSystem
{
    [Serializable]
    public class TickerRotator
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _target;
        [SerializeField] private Quaternion _startRotation;
        [SerializeField] private Quaternion _endRotation;
        
        private Tween _rotateTween;

        public void Start()
        {
            _target.rotation = _startRotation;
            
            _rotateTween = Tween.RotationAtSpeed(_target, _endRotation, _speed, Ease.Linear, -1, CycleMode.Yoyo);
        }

        public void Stop()
        {
            _rotateTween.Stop();
        }
    }
}