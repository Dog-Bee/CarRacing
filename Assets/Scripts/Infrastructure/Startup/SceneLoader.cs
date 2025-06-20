using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        
        loadOperation.allowSceneActivation = false;
        
        while (loadOperation.progress < 0.9f)
        {
            yield return null;
        }
        loadOperation.allowSceneActivation = true;
    }
}
