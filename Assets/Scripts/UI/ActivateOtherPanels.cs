using System.Collections.Generic;
using UnityEngine;

namespace Magic.UI
{


    public class ActivateOtherPanels : MonoBehaviour
    {
        [System.Serializable]
        public class PanelKeyPair
        {
            [SerializeField] private int _keyNumber;
            [SerializeField] private GameObject _panelObject;

            public int keyNumber => _keyNumber;
            public GameObject panelObject => _panelObject;
        }

        [SerializeField] private List<PanelKeyPair> _panelList = new List<PanelKeyPair>();

        private void Update()
        {
            foreach (var panelPair in _panelList)
            {
                if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + panelPair.keyNumber)))
                {
                    ActivateOnlyPanelWithKey(panelPair.keyNumber);
                    break; 
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DeactivateAllPanels();
            }
        }

        private void ActivateOnlyPanelWithKey(int activeKey)
        {
            foreach (var panel in _panelList)
            {
                if (panel.panelObject != null)
                    panel.panelObject.SetActive(panel.keyNumber == activeKey);
            }

        }

        private void DeactivateAllPanels()
        {
            foreach (var panel in _panelList)
            {
                if (panel.panelObject != null)
                    panel.panelObject.SetActive(false);
            }

        }
    }
}
