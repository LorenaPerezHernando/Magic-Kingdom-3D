using Magic.Interaction;
using Magic.UI;
using UnityEngine;
namespace Magic.UI
{ 
    public class UIGameController : MonoBehaviour
    {
         private InteractionSystem _interactionSystem => GameController.Instance?.InteractionSystem;
        [Header("Interaction")]
        [SerializeField] private InteractionPanel _interactionPanel;
        void Start()
        {
            Debug.Log("UIGameController iniciado");

            if (_interactionSystem == null)
            {
                Debug.LogError("No se encontró InteractionSystem");
                return;
            }

            if (_interactionPanel == null)
            {
                Debug.LogError("No se encontró InteractionPanel");
                return;
            }
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
