using TMPro;
using UnityEngine;

namespace Magic.UI
{


    public class UIInteraction : MonoBehaviour
    {
        #region Fields
        [SerializeField] private TextMeshProUGUI _textPanel;
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            _textPanel = GetComponentInChildren<TextMeshProUGUI>();
        }
        void Start()
        {
            gameObject.SetActive(false);
        }


        #endregion

        #region Public Methods
        public void Show(string message)
        {
            gameObject.SetActive(true);
            _textPanel.text = message;
        }

        internal void Hide()
        {
            gameObject.SetActive(false);

        }
        #endregion

    }
}
