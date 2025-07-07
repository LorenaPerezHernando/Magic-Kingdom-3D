using System.Collections;
using UnityEngine;

public class MagicParticle : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform _playerPos;
    void Start()
    {
        //TODO WIND AUDIO o Electric 
        _playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.LookAt(_playerPos);
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
            print("Collision con el player");
            //TODO HEALTH SYSTEM DAÑO 
        }
    }

}
