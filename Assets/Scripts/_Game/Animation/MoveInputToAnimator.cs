using System;
using _Game.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Animation
{
    public class MoveInputToAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject movementInputObj;
        private IMovementInput _movementInput;
        private static readonly int Speed = Animator.StringToHash("speed");

        private void Start()
        {
            _movementInput = movementInputObj.GetComponent<IMovementInput>();
        }

        private void Update()
        {
            if(_movementInput==null) return;
            
            animator.SetFloat(Speed,_movementInput.MovementData.Direction.magnitude);
        }
    }
}