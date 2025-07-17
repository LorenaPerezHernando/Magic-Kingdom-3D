using UnityEngine;

namespace Magic.Inventory
{
    public enum ItemType
    {
        Default,
        Healing,
        Research,
        Attack
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "Magic/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _itemName;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
        [TextArea][SerializeField] private string _description;
        [SerializeField] private int _maxStack = 10;

        public int ID => _id;
        public string ItemName => _itemName;
        public ItemType ItemType => _itemType;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;
        public string Description => _description;
        public int MaxStack => _maxStack;
    }
}
