namespace Mechanics.Interactable
{
    public abstract class BaseInteractable : Mirror.NetworkBehaviour, IInteractable
    {
        public float TimeToUse { get; protected set; } = 4f;
        public bool IsScrap { get; protected set; } = false;

        public virtual void Interact()
        {

        }
    }
}