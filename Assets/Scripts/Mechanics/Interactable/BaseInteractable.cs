using Mirror;
using UnityEngine;

namespace Mechanics.Interactable
{
    public abstract class BaseInteractable : NetworkBehaviour, IInteractable
    {
        public float TimeToUse { get; protected set; } = 4f;
        public string Type { get; protected set; } = "";


        public virtual void Interact(GameObject playerObject)
        {

        }
    }
}