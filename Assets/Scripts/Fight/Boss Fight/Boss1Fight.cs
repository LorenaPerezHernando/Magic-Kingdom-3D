using Magic;
using System.Collections;
using UnityEngine;

public class Boss1Fight : MonoBehaviour
{
    #region Fields & Properties
    [Header("Particles")]
    [SerializeField] private GameObject _sphereInHand;
    [SerializeField] private Transform _instantiatePosMagicAttack;
    [SerializeField] private GameObject _prefabMagicSphere;
    [SerializeField] private GameObject _shortDistanceAttackParticle;
    [Header("Fighting Variables")]
    private bool _isBlocked = false;
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToAttack = 10; 
    [SerializeField] private bool _startFight = false;
    [SerializeField] private bool _isAttacking = false;
    private bool _isPushing = false;
    private GameObject _player;
    private Animator _anim;
    private Coroutine _attackRoutine;
    #endregion
    #region Unity Callbacks
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        _sphereInHand.SetActive(false);
        
    }

    void Update()
    {
        if (_isBlocked) return;
        _distance = Vector3.Distance(transform.position, _player.transform.position);
        if(_distance < 9 && !_isBlocked)
        {
            _startFight=true;
            //TODO Music Intensifies 
        }

        if (_startFight && !_isBlocked)
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
                _attackRoutine = StartCoroutine(AttackCorrutine()); 
            }
            else
            {
                _anim.SetTrigger("Idle2");
            }

            if (_distance <= 2.5 && !_isPushing)
            {
                _anim.SetTrigger("Attack");
                StartCoroutine(ShortAttack());
                _isPushing = true;

            }
            //TODO else con otra animación idle (pero furiosa)
        }

    }
    #endregion

    #region Public Methods
    public void SetBlocked(bool value)
    {
        _isBlocked = value;
        _startFight = false;
        if (_isBlocked && _attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }
        _isAttacking = false;
        _anim.SetBool("Walk", false);
    }
    #endregion
    #region Private Methods

    private IEnumerator AttackCorrutine()
    {
        _isAttacking = true;


        yield return new WaitForSeconds(_timeToAttack);
        if (_isBlocked)
        {
            _isAttacking = false;
            yield break;
        }
        if (_distance > 2.5 && _distance < 10)
        {
            //TODO AUDIO VFX 
            _anim.SetTrigger("Magic Attack");
            _sphereInHand.SetActive(true);
            StartCoroutine(RelayShoot(2));
            StartCoroutine(RelayShoot(2.1f));
            StartCoroutine(RelayShoot(2.2f));

        }
        
            _isAttacking = false; 

    }
    private IEnumerator ShortAttack()
    {
        yield return new WaitForSeconds(1);
        _shortDistanceAttackParticle.SetActive(true);
        _isPushing = false;

    }

    private IEnumerator RelayShoot(float timeToWait)
    {
        
        yield return new WaitForSeconds(timeToWait);
        _sphereInHand.SetActive(false);
        Instantiate(_prefabMagicSphere, _instantiatePosMagicAttack.transform.position, Quaternion.identity);

    }
    #endregion



}
