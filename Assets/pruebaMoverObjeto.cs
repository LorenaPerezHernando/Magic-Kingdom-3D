using UnityEngine;

public class pruebaMoverObjeto : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * 5f, ForceMode.Impulse);
        }
    }
}
