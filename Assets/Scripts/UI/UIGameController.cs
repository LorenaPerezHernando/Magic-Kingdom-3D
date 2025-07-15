using Magic;
using UnityEngine;
using Magic.Interact;
using UnityEngine.UI;

namespace Magic.UI
{


    public class UIGameController : MonoBehaviour
    {
        #region Fields
        private PlayerInteraction _interactionSystem => GameController.Instance.InteractionSystem;

        [Header("Interaction")]
        [SerializeField] private UIInteraction _interactionPanel;

        [Header("Fight")]
        [SerializeField] private Slider _playerSlider;
        [SerializeField] private Slider _bossSlider;
        [SerializeField] private GameObject _fightPanel;
        [SerializeField] private GameObject _deathPanel;
        

        #endregion

        void Start()
        {
            _interactionSystem.OnShowInteraction += ShowInteraction;
            _interactionSystem.OnHideInteraction += HideInteraction;

            _deathPanel.SetActive(false);
            _fightPanel.SetActive(false);
        }


        #region Public Methods
        public void ShowInteraction(string message)
        {
            _interactionPanel.Show(message);
        }
        public void HideInteraction()
        {
            _interactionPanel.Hide();

        }

        public void ShowDeathPanel()
        {
            _deathPanel.SetActive(true);
        }

        public void ShowFightPanel()
        {
            _fightPanel.SetActive(true);
        }
        public void HideFightPanel()
        {
            _fightPanel.SetActive(false);
        }
        #endregion

        #region Private Methods
        internal void UpdatePlayerHealth(float value)
        {
            _playerSlider.value = value;
        }

        internal void UpdateBossHealth(float value)
        {
            _bossSlider.value = value;
        }
        #endregion
    }
}

