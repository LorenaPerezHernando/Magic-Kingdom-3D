using Magic.Interaction;
using Magic.UI;
using UnityEngine;
namespace Magic.UIController 
{ 
    public class UIGameController : MonoBehaviour
    {
        private InteractionSystem _interactionSystem => GameController.Instance?.InteractionSystem;
        [Header("Interaction")]
        [SerializeField] private InteractionPanel _interactionPanel;
        void Start()
        {
            _interactionSystem.OnShowInteraction += ShowInteraction;
            _interactionSystem.OnHideInteraction += HideInteraction;
        }

        // Update is called once per frame
        void Update()
        {

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
