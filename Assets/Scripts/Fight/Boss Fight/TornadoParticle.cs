using UnityEngine;

namespace Magic.VFX
{


    public class TornadoParticle : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _lifetime = 3f;

        private Vector3 _direction;

        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _pushForce = 10f;


        private void Start()
        {
            Destroy(gameObject, _lifetime);
        }

        private void Update()
        {
            transform.position += _direction * _speed * Time.deltaTime;

        }

        public void SetDirection(Vector3 dir)
        {
            _direction = dir.normalized;
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody otherRb = other.attachedRigidbody;
            if (otherRb != null)
            {
                otherRb.AddForce(_direction * _pushForce, ForceMode.Impulse);
            }


            if (other.CompareTag("Boss"))
            {
                HealthSystem bossHealth = other.GetComponent<HealthSystem>();

                bossHealth.TakeDamage(20f);
                print("Boss ha recibido 20 de daño del Tornado.");
            }
        }
    }

    
}
