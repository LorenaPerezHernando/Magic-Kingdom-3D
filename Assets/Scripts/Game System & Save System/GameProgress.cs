using UnityEngine;

namespace Magic.Data
{


    public class GameProgress : MonoBehaviour
    {
        public int spirits;
        public int villages;

        public int puzzlesCompleted;
        public int bossesDefeated;

        public int healingPlants;

        public void ResetProgress()
        {
            spirits = 0;
            villages = 0;
            puzzlesCompleted = 0;
            bossesDefeated = 0;
            healingPlants = 0;
        }
    }
}
