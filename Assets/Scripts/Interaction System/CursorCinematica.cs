using UnityEngine;

public class CursorCinematica : MonoBehaviour
{
    public void ForceUnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
