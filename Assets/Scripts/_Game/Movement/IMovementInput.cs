namespace _Game.Movement
{
    public interface IMovementInput
    {
        
        MovementData MovementData { get; }
        void ReadInput();
    }
}