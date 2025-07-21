using Magic.VFX;
using System;
using System.Collections;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public event Action<float> OnHeal;
    private Animator _anim;
    [SerializeField] private GameObject _healVFX;
    [SerializeField] private GameObject _tornadoVFXPrefab;
    [SerializeField] private Transform _tornadoSpawnPoint;

    public bool isBlocked = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isBlocked) return;
        if (Input.GetMouseButtonUp(0))
        {
            if (isBlocked) return;
            _anim.SetTrigger("Attack");
            SpawnAttack();
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (isBlocked) return;
            //TODO ATTACK 2
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (isBlocked) return;
            HealVFX();
        }
       
    }
    public void SetBlocked(bool value)
    {
        isBlocked = value;

    }


    public void Fight()
    {
        _anim.SetBool("Combat", true); //Movimiento de combate 
    }

    private void SpawnAttack()
    {
        
        if (_tornadoVFXPrefab != null && _tornadoSpawnPoint != null)
        {
            GameObject tornado = Instantiate(_tornadoVFXPrefab, _tornadoSpawnPoint.position, Quaternion.Euler(-90f, 0,0));
            tornado.GetComponent<TornadoParticle>().SetDirection(transform.forward);
        }
    }

    private void HealVFX()
    {
        _anim.SetTrigger("Heal");
        _healVFX.SetActive(false);
        _healVFX.SetActive(true);
        OnHeal?.Invoke(20f);

    }
}
