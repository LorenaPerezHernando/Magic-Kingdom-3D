using System;
using UnityEngine;

namespace Magic.Boss
{


public class TriggerFight : MonoBehaviour
{
    public event Action OnStartFight;
    [SerializeField] private Animator _boss;


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
