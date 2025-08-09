using UnityEngine;

public class DesactivateObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Stage"))
            collision.gameObject.SetActive(false);
    }
}
