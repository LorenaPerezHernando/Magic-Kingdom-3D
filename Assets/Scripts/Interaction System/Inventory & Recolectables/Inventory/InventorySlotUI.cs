using Magic.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Magic.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        private InventoryQuantity _item;

        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private Button _useButton;

        public void Set(InventoryQuantity data)
        {
            _item = data;
            _nameText.text = data.item.ItemName;
            _icon.sprite = data.item.Icon;
            _quantityText.text = data.quantity.ToString();

            _useButton.onClick.RemoveAllListeners();
            _useButton.onClick.AddListener(() => InventoryManager.Instance.UseItem(_item.item));

            Debug.Log("Creado: " + data.item.ItemName);
        }
    }
}
