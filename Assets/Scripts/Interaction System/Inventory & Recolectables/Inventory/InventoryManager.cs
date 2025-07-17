using System;
using System.Collections.Generic;
using UnityEngine;
using Magic.Recolectable;

namespace Magic.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public event Action<InventoryQuantity> OnItemAdded;
        public event Action OnItemUsed;
        public static InventoryManager Instance { get; private set; }

        [SerializeField] private List<InventoryQuantity> _items = new List<InventoryQuantity>();

        public IReadOnlyList<InventoryQuantity> Items => _items;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void AddItem(Item newItem, int amount = 1)
        {
            var existing = _items.Find(i => i.item.ID == newItem.ID);

            if (existing != null)
            {
                existing.quantity = Mathf.Min(existing.quantity + amount, newItem.MaxStack);
            }
            else
            {
                _items.Add(new InventoryQuantity(newItem, amount));
            }

            OnItemAdded?.Invoke(existing ?? new InventoryQuantity(newItem, amount));
        }

        public void UseItem(Item item)
        {
            switch (item.ItemType)
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

            RemoveItem(item);
            OnItemUsed?.Invoke();
        }

        public void RemoveItem(Item item, int amount = 1)
        {
            var existing = _items.Find(i => i.item == item);
            if (existing != null)
            {
                existing.quantity -= amount;

                if (existing.quantity <= 0)
                {
                    _items.Remove(existing);
                }

                OnItemUsed?.Invoke(); // Esto puedes cambiarlo a OnItemRemoved si prefieres más claridad
            }
        }
    }
}
