using Magic.Boss;
using Magic.Interact;
using Magic.Inventory;
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
        [Header("Player")]
        public ThirdPersonController ThirdPersonController => _thirdPersonController;
        public PlayerInteraction InteractionSystem => _interactionSystem;
        public HealthSystem PlayerHealthSystem => _playerHealth;
        public PlayerFight PlayerFight => _playerFight;
        [Header("Bosses")]                               
        public Boss1Fight Boss1Fight => _boss1Fight;                                                                                                                                    
        [Header("UI")]
        public InventoryManager InventoryManager => _inventoryManager;
        public UIGameController UIGameController => _uiController;
        public ActivatePausePanel ActivatePausePanel => _activatePausePanel;
        [Header("Scene Objects")]
        public TriggerFight TriggerFight => _triggerFight;
        public CameraController CameraController => _cameraController;

        #endregion

        #region Fields
        [Header("Player")]
        [SerializeField] private ThirdPersonController _thirdPersonController;
        [SerializeField] private PlayerInteraction _interactionSystem;
        [SerializeField] private HealthSystem _playerHealth;
        [SerializeField] private PlayerFight _playerFight;
        [Header ("Bosses")]
        [SerializeField] private Boss1Fight _boss1Fight;
        [Header("UI")]
        [SerializeField] private InventoryManager _inventoryManager;
        [SerializeField] private UIGameController _uiController;
        [SerializeField] private ActivatePausePanel _activatePausePanel;
        [Header("Scene Objects")]
        [SerializeField] private TriggerFight _triggerFight;
        [SerializeField] private CameraController _cameraController;
        #endregion

        #region Unity Callbacks
        private void Start()
        {

            _interactionSystem.OnShowInteraction += msg => OnShowInteraction?.Invoke(msg);
            _interactionSystem.OnHideInteraction += () => OnHideInteraction?.Invoke();

            _triggerFight.OnStartFight += _uiController.ShowFightPanel;
            _triggerFight.OnStartFight += _playerFight.Fight;

            _activatePausePanel.OnGamePause += () =>
            {
                _thirdPersonController.SetBlocked(true);
                _cameraController.SetBlocked(true);
                if(_boss1Fight != null)
                    _boss1Fight.SetBlocked(true);
                Cursor.visible = true;

              

            };
            _activatePausePanel.OnGameResume += () =>
            {
                _thirdPersonController.SetBlocked(false);
                _cameraController.SetBlocked(false);
                if(_boss1Fight != null)
                    _boss1Fight.SetBlocked(false);
                Cursor.visible = false;

            };


            

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
