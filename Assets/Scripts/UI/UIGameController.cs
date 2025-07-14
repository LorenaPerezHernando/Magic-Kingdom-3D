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

        [Header("UI")]
        [SerializeField] private Slider _playerSlider;
        [SerializeField] private GameObject _fightPanel;

        #endregion

        void Start()
        {

            _interactionSystem.OnShowInteraction += ShowInteraction;
            _interactionSystem.OnHideInteraction += HideInteraction;
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
        #endregion
    }
}

