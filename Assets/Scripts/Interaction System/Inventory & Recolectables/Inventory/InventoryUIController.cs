using Magic.UI;
using UnityEngine;

namespace Magic.Inventory
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private Transform _contentParent;

        private void Start()
        {
            GameController.Instance.InventoryManager.OnItemAdded += _ => RefreshUI();
            GameController.Instance.InventoryManager.OnItemUsed += RefreshUI;

            foreach (var item in GameController.Instance.InventoryManager.Items)
            {
                Debug.Log("Instanciando UI para: " + item.item.ItemName);
                CreateSlot(item);
            }
        }

        private void CreateSlot(InventoryQuantity itemQuantity)
        {
            Debug.Log("Creando slot para: " + itemQuantity.item.ItemName);
            GameObject slotGO = Instantiate(_slotPrefab, _contentParent);
            slotGO.GetComponent<InventorySlotUI>().Set(itemQuantity);
        }

        public void RefreshUI()
        {
            foreach (Transform child in _contentParent)
                Destroy(child.gameObject);

            foreach (var itemQuantity in GameController.Instance.InventoryManager.Items)
            {
                CreateSlot(itemQuantity);
            }
        }
    }
}
