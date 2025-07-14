using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
namespace Magic
{
    public class HealthSystem : MonoBehaviour
    {
        public float _maxHealth = 100f;
        private float _currentHealth;
        public UnityEvent<float> OnHealthChanged;
        public UnityEvent OnDeath;
        void Awake()
        {
            _currentHealth = _maxHealth;
        }
        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;
            OnHealthChanged.Invoke(_currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
        public void Heal(float amount)
        {
            _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
            OnHealthChanged.Invoke(_currentHealth);
        }
        void Die()
        {
            OnDeath.Invoke();
            Destroy(gameObject);
        }
    }
}

