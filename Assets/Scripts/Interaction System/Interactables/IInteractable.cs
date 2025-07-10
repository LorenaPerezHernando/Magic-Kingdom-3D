using UnityEngine;

namespace Magic.Interact
{
    public interface IInteractable
    {
        void Interact();
        InteractableInfo GetInfo();
        
    }
}
