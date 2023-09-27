using UnityEngine;

namespace _Game.Movement
{
    public interface IMotor
    {
        void Move(MovementData movementData);
    }

    public class MovementData
    {
        public Vector3 Direction { get; set; }
        public Quaternion rotation;
    }
}