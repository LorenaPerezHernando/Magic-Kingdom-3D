using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _anim.SetTrigger("Attack");
        }

        if (Input.GetMouseButtonUp(1))
        {
            //TODO ATTACK 2
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _anim.SetTrigger("Heal");
        }
    }

    public void Fight()
    {
        _anim.SetBool("Combat", true); 
    }
}
