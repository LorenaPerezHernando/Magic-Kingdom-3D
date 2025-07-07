using System.Collections;
using UnityEngine;

public class Boss1Fight : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private GameObject _sphereInHand;
    [SerializeField] private Transform _instantiatePosMagicAttack;
    [SerializeField] private GameObject _prefabMagicSphere;
    [SerializeField] private Vector3 _targetScale = new Vector3(1f, 1f, 1f);
    private Vector3 _initialScale = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private GameObject _shortDistanceAttackParticle;
    [Header("Fighting Variables")]
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
        _sphereInHand.SetActive(false);
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        _distance = Vector3.Distance(transform.position, _player.transform.position);
        if(_distance < 9)
        {
            _startFight=true;
            //TODO Music Intensifies 
        }

        if (_startFight)
        {
            transform.LookAt(_player.transform.position);

            if (_distance > 10)
            {
                _anim.SetBool("Walk", true);
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            }
            else
            {
                _anim.SetBool("Walk", false);
            }

            if (!_isAttacking)
            {               
                StartCoroutine(AttackCorrutine());
            }
            else
            {
                _anim.SetTrigger("Idle2");
            }
            //TODO else con otra animación idle (pero furiosa)
        }

    }

    IEnumerator AttackCorrutine()
    {
        _isAttacking = true;

        yield return new WaitForSeconds(_timeToAttack);
        if (_distance > 2.5 && _distance < 10)
        {
            //TODO AUDIO VFX 
            _anim.SetTrigger("Magic Attack");
            _sphereInHand.SetActive(true);
            StartCoroutine(RelayShoot(2));
            StartCoroutine(RelayShoot(2.1f));
            StartCoroutine(RelayShoot(2.2f));

        }
        else
            _shortDistanceAttackParticle.SetActive(false);

            _isAttacking = false; 

    }

    IEnumerator RelayShoot(float timeToWait)
    {
        
        yield return new WaitForSeconds(timeToWait);
        _sphereInHand.SetActive(false);
        Instantiate(_prefabMagicSphere, _instantiatePosMagicAttack.transform.position, Quaternion.identity);

    }


}
