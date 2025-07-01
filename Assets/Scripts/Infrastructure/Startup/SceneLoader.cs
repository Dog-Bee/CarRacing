using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string _prevScene="";
    private string _currentScene="";
    public void LoadScene(string sceneName)
    {
        if (_currentScene == sceneName) return;
            
        StartCoroutine(LoadSceneRoutine(sceneName));
    }
   

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        _prevScene = _currentScene;
        _currentScene = sceneName;
        
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        
        loadOperation.allowSceneActivation = false;
        
        while (loadOperation.progress < 0.9f)
        {
            yield return null;
        }
        loadOperation.allowSceneActivation = true;

        if (_prevScene != "")
        {
            UnloadScene(_prevScene);
        }
    }

    private void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName); 
    }
    
}
