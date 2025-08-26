using UnityEngine;

namespace Project.Scripts.MessageBrokers.BallMessages
{
    public struct M_BallDespawned
    {
        public M_BallDespawned(Vector2 position)
        {
            Position = position;
        }
        
        public Vector2 Position { get; }
    }
}