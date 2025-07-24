using Magic.Boss;
using Magic.Data;
using Magic.Interact;
using Magic.Inventory;
using Magic.UI;
using System;
using System.Collections;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Magic.Data.SaveData;
using SaveData = Magic.Data.SaveData;

namespace Magic
{
    public class GameController : Singleton<GameController>
    {
        #region Properties
        public event Action<string> OnShowInteraction;
        public event Action OnHideInteraction;

        public event Action OnPush;
        [Header("Save System")]
        public GameProgress GameProgress => _gameProgress;
        public SaveData SaveData => _saveData;

        [Header("Player")]
        public ThirdPersonController ThirdPersonController => _thirdPersonController;
        public PlayerInteraction InteractionSystem => _interactionSystem;
        public HealthSystem PlayerHealthSystem => _playerHealth;
        public PlayerFight PlayerFight => _playerFight;
        [Header("Bosses")]                               
        public Boss1Fight Boss1Fight => _boss1Fight;  
        public HealthSystem BossHealth => _bossHealth;                                                                                                                                  
        [Header("UI")]                                                                           
        public InventoryManager InventoryManager => _inventoryManager;
        public UIGameController UIGameController => _uiController;
        public ActivatePausePanel ActivatePausePanel => _activatePausePanel;
        [Header("Scene Objects")]
        public TriggerFight TriggerFight => _triggerFight;
        public CameraController CameraController => _cameraController;

        #endregion

        #region Fields
        [Header("Save System")]
        [SerializeField] private GameProgress _gameProgress;
        [SerializeField] private SaveData _saveData;
        [Header("Player")]
        [SerializeField] private ThirdPersonController _thirdPersonController;
        [SerializeField] private PlayerInteraction _interactionSystem;
        [SerializeField] private HealthSystem _playerHealth;
        [SerializeField] private PlayerFight _playerFight;
        [Header ("Bosses")]
        [SerializeField] private Boss1Fight _boss1Fight;
        [SerializeField] private HealthSystem _bossHealth;
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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        
            _interactionSystem.OnShowInteraction += msg => OnShowInteraction?.Invoke(msg);
            _interactionSystem.OnHideInteraction += () => OnHideInteraction?.Invoke();

            _triggerFight.OnStartFight += _uiController.ShowFightPanel;
            _triggerFight.OnStartFight += _playerFight.Fight;

            _activatePausePanel.OnGamePause += () =>
            {
                _thirdPersonController.SetBlocked(true);
                _cameraController.SetBlocked(true);
                _playerHealth.SetBlocked(true);
                _playerFight.SetBlocked(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                _boss1Fight.SetBlocked(true);
              

            };
            _activatePausePanel.OnGameResume += () =>
            {
                _thirdPersonController.SetBlocked(false);
                _cameraController.SetBlocked(false);
                _playerHealth.SetBlocked(false);
                _playerFight.SetBlocked(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked ;

                _boss1Fight.SetBlocked(false);
            };


            

            if (_playerHealth != null)
            {
                _playerHealth.OnHealthChanged += _uiController.UpdatePlayerHealth;
                _playerHealth.OnDeath += _uiController.ShowDeathPanel;

                _playerFight.OnHeal += _playerHealth.Heal;
            }

            if(_bossHealth != null)
            {
                _bossHealth.OnHealthChanged += _uiController.UpdateBossHealth;
                _bossHealth.OnDeath += _uiController.VictoryOnFightWithBoss1;
            }



            

            
        }
        #endregion

        #region Public Methods
        public void TriggerPush()
        {
            OnPush?.Invoke();
        }

        #endregion
        #region GameProgress
        public void SavePosition(Vector3 position)
        {
            SaveData saveData = SaveSystem.Load() ?? new SaveData();

            saveData.player = new PlayerSaveData
            {
                position = position
            };

            SaveSystem.Save(saveData);
            Debug.Log("Checkpoint guardado por GameController.");
        }

        public void AddSpirit()
        {
            _gameProgress.spirits++;
            SaveGame(); 
        }

        public void CompletePuzzle()
        {
            _gameProgress.puzzlesCompleted++;
            SaveGame();
        }

        public void DefeatBoss()
        {
            _gameProgress.bossesDefeated++;
            SaveGame();
        }

        public void FinishedVillage()
        {
            _gameProgress.villages++;
            SaveGame();
        }
        #endregion

        #region Load And Save
        public void NewGame()
        {
            _saveData = new SaveData();
            //TODO 
            //_thirdPersonController.transform.position = POSICION INICIAL
        }
        public void LoadGame()
        {
            _saveData = SaveSystem.Load();

            if (SceneManager.GetActiveScene().buildIndex != _saveData.scene)
            {
                // Puedes guardar temporalmente el SaveData y cargar escena, luego aplicar
                StartCoroutine(LoadSceneAndApplySave(_saveData));
                return;
            }

            // Ya estás en la escena correcta, aplica los datos directamente
            ApplySaveData(_saveData);

            // PLAYER
            _thirdPersonController.transform.position = _saveData.player.position;

            // PROGRESS
            _gameProgress.spirits = _saveData.progress.spirits;
            _gameProgress.villages = _saveData.progress.villages;
            _gameProgress.puzzlesCompleted = _saveData.progress.puzzles;
            _gameProgress.bossesDefeated = _saveData.progress.bosses;

            // INVENTORY
            _gameProgress.healingPlants = _saveData.inventory.healingPlants;
        }
        public void SaveGame()
        {
            _saveData = new SaveData();
            //ESCENA
            _saveData.scene = SceneManager.GetActiveScene().buildIndex;
            // PLAYER
            _saveData.player = new PlayerSaveData
            {
                position = _thirdPersonController.transform.position
            };

            // PROGRESS
            _saveData.progress = new GameProgressData
            {
                spirits = _gameProgress.spirits,
                villages = _gameProgress.villages,
                puzzles = _gameProgress.puzzlesCompleted,
                bosses = _gameProgress.bossesDefeated
            };

            // INVENTORY
            _saveData.inventory = new InventoryData
            {
                healingPlants = _gameProgress.healingPlants
            };

            SaveSystem.Save(_saveData);
        }

        private IEnumerator LoadSceneAndApplySave(SaveData saveData)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(saveData.scene);

            while (!asyncLoad.isDone)
                yield return null;

            yield return new WaitForSeconds(0.1f); // Pequeño delay por seguridad

            ApplySaveData(saveData);
        }
        private void ApplySaveData(SaveData saveData)
        {
            _thirdPersonController.transform.position = saveData.player.position;

            _gameProgress.spirits = saveData.progress.spirits;
            _gameProgress.villages = saveData.progress.villages;
            _gameProgress.puzzlesCompleted = saveData.progress.puzzles;
            _gameProgress.bossesDefeated = saveData.progress.bosses;

            _gameProgress.healingPlants = saveData.inventory.healingPlants;
        }


       

        #endregion
    }
}
