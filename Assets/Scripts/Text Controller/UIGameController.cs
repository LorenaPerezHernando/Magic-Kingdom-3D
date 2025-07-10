using Magic;
using UnityEngine;
using Magic.Interact;

namespace Magic.UI
{


    public class UIGameController : MonoBehaviour
    {
        #region Fields
        private PlayerInteraction _interactionSystem => GameController.Instance.InteractionSystem;

        [Header("Interaction")]
        [SerializeField] private UIInteraction _interactionPanel;
        #endregion

        private void Awake()
        {
            _interactionPanel = GetComponentInChildren<UIInteraction>();
        }
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
        #endregion
    }
}

