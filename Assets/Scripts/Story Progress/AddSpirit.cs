using UnityEngine;
using Magic;
using Magic.Dialogue;

public class AddSpirit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DialogueManager.Instance.ShowRewardMessage("You have received the Sage Spirit");
        GameController.Instance.AddSpirit();
        Destroy(this);
    }
}
