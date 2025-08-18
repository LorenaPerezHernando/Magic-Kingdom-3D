using Magic.Interact;
using System;
using UnityEngine;

namespace Magic.ClockPuzzle
{
    public class PuzzleClock : MonoBehaviour, IInteractable
    {
        #region Fields & Properties
        [SerializeField] private InteractableInfo _info;
        [Header("Mecanic")]
        [SerializeField] private float _rotationSpeed = 2f;
        [SerializeField] private float _rotationAmount = 30;
        [Header("Goal")]
        [SerializeField] private GameObject[] _objectToDesactivate;
        private Transform _objectToRotate;
        [SerializeField] private GameObject _targetPos;
        [Header("Rotation")]
        [SerializeField] private bool _isRotating = false;
        private float _rotationProgress = 0f;
        private float _startAngle;
        private float _targetAngle;
        #endregion
        #region Unity Callbacks

        private void Awake()
        {
            _objectToRotate = transform.parent;
        }
        #endregion
        #region Public Methods


        public void Interact()
        {
            Debug.Log("Interacting");
            if (!_isRotating)
            {
                _startAngle = _objectToRotate.eulerAngles.y;
                _targetAngle = _startAngle + _rotationAmount;
                _isRotating = true;

                GameController.Instance.TriggerPush();
                GameController.Instance.AudioController.PlayMovingClock();
            }

        }
        public InteractableInfo GetInfo()
        {
            return _info;
        }
        #endregion
        #region Unity Callbacks
        private void Update()
        {
            
            if (_isRotating)
            {
                print("is Rotating");
                _rotationProgress += Time.deltaTime * _rotationSpeed;
                float currentY = Mathf.LerpAngle(_startAngle, _targetAngle, Time.deltaTime * _rotationSpeed);
                _objectToRotate.rotation = Quaternion.Euler(0, currentY, 0);

               

                if (_rotationProgress >= 1f)
                {
                    _objectToRotate.rotation = Quaternion.Euler(0, _targetAngle, 0);
                    _isRotating = false;
                    _rotationProgress = 0f;
                }

            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject == _targetPos)
            {
                if(_objectToDesactivate != null)
                {
                    foreach (var obj in _objectToDesactivate)
                    {
                        if (obj != null)
                            Destroy(obj);
                    }
                }
                    
                //TODO Particulas, sonido de completado
                GameController.Instance.CompletePuzzle();
                GameController.Instance.AudioController.PlayProgressSound();
                GameController.Instance.AudioController.PlayClock();
                Destroy(gameObject);
            }
        }
        #endregion
    }
}
