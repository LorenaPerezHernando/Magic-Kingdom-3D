using Magic.Boss;
using Magic.Interact;
using Magic.UI;
using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Magic
{
    public class GameController : Singleton<GameController>
    {
        #region Properties
        public event Action<string> OnShowInteraction;
        public event Action OnHideInteraction;

        public event Action OnPush;
        [Header("Scene Objects")]
        public TriggerFight TriggerFight => _triggerFight;
        [Header("Player")]
        public PlayerInteraction InteractionSystem => _interactionSystem;
        public HealthSystem PlayerHealthSystem => _playerHealth;
        public PlayerFight PlayerFight => _playerFight;
        [Header("UI")]
        public UIGameController UIGameController => _uiController;
        #endregion

        #region Fields
        [Header("Player")]
        [SerializeField] private PlayerInteraction _interactionSystem;
        [SerializeField] private HealthSystem _playerHealth;
        [SerializeField] private PlayerFight _playerFight;
        [Header("UI")]
        [SerializeField] private UIGameController _uiController;
        [Header("Scene Objects")]
        [SerializeField] private TriggerFight _triggerFight;
        #endregion

        #region Unity Callbacks
        private void Start()
        {
            _interactionSystem.OnShowInteraction += msg => OnShowInteraction?.Invoke(msg);
            _interactionSystem.OnHideInteraction += () => OnHideInteraction?.Invoke();

            _triggerFight.OnStartFight += _uiController.ShowFightPanel;
            _triggerFight.OnStartFight += _playerFight.Fight;

            if (_playerHealth != null)
            {
                _playerHealth.OnHealthChanged += _uiController.UpdatePlayerHealth;
                _playerHealth.OnDeath += _uiController.ShowDeathPanel;
            }

            
        }
        #endregion

        #region Public Methods

        public void TriggerPush()
        {
            OnPush?.Invoke();
        }

        #endregion
    }
}
