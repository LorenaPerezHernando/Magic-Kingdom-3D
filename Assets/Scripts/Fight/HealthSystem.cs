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
            if (gameObject.CompareTag("Boss"))
            {
                Animator _anim = GetComponent<Animator>();
                _anim.SetTrigger("Receive Hit");
            }
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
            GameController.Instance.AudioController.PlayPlayerHealingPower();
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
                Boss1Fight bossFight = GetComponent<Boss1Fight>();
                bossFight.enabled = false;
                _anim.SetTrigger("Die");
                //TODO Death Particle
                OnDeath.Invoke();
                GameController.Instance.DefeatBoss();
                StartCoroutine(BossDied());
            }
            if (gameObject.CompareTag("Player"))
            {
                Animator _anim = GetComponent<Animator>();
                _anim.SetTrigger("Die");
                GameController.Instance.PlayerDied();

                // TODO ¿Tienes objetos para revivir? == Revivir
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
            GameController.Instance.LoadScene(2); 
            Debug.Log("Boss Died, do rewards");
        }

       
        #endregion
    }
}

