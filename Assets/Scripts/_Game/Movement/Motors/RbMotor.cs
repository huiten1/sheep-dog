
using Unity.Mathematics;
using UnityEngine;

namespace _Game.Movement.Motors
{
    public class RbMotor:IMotor
    {
        private Rigidbody _rb;

        private float _moveSpeed;
        public RbMotor(Rigidbody rb,float moveSpeed)
        {
            _rb = rb;
            _moveSpeed = moveSpeed;
        }
        public void Move(MovementData movementData)
        {
            _rb.MovePosition(_rb.position+movementData.Direction * (_moveSpeed * Time.fixedDeltaTime));
            
            
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, movementData.rotation.normalized,Time.fixedDeltaTime*5));
        }
    }
    public class CCMotor:IMotor
    {
        private CharacterController _cc;

        private float _moveSpeed;
        public CCMotor(CharacterController characterController,float moveSpeed)
        {
            _cc = characterController;
            _moveSpeed = moveSpeed;
          
        }
        public void Move(MovementData movementData)
        {

            
            _cc.Move(movementData.Direction * (_moveSpeed * Time.deltaTime));
            var position = _cc.transform.position;
            position = new Vector3(position.x, 0, position.z);
            _cc.transform.position = position;
            if(movementData.Direction.sqrMagnitude>0.01f)
                _cc.transform.rotation= Quaternion.Slerp(_cc.transform.rotation, Quaternion.LookRotation(movementData.Direction),Time.deltaTime*5);
        }
    }
}