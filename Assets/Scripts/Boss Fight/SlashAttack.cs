using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SlashAttack : MonoBehaviour
{
    [SerializeField] private float pushForce = 5f;
    private bool _isPushing = true; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (_isPushing)
            {
                //TODO WIND AUDIO & Slap
                //TODO VFX Particulas player tirando arena/nubes de polvo por el golpe
                StartCoroutine(Push(controller, other.transform));
            }

          
        }
    }

    IEnumerator Push(CharacterController controller, Transform playerTransform)
    {
        _isPushing = false; 
        yield return new WaitForSeconds(0.5f);
        Vector3 pushDirection = -playerTransform.transform.forward;

        controller.Move(pushDirection * pushForce);
        print("Player Pushed");
        _isPushing = true;


    }
}
