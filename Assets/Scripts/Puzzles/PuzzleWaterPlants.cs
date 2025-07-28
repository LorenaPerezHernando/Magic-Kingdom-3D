using UnityEngine;

public class PuzzleWaterPlants : MonoBehaviour
{
    [SerializeField] private int _plantsWatered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PotsToWater"))
        {
            _plantsWatered++;

            if(_plantsWatered >= 3)
            {
                print("Rewards");
                //TODO Rewards : Women talks (me has ayudado a regar, muchas gracias, no se que me ha pasado,
                //Puedes quedarte lo que quieras de mi jardin
            }
        }
    }
}
