using System;
using PrimeTween;
using UnityEngine;

namespace Project.Scripts.TickerSystem
{
    [Serializable]
    public class TickerRotator
    {
        [SerializeField] private float _duration;
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _startRotation;
        [SerializeField] private Vector3 _endRotation;
        
        private Tween _rotateTween;

        public void Start()
        {
            _target.rotation = Quaternion.Euler(_startRotation);
            
            _rotateTween = Tween.Rotation(
                _target,
                Quaternion.Euler(_endRotation),
                _duration, 
                Ease.Linear, 
                -1,
                CycleMode.Yoyo);
        }

        public void Stop()
        {
            _rotateTween.Stop();
        }
    }
}