using UnityEngine;

namespace Magic.Boss
{


public class TriggerFight : MonoBehaviour
{
    [SerializeField] private Animator _boss;


    #region Unity Callbacks
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            print("Start fight");
            _boss.SetTrigger("Greetings");
                //TODO Particulas al tocar el suelo 
                //TODO Particulas de cerrar el circulo donde van a luchar 

            Destroy(gameObject);
        }
    }

    #endregion
}
}
