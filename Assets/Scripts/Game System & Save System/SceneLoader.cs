using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int _sceneToLoad;
    public void LoadSceneByIndex(int index)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (index >= 0 && index < sceneCount)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogWarning("indice no esta en Build");
        }
    }
}
