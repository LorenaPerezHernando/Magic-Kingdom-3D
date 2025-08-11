using UnityEngine;


public class OwlFyingToGuardian : MonoBehaviour
{
    [Header("Detección")]
    [SerializeField] private float _detectRadius = 10f;
    [SerializeField] private string _playerTag = "Player";
    [SerializeField] private GameObject _owlWings;


    [Header("Destinos")]
    [SerializeField] private Transform[] _destinations;
    [SerializeField] private float _moveSpeed = 5f;

    private int _currentIndex = 0;
    private bool _playerInside = false;

    void Update()
    {
        DetectPlayer();

        if (_playerInside && _destinations.Length > 0)
        {
            Transform target = _destinations[_currentIndex];
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                _moveSpeed * Time.deltaTime
            );


            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                _currentIndex++;

                if (_currentIndex >= _destinations.Length)
                {
                    Destroy(_owlWings);
                    Destroy(this); 
                }
            }
        }
    }

    void DetectPlayer()
    {
        _playerInside = false;
        Collider[] hits = Physics.OverlapSphere(transform.position, _detectRadius, ~0, QueryTriggerInteraction.Collide);

        foreach (var h in hits)
        {
            if (h.CompareTag(_playerTag))
            {
                _playerInside = true;
                
                break;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, _detectRadius);
    }
}





