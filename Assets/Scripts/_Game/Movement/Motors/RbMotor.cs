using UnityEngine;

namespace _Game.Movement.Motors
{
    public class RbMotor:IMotor
    {
        private Rigidbody _rb;
        public RbMotor(Rigidbody rb)
        {
            _rb = rb;
        }
        public void Move(MovementData movementData)
        {
            _rb.MovePosition(_rb.position+movementData.Direction * (10 * Time.fixedDeltaTime));
            
            
            _rb.MoveRotation(movementData.rotation.normalized);
        }
    }
}