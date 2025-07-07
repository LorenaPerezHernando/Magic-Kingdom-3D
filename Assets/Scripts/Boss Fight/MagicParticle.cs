using System.Collections;
using UnityEngine;

public class MagicParticle : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Transform _playerPos;
    void Start()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.LookAt(_playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

    }

}
