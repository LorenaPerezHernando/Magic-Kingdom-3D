using Magic.Interact;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    #region Events
    public event Action<string> OnShowInteraction;
    public event Action OnHideInteraction;
    #endregion

    #region Fields
    [Header("Variables")]
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private float _distance = 5f;
    [SerializeField] private LayerMask _interactionLayer;

    private IInteractable _currentInteractable;
    private bool _interactableDetected = false;
    #endregion

    #region Unity Callbacks
    private void Update()
    {
        if (_interactableDetected && Input.GetKeyDown(KeyCode.E))
        {
            _currentInteractable?.Interact();
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.SphereCast(origin, _radius, direction, out hit, _distance, _interactionLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (_currentInteractable != interactable)
                {
                    InteractableInfo info = interactable.GetInfo();
                    OnShowInteraction?.Invoke("E - " + info.Action + " " + info.Type);
                    _currentInteractable = interactable;
                }

                _interactableDetected = true;
                return;
            }
        }

        if (_interactableDetected)
        {
            _interactableDetected = false;
            _currentInteractable = null;
            OnHideInteraction?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {       

        Vector3 start = transform.position;
        Vector3 dir = transform.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(start, _radius);
        Gizmos.DrawWireSphere(start + dir * _distance, _radius);
        Gizmos.DrawLine(start, start + dir * _distance);
    }
    #endregion
}

