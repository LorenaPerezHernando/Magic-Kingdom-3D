using System;
using UnityEngine;

namespace Magic.Interaction
{
    public class InteractionSystem : MonoBehaviour
    {
        #region Properties
        public event Action<string> OnShowInteraction;
        public event Action OnHideInteraction;
        #endregion

        private bool _interactebleDetected = false;
        private IInteractable _currentInteraction;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_interactebleDetected && Input.GetKeyDown(KeyCode.E))
            {
                _currentInteraction?.Interact(); // o lo que sea que haga
                Debug.Log("Interaction performed");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            IInteractable interaction = other.GetComponent<IInteractable>();
            if (interaction != null)
            {
                _currentInteraction = interaction;
                InteractableInfo info = interaction.GetInfo();
                OnShowInteraction?.Invoke("E - To " + info.Action + " " + info.Type);
                _interactebleDetected = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IInteractable interaction = other.GetComponent<IInteractable>();
            if (interaction != null && interaction == _currentInteraction)
            {
                _currentInteraction = null;
                _interactebleDetected = false;
                OnHideInteraction?.Invoke();
            }
        }
    }
}
