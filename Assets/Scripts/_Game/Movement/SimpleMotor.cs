using UnityEngine;

namespace _Game.Movement
{
    public class SimpleMotor: IMotor
    {
        private Transform _transform;

        public SimpleMotor(Transform transform)
        {
            _transform = transform;
        }
        public void Move(MovementData movementData)
        {
            _transform.position += movementData.Direction*Time.deltaTime;
        }
    }
}