using UnityEngine;
using Zenject;

namespace _Game.Movement.Inputs
{
    public class JoystickInput:IMovementInput
    {
        private Joystick _joystick;
        
        [Inject]
        public JoystickInput(Joystick joystick)
        {
            _joystick = joystick;
            MovementData = new MovementData();
        }
        public MovementData MovementData { get; private set; }
        public void ReadInput()
        {
            var dir =new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            MovementData.Direction = dir;
            
            if(dir.magnitude>0.01f)
                MovementData.rotation = Quaternion.LookRotation(dir.normalized);
        }
    }
}