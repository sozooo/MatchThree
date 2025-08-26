using System;
using Project.Scripts.TickerSystem.BallSpawningSystem;
using UnityEngine;

namespace Project.Scripts.Lines
{
    public class Line : MonoBehaviour
    {
        public event Action<Line, Ball> OnBallStored;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Ball ball) == false)
                return;

            ball.Store();
            
            OnBallStored?.Invoke(this, ball);
        }
    }
}