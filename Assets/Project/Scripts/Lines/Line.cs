using System;
using Project.Scripts.TickerSystem.BallSpawningSystem;
using UnityEngine;

namespace Project.Scripts.Lines
{
    public class Line : MonoBehaviour
    {
        public event Action<Line, Ball> OnBallStored;
        public event Action<Line, Ball> OnBallRelease;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Ball ball) == false)
                return;

            ball.Store();
            
            OnBallStored?.Invoke(this, ball);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Ball ball) == false)
                return;
            
            OnBallRelease?.Invoke(this, ball);
        }
    }
}