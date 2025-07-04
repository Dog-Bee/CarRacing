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
        
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_currentScene,LoadSceneMode.Additive);
        
        loadOperation.allowSceneActivation = false;
        
        while (loadOperation.progress < 0.9f)
        {
            yield return null;
        }
        loadOperation.allowSceneActivation = true;

        while (!loadOperation.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentScene));

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
