using Magic.Interact;
using System;
using UnityEngine;

namespace Magic
{
    public class GameController : Singleton<GameController>
    {
        #region Properties
        [Header("Interaction")]
        public PlayerInteraction InteractionSystem => _interactionSystem;
        public event Action<string> OnShowInteraction;
        public event Action OnHideInteraction;

        public event Action OnPush;
        #endregion
        [SerializeField] private PlayerInteraction _interactionSystem;

        
        private void Start()
        {
            _interactionSystem.OnShowInteraction += msg => OnShowInteraction?.Invoke(msg);
            _interactionSystem.OnHideInteraction += () => OnHideInteraction?.Invoke();
        }

        public void TriggerPush()
        {
            OnPush?.Invoke();
        }
    }
}
