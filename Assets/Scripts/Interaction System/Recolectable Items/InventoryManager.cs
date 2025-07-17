using System;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public event Action<Item> OnItemAdded;
        public event Action OnItemUsed;
        public static InventoryManager Instance { get; private set; }

        [SerializeField] private List<Item> _items = new List<Item>();

        public IReadOnlyList<Item> Items => _items;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
            Debug.Log($"Item añadido al inventario: {item.itemName}");
            OnItemAdded?.Invoke(item);
        }

        public void UseItem(Item item)
        {
            switch (item.itemType)
            {
                case ItemType.Attack:
                    Debug.Log("¡Ataque aumentado!");
                    break;
                case ItemType.Healing:
                    Debug.Log(" ¡Vida restaurada!");
                    GameController.Instance.PlayerHealthSystem.Heal(20f);
                    break;
                case ItemType.Research:
                    Debug.Log(" Conocimiento adquirido!");
                    break;
                default:
                    Debug.Log("Objeto sin función definida.");
                    break;
            }

            _items.Remove(item);
            OnItemUsed?.Invoke();
        }
    }
}
