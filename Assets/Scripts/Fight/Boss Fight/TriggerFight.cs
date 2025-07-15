using System;
using UnityEngine;

namespace Magic.Boss
{


    public class TriggerFight : MonoBehaviour
    {
        #region Fields & Properties
        [SerializeField] private GameObject UIGameController;
        public event Action OnStartFight;
        [SerializeField] private Animator _boss;
        #endregion


        #region Unity Callbacks
        private void OnTriggerEnter(Collider other)
        {
        

            if (other.CompareTag("Player"))
            {
                print("Start fight");
                OnStartFight?.Invoke();
                _boss.SetTrigger("Greetings");
                //TODO Particulas al tocar el suelo 
                //TODO Particulas de cerrar el circulo donde van a luchar 

                Destroy(gameObject);
            }
        }

    #endregion
    }
}
