namespace Interactor
{
    public interface IInteractable
    {
        void Interact(Interactor interactor);
        void StopInteract(Interactor interactor);
    }
}