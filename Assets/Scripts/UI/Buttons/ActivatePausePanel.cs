using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Magic.UI
{
    public class ActivatePausePanel : MonoBehaviour
    {
        public event Action OnGamePause;
        public event Action OnGameResume;

        [SerializeField] private string _inputKey;
        [SerializeField] private GameObject _objectToActivate;
        [SerializeField] private bool _isActive = false;

        [SerializeField] private List<GameObject> _panelsToManage = new List<GameObject>();

        private void Awake()
        {

        }



        private void Update()
        {
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), _inputKey.ToString().ToUpper());
            if (Input.GetKeyUp(key))
            {
                ActivateChild();
            }
        }

        public void ActivateChild()
        {
            //TODO AUDIO DE ABRIR
            print("Activate Button");
            bool newState = !_objectToActivate.activeSelf;
            _objectToActivate.SetActive(newState);

            if (newState)
            {
                OnGamePause?.Invoke();
            }

            else
                OnGameResume?.Invoke();
            

            print("Activate Button: " + (newState ? "OPEN" : "CLOSE"));


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
    }
}
