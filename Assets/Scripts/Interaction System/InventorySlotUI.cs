using Magic.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Magic.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        private Item _item;

        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Button _useButton;

        public void Set(Item item)
        {
            _item = item;
            _nameText.text = item.itemName;
            _icon.sprite = item.icon;

            _useButton.onClick.RemoveAllListeners();
            _useButton.onClick.AddListener(() => InventoryManager.Instance.UseItem(item));
            Debug.Log("Creado: " +  item.name);
        }
    }
}
