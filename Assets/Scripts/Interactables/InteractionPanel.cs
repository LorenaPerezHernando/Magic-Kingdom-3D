using TMPro;
using UnityEngine;

namespace Magic.UI
{
    public class InteractionPanel : MonoBehaviour
    {
        private TextMeshProUGUI _textInteraction;
        private void Awake()
        {
            _textInteraction = GetComponentInChildren<TextMeshProUGUI>();
        }
        void Start()
        {
            gameObject.SetActive(false);
        }

        #region Public Methods
        public void Show(string message)
        {
            gameObject.SetActive(true);
            _textInteraction.text = message;
        }

        internal void Hide()
        {
            gameObject.SetActive(false);

        }
        #endregion
    }
}
