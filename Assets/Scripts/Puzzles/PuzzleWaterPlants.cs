using UnityEngine;

public class PuzzleWaterPlants : MonoBehaviour
{
    [Header("Finished Puzzle")]
    [SerializeField] private Animator _farmerAnim;
    [SerializeField] private GameObject _rainVFX;

    [Header("Puzzle")]
    [SerializeField] private int _plantsWatered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PotsToWater"))
        {
            _plantsWatered++;

            if(_plantsWatered >= 3)
            {
                print("Rewards");
                //TODO Rewards : 
                _farmerAnim.SetBool("Idle", true);
                _rainVFX.SetActive(false);
                //TODO farmer looks at player
                //TODO farmer talks : Women talks (me has ayudado a regar, muchas gracias, no se que me ha pasado,
                //Puedes quedarte lo que quieras de mi jardin

            }
        }
    }
}
