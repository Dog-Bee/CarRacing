#if(UNITY_EDITOR)
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad] public class AutoBootstrapPlayHandler
{
    private const string BOOTSTRAP_SCENE_NAME = "BootstrapScene";
    private const string AUTO_PLAY_PREF_KEY = "AutoPlayFromBootstrap_Enabled";
    private const string RESOURCE_CONFIG_PATH = "Editor/BootstrapPlayConfig";
    private const string BOOTSTRAP_FILE_NAME = "BootstrapPlayConfig";
    private const string DEFAULT_SCENE_FOLDER = "Assets/Scenes/";

    static AutoBootstrapPlayHandler()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode && IsEnabled())
        {
            var config = GetOrCreateConfig();
            if (config == null || string.IsNullOrEmpty(config.sceneName))
            {
                Debug.LogWarning($"[AutoBootstrap] Config not set or invalid");
                return;
            }

            if (!IsSceneOpen(config.sceneName))
            {
                Debug.Log($"<color=yellow><b>[AutoBootstrap]<b> Switching to scene '{config.sceneName}' before entering Play Mode....</color>");
                EditorApplication.isPlaying = false;
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene($"{DEFAULT_SCENE_FOLDER}{config.sceneName}.unity");
                EditorApplication.delayCall += ()=> EditorApplication.isPlaying = true;
            }
                
        }
    }

    private static bool IsSceneOpen(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsEnabled()
    {
        return EditorPrefs.GetBool(AUTO_PLAY_PREF_KEY, true);
    }

    private static BootstrapPlayConfig GetOrCreateConfig()
    {
        var config = Resources.Load<BootstrapPlayConfig>(RESOURCE_CONFIG_PATH);
        if (config != null) return config;

        config = ScriptableObject.CreateInstance<BootstrapPlayConfig>();
        config.sceneName = "BootstrapScene";

        string folder = "Assets/Resources/Editor";
        string assetPath = $"{folder}/{BOOTSTRAP_FILE_NAME}";
        if(!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");
        if(!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets/Resources", "Editor");
        
        AssetDatabase.CreateAsset(config, assetPath);
        AssetDatabase.SaveAssets();
        
        Debug.LogWarning($"[AutoBootstrap] BootstrapPlayConfig {assetPath} has been created!");
        return config;
    }
    
   
    
}
#endif