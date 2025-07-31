using Magic;
using Magic.Dialogue;
using UnityEngine;

public class DialogueCollision : MonoBehaviour
{
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private bool _hasPlayed = false;

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision");
        if (collision.gameObject.CompareTag("Player") && !_hasPlayed)
        {
            print("Collision");
            //_hasPlayed = true;
            DialogueManager.Instance.StartDialogue(_dialogueData);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogueManager.Instance.CloseDialogue();
        }
    }


    public void CollisionDialogue()
    {
        if (!_hasPlayed)
        {
            print("Play Dialogue");
            //_hasPlayed = true;
            DialogueManager.Instance.StartDialogue(_dialogueData);
        }
    }
}
