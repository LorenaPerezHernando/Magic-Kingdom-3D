using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StoriesAnimPlayer : MonoBehaviour
{
    private Animator _aenyaAnim;

    private float _inputHorizontal;
    [SerializeField] private int _speed;

    private void Awake()
    {
        _aenyaAnim = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0f, 530f, 0f);

    }
    private void Start()
    {
        StartCoroutine(CinematicWalking());
    }
    IEnumerator CinematicWalking()
    {

        _aenyaAnim.SetBool("IsMoving", true);
        float elapsed = 0f;
        while (elapsed < 14f)
        {
            transform.rotation = Quaternion.Euler(0f, 530f, 0f);
            transform.position += Vector3.forward * _speed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        _aenyaAnim.SetBool("IsMoving", false);
    }
}
