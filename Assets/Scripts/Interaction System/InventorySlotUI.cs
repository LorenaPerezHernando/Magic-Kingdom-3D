using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Magic.Inventory;

namespace Magic.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;

        public void Set(Item item)
        {
            _icon.sprite = item.icon;
            _nameText.text = item.itemName;
        }
    }
}
