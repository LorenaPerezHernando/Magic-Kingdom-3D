using System.Collections;
using UnityEngine;

public class Boss1Fight : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToAttack = 10; 
    [SerializeField] private bool _startFight = false;
    [SerializeField] private bool _isAttacking = false;
    private GameObject _player;
    private Animator _anim;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        _distance = Vector3.Distance(transform.position, _player.transform.position);
        if(_distance < 9)
            _startFight=true;

        if (_startFight)
        {
            transform.LookAt(_player.transform.position);

            if (_distance > 10)
            {
                _anim.SetBool("Walk", true);
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            }
            else
                _anim.SetBool("Walk", false);

            if(!_isAttacking) 
            StartCoroutine(AttackCorrutine());
        }

    }

    IEnumerator AttackCorrutine()
    {
        _isAttacking = true;

        yield return new WaitForSeconds(_timeToAttack);
        if (_distance >= 2.5 && _distance < 10)
            _anim.SetTrigger("Magic Attack");
        else if (_distance < 2.5)
            _anim.SetTrigger("Attack");

        _isAttacking = false; 

    }
}
