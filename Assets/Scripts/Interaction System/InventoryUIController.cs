using Magic;
using Magic.Inventory;
using Magic.UI;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _contentParent;

    private void Start()
    {
        GameController.Instance.InventoryManager.OnItemAdded += CreateSlot;
        GameController.Instance.InventoryManager.OnItemUsed += RefreshUI;

        foreach (var item in GameController.Instance.InventoryManager.Items)
        {
            Debug.Log("Instanciando UI para: " + item.itemName);
            CreateSlot(item);
        }
    }

    private void CreateSlot(Item item)
    {
        Debug.Log("Creando slot para: " + item.itemName);
        GameObject slotGO = Instantiate(_slotPrefab, _contentParent);
        slotGO.GetComponent<InventorySlotUI>().Set(item);
    }

    public void RefreshUI()
    {
        foreach (Transform child in _contentParent)
            Destroy(child.gameObject);

        foreach (var item in GameController.Instance.InventoryManager.Items)
        {
            CreateSlot(item);
        }
    }
}
