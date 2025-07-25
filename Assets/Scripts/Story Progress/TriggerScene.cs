using Magic;
using System;
using UnityEngine;

public class TriggerScene : MonoBehaviour
{
    [SerializeField] private int _sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.SavePosition(other.transform.position);

            GameController.Instance.LoadScene(_sceneToLoad);
            print("Pasando a la siguiente escena");
        }

    }
}
