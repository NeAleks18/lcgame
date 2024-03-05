namespace Mechanics.Interactable
{
    public abstract class BaseInteractable : Mirror.NetworkBehaviour, IInteractable
    {
        public float TimeToUse { get; protected set; } = 4f;

        public virtual void Interact()
        {

        }
    }
}