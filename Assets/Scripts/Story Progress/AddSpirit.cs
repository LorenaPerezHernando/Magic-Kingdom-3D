using UnityEngine;
using Magic;
using Magic.Dialogue;

public class AddSpirit : MonoBehaviour
{
    [SerializeField] private SpiritInfo spiritInfo;
    private void OnTriggerEnter(Collider other)
    {
        GameController.Instance?.AddSpirit(spiritInfo);
        Destroy(gameObject);
    }
}
