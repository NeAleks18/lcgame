using Mirror;
using UnityEngine;

namespace Mechanics.Interactable
{
    public abstract class BaseInteractable : NetworkBehaviour, IInteractable
    {
        public float TimeToUse { get; protected set; } = 4f;


        public virtual void Interact(GameObject playerObject)
        {

        }
    }
}