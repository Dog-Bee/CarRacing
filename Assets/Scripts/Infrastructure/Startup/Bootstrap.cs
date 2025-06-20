using UnityEngine;


public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SceneName]
    [SerializeField] private string sceneToLoad;
    private void Start()
    {
        sceneLoader.LoadScene(sceneToLoad);
    }
}
