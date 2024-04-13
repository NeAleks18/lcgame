using Mirror;

namespace Mechanics.Interactable
{
    public abstract class BaseInteractable : NetworkBehaviour, IInteractable
    {
        public float TimeToUse { get; protected set; } = 4f;
        public string Type { get; protected set; } = "";


        [ClientRpc]
        public virtual void Interact()
        {

        }
    }
}