using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadSceneFromButton : MonoBehaviour
{
    [SceneName]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Button button; 

    SceneLoader _sceneLoader;
    
    [Inject] 
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        button.onClick.AddListener(() => _sceneLoader.LoadScene(sceneToLoad));
    }
}
