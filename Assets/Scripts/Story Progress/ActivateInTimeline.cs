using UnityEngine;

public class ActivateInTimeline : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _vfxSmoke;
    [SerializeField] private ParticleSystem[] _vfxSlashes;


    public void PlaySlashes()
    {
        foreach (var slashes in _vfxSlashes)
        {
            slashes.Play();
        }
    }

    public void PlaySmoke()
    {
        foreach(var smoke in _vfxSlashes)
            { smoke.Play(); }
    }
}
