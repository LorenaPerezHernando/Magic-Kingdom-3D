using UnityEngine;

public class CursorController : MonoBehaviour
{
    private bool _isCursorVisible = false;

    private void Start()
    {
        ApplyCursorState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isCursorVisible = !_isCursorVisible;
            ApplyCursorState();
        }
    }

    private void ApplyCursorState()
    {
        Cursor.visible = _isCursorVisible;
        Cursor.lockState = _isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        Debug.Log("Cursor " + (_isCursorVisible ? "visible" : "oculto"));
    }
}
