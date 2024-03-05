namespace Mechanics.Interactable
{
    public abstract class BaseInteractable : Mirror.NetworkBehaviour, IInteractable
    {
        public short TimeToUse = 4;

        public virtual void Interact()
        {

        }
    }
}