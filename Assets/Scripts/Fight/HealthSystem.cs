using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace Magic
{
    public class HealthSystem : MonoBehaviour
    {
        #region Fields & Properties
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _currentHealth;
        public event Action <float> OnHealthChanged;
        public event Action OnDeath;

        public bool isBlocked = false;
        #endregion
        #region Unity CallBacks
        void Awake()
        {
            _currentHealth = _maxHealth;
        }
        #endregion
        #region Public Methods
        public void SetBlocked(bool value)
        {
            isBlocked = value;
            
        }
        public void TakeDamage(float damageAmount)
        {
            if (isBlocked) return;
            _currentHealth -= damageAmount;
            OnHealthChanged?.Invoke(_currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
        public void Heal(float amount)
        {
            if (isBlocked) return;
            _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
            OnHealthChanged.Invoke(_currentHealth);
            Debug.Log("Se ha curado: " + amount);
        }

        public void SetHealth(float value)
        {
            if (isBlocked) return;
            _currentHealth = value;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth);
        }
        #endregion

        #region Private Methods
        void Die()
        {
            if(gameObject.CompareTag("Boss"))
            {
                Animator _anim = GetComponent<Animator>();
                _anim.SetTrigger("Die");
                //TODO Death Particle
                OnDeath.Invoke();
                StartCoroutine(BossDied());
            }
            if (gameObject.CompareTag("Player"))
            {
                Animator _anim = GetComponent<Animator>();
                _anim.SetTrigger("Die");
                //TODO logica de morirse del player
                //¿Tienes objetos para revivir? == Revivir
                //Todo panel de muerte
            }
            
            //Destroy(gameObject);
        }


        internal void RevivedHealth()
        {
            if (isBlocked) return;
            OnHealthChanged?.Invoke(_maxHealth);
            _currentHealth = _maxHealth;
        }

        IEnumerator BossDied()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

       
        #endregion
    }
}

