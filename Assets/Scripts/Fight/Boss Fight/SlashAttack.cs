using Magic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SlashAttack : MonoBehaviour
{
    #region Fields & Properties
    [SerializeField] private float pushForce = 5f;
    private float _lastPushTime = -1f;
    [SerializeField] private float _pushCooldown = 1f;
    private bool _isPushing = true;
    private ParticleSystem _particleSlash;
    private ParticleSystem _particlesChild;
    #endregion
    #region Unity Callbacks

    private void Start()
    {
        _particleSlash = GetComponent<ParticleSystem>();
        _particlesChild = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController _controller = other.GetComponent<CharacterController>();
            Animator _animator = other.GetComponent<Animator>();
                print("Collision con el player");


            HealthSystem _playerHealth = other.GetComponent<HealthSystem>();

            if (_playerHealth != null && _animator != null)
            {
                _playerHealth.TakeDamage(5f);
                _animator.SetTrigger("Hit");
                print("Trigger with Slash");
                //TODO Particulas de sangre cuando impacta
            }
            else
                print("Health Sys Null");

            if (_isPushing)
            {
                //TODO WIND AUDIO & Slap
                //TODO VFX Particulas player tirando arena/nubes de polvo por el golpe
                StartCoroutine(Push(_controller, other.transform));
            }

          
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Collision con el player");         

            if (_isPushing)
            {
                CharacterController _controller = other.GetComponent<CharacterController>();
               // Animator _animator = other.GetComponent<Animator>();
                //TODO WIND AUDIO & Slap
                //TODO VFX Particulas player tirando arena/nubes de polvo por el golpe
                StartCoroutine(Push(_controller, other.transform));
                _lastPushTime = Time.time;
            }


        }
    }
    #endregion
    #region Private Methods

    private IEnumerator Push(CharacterController controller, Transform playerTransform)
    {
        _isPushing = false;
        yield return new WaitForSeconds(0.5f);
        _particleSlash.Play();
        _particlesChild.Play();

        Vector3 pushDirection = -playerTransform.transform.forward;
        controller.Move(pushDirection * pushForce);
        print("Player Pushed");
        yield return new WaitForSeconds(0.5f);

        _particleSlash.Stop();
        _particlesChild.Stop();
        _isPushing = true;


    }
    #endregion
}
