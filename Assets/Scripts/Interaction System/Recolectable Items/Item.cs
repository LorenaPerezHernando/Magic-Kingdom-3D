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
        public string itemName;
        public ItemType itemType;
        public Sprite icon;
        public GameObject prefab;
        [TextArea] public string description;
        public int maxStack = 10;
    }
}