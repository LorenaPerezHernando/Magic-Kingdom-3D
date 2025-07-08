using UnityEngine;

namespace Magic.ClockPuzzle
{


    public class PuzzleClock : MonoBehaviour
    {

        [SerializeField] private Transform _targetToRotate;
        [Header("Mecanic")]
        [SerializeField] private float _rotationSpeed = 2f;
        [SerializeField] private float _rotationAmount = 30;
        [Header("Goal")]
        [SerializeField] private float _target1 = 7;
        [SerializeField] private float _target2 = 370;
        [Header("Rotation")]               
        private bool _isRotating = false;       
        private float _startAngle;
        private float _targetAngle;

        private void Awake()
        {
            _targetToRotate = transform.parent;
        }


        public void Interact()
        {
            Debug.Log("Interacting");
            
        }
        private void Update()
        {
            if (Mathf.Abs(Mathf.DeltaAngle(_targetToRotate.eulerAngles.y, _target1)) < 1f ||
                    Mathf.Abs(Mathf.DeltaAngle(_targetToRotate.eulerAngles.y, _target2)) < 1f)
            {
                //TODO OBJETIVO CUMPLIDO
                Debug.Log("Objetivo Cumplido 1");
                Destroy(gameObject);
            }
            if (_isRotating)
            {
                float newY = Mathf.LerpAngle(_targetToRotate.eulerAngles.y, _targetAngle, Time.deltaTime * _rotationSpeed);
                _targetToRotate.rotation = Quaternion.Euler(0, newY, 0);


                if (Mathf.Abs(Mathf.DeltaAngle(_targetToRotate.eulerAngles.y, _targetAngle)) < 0.5f)
                {
                    _targetToRotate.rotation = Quaternion.Euler(0, _targetAngle, 0);
                    _isRotating = false;
                }
                
            }
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {

                if (!_isRotating)
                {
                    _startAngle = _targetToRotate.eulerAngles.y;
                    _targetAngle = _startAngle + _rotationAmount;
                    _isRotating = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _isRotating = false;
        }

    }
}
