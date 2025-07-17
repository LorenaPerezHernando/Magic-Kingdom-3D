using System;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public event Action<Item> OnItemAdded;
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
    }
}
