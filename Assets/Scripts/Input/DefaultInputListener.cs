using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class DefaultInputListener : MonoBehaviour
{
    [SceneName]
    [SerializeField] private string menuName;
    
    private DefaultInput _input;
    private SceneLoader _sceneLoader;
    
    [Inject] private void Construct(DefaultInput input,SceneLoader loader)
    {
        _input = input;
        _sceneLoader = loader;
        _input.Esc.performed += LoadMenu;
    }

    private void OnDisable()
    {
        _input.Esc.performed -= LoadMenu;
    }

    private void LoadMenu(InputAction.CallbackContext context)
    {
        _sceneLoader.LoadScene(menuName);
    }
    
}
