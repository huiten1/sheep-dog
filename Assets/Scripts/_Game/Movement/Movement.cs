
using System;
using UnityEngine;
using Zenject;

namespace _Game.Movement
{
    public class Movement : MonoBehaviour
    {

        private IMotor _motor;
        private IMovementInput _movementInput;
        [SerializeField] private UpdateType updateType;

  
        [Inject]
        public void Construct(IMovementInput movementInput, IMotor motor)
        {
            _motor = motor;
            _movementInput = movementInput;
        }

        private void Update()
        {
            if(updateType!=UpdateType.Update) return;
            Tick();
        }

        private void FixedUpdate()
        {
            if(updateType!=UpdateType.FixedUpdate) return;
            Tick();
        }

        private void LateUpdate()
        {
            if(updateType!=UpdateType.LateUpdate) return;
            Tick();
        }

        private void Tick()
        {
            _movementInput?.ReadInput();
            _motor?.Move(_movementInput.MovementData);
        }
    }

    public enum UpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate,
    }
}