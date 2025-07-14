using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace Magic.UI
{
    public class ActivatePausePanel : MonoBehaviour
    {
        [SerializeField] private string _inputKey;
        [SerializeField] private GameObject _objectToActivate;
        [SerializeField] private bool _isActive = false;



        private void Update()
        {
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), _inputKey.ToString().ToUpper());
            if (Input.GetKeyUp(key))
            {
                ActivateChild();
            }
        }

        private void ActivateChild()
        {
            print("Activate Button");
            _isActive = !_isActive;
            _objectToActivate.SetActive(_isActive);
        }
    }
}
