
using _Game.Movement;
using _Game.Movement.Inputs;
using _Game.Movement.Motors;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private CharacterController cc;

    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromInstance(joystick).AsSingle();
        Container.BindInterfacesTo<JoystickInput>().AsSingle();
        Container.BindInterfacesTo<CCMotor>().AsSingle().WithArguments(cc,GameManager.Instance.GameData.playerSpeed);
    }
}