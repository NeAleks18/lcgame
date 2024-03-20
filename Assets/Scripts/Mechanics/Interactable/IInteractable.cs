namespace Mechanics.Interactable
{
    public interface IInteractable
    {
        float TimeToUse { get; }
        string Type { get; }
        void Interact();
    }
}