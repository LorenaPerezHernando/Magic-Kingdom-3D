using UnityEngine;
using UnityEngine.Rendering;

namespace Magic.ClockPuzzle
{


    public class PuzzleClock : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 2f;
        private float _target1 = 7;
        private float _target2 = 370;
        //Bool
        private bool _isInside = false; 
        private bool _isRotating = false;
        //Variables
        private float _startAngle;
        private float _nextTargetAngle;
        private float _rotationAmount = 30;

        private void Update()
        {
            if (_isRotating)
            {
                float currentY = Mathf.LerpAngle(transform.eulerAngles.y, _nextTargetAngle, Time.deltaTime * _rotationSpeed);
                transform.rotation = Quaternion.Euler(0, currentY, 0);

                if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, _nextTargetAngle)) < 0.5f)
                {
                    transform.rotation = Quaternion.Euler(0, _nextTargetAngle, 0);
                    _isRotating = false;
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isInside = true;
                //Action in input =  Push();
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isInside = false;
            }
        }
        public void TryPushing()
        {
            if( _isInside && !_isRotating)
            {
                Push();
            }
        }

        private void Push() 
        {
            _startAngle = transform.eulerAngles.y;
            _nextTargetAngle = _startAngle + _rotationAmount;
            _isRotating = true;

        }
    }
}
