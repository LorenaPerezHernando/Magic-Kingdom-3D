using Magic.Interact;
using Magic.Inventory;
using UnityEngine;

namespace Magic.Recolectable
{

    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _amount = 1;
        [SerializeField] private InteractableInfo _info;

        public void Interact()
        {
            GameController.Instance.InventoryManager.AddItem(_item, _amount);
            Destroy(gameObject);
        }

        public InteractableInfo GetInfo() => _info;
    }
    
}
