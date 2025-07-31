using System.Collections.Generic;
using UnityEngine;

public class CameraAvoidsStage : MonoBehaviour
{
    private List<Renderer> renderersToHide = new List<Renderer>();
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            renderersToHide = new List<Renderer>(collision.gameObject.GetComponentsInChildren<Renderer>());
            foreach (Renderer r in renderersToHide)
                r.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stage"))
        {
            foreach (Renderer r in renderersToHide)
                if (r != null) r.enabled = true;

            renderersToHide.Clear();
        }
    }
}
