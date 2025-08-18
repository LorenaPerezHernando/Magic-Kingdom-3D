using Magic.Dialogue;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private bool _hasPlayed = false;

    //[Header("Optional Reward Toast")]
    //[SerializeField] private bool hasRewardText = false;
    //[TextArea]
    //[SerializeField] private string rewardText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasPlayed)
        {
            _hasPlayed = true;
            DialogueManager.Instance.StartDialogue(_dialogueData);
           
        }
    }

    public void TriggerDialogue()
    {
        if (!_hasPlayed)
        {
            print("Play Dialogue");
            _hasPlayed = true;
            DialogueManager.Instance.StartDialogue(_dialogueData);
        }
    }


}
