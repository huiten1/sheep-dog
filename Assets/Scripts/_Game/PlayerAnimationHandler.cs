using System;
using _Game.Movement;
using UnityEngine;
using Zenject;

namespace _Game
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        public Animator animator;

        private IMovementInput _movementInput;
        private static readonly int Speed = Animator.StringToHash("speed");

        [Inject]
        public void Construct(IMovementInput movementInput)
        {
            _movementInput = movementInput;
        }

        private void Update()
        {
            if(!animator) return;
            if(_movementInput==null) return;
            
            animator.SetFloat(Speed,_movementInput.MovementData.Direction.magnitude);
        }
    }
}