using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Magic.UI
{
    public class ActivatePausePanel : MonoBehaviour
    {
        #region Fields & Propertoes
        public event Action OnGamePause;
        public event Action OnGameResume;
        [Header("Main Panel to Open")]
        [SerializeField] private string _inputKey;
        [SerializeField] private GameObject _objectToActivate;
        [Header("Other Panels")]
        [SerializeField] private List<GameObject> _panelsToManage = new List<GameObject>();
        #endregion
        #region Unity Callbacks
        private void Update()
        {
            //KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), _inputKey.ToString().ToUpper());
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ActivateChild();
            }
        }
        #endregion
        #region Public Methods
        public void ActivateChild()
        {
            //TODO AUDIO DE ABRIR
            //print("Activate Button");
            bool newState = !_objectToActivate.activeSelf;
            _objectToActivate.SetActive(newState);

            if (newState)
            {
                OnGamePause?.Invoke();
            }

            else
                OnGameResume?.Invoke();
            

            //print("Activate Button: " + (newState ? "YES" : "NO"));


        }
        public void HideAll()
        {
            //TODO AUDIO DE CERRAR
            foreach (var panel in _panelsToManage)
            {
                if (panel != null)
                    panel.SetActive(false);
            }
        }
        #endregion
    }
}
