using System;
using UnityEngine;

namespace Magic.Data
{


    [System.Serializable]
    public class SaveData
    {
        public int scene;
        public PlayerSaveData player;
        public GameProgressData progress;
        public InventoryData inventory;


        [Serializable]
        public class PlayerSaveData
        {
            public Vector3 position;
        }

        [Serializable]
        public class GameProgressData
        {
            public int spirits;
            public int villages;
            public int puzzles;
            public int bosses;
        }

        [Serializable]
        public class InventoryData
        {
            public int healingPlants;
        }
    }
}
