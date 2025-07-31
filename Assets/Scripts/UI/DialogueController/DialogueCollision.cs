using Magic.Dialogue;
using UnityEngine;

public class DialogueCollision : MonoBehaviour
{
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private bool _hasPlayed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_hasPlayed)
        {
            _hasPlayed = true;
            DialogueManager.Instance.StartDialogue(_dialogueData);
        }
    }


    public void CollisionDialogue()
    {
        if (!_hasPlayed)
        {
            print("Play Dialogue");
            _hasPlayed = true;
            DialogueManager.Instance.StartDialogue(_dialogueData);
        }
    }
}
