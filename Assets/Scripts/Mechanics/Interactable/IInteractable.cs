using UnityEngine;

namespace Mechanics.Interactable
{
    public interface IInteractable
    {
        float TimeToUse { get; }
        void Interact(GameObject playerObject);
    }
}