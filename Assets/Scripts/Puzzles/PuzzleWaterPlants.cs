using UnityEngine;

public class PuzzleWaterPlants : MonoBehaviour
{
    [Header("Finished Puzzle")]
    [SerializeField] private Transform _farmer;
    [SerializeField] private Transform _player;
    [SerializeField] private DialogueTrigger _farmerDialogue; 
    [SerializeField] private Animator _farmerAnim;
    [SerializeField] private GameObject _rainVFX;
    [SerializeField] private GameObject _smokeVFX;

    [Header("Puzzle")]
    [SerializeField] private int _plantsWatered;

    private void Awake()
    {
        _farmerDialogue = _farmer.GetComponent<DialogueTrigger>();
        _farmerAnim = _farmer.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PotsToWater"))
        {
            _plantsWatered++;
            //TODO sounds

            if(_plantsWatered >= 3)
            {
                print("Rewards");
                _farmerAnim.SetBool("Idle", true);
                _rainVFX.SetActive(false);
                _smokeVFX.SetActive(false);               
                _farmer.LookAt(_player.transform);
                _farmerDialogue.TriggerDialogue();

            }
        }
    }
}
