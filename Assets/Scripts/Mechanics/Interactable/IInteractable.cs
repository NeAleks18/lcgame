namespace Mechanics.Interactable
{
    public interface IInteractable
    {
        float TimeToUse { get; }
        bool IsScrap { get; }
        void Interact();
    }
}