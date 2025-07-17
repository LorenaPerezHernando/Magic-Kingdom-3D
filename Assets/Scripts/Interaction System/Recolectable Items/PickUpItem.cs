using Magic.Interact;
using Magic.Inventory;
using UnityEngine;

namespace Magic.Recolectable
{

    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item _item;
        [SerializeField] private InteractableInfo _info;

        public void Interact()
        {
            GameController.Instance.InventoryManager.AddItem(_item);
            Destroy(gameObject);
        }

        public InteractableInfo GetInfo() => _info;
    }
    
}
