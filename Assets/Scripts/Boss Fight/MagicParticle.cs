using Magic;
using System.Collections;
using UnityEngine;

public class MagicParticle : MonoBehaviour
{
    #region Fields & Properties
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _speed = 1f;
    private Transform _playerPos;
    #endregion

    #region UnityCallbacks
    void Start()
    {
        //TODO WIND AUDIO o Electric 
        _playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.LookAt(_playerPos);

        StartCoroutine(DestroyObject());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem _playerHealth = other.GetComponent<HealthSystem>();

            if (_playerHealth != null)
            {
                _playerHealth.TakeDamage(5f);

                print("Trigger with Boss Bubbles");
                //TODO Particulas de sangre cuando impacta
            }
            else
                print("Health Sys Null");
        }
    }
    #endregion

    #region Private Methods
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    #endregion


}
