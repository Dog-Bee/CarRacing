using System.Linq;
using UnityEditor;
using UnityEngine;

public class BootstrapSettingsWindow : EditorWindow
{
    private BootstrapPlayConfig _config;
    private string[] _scenes;
    private int _selectedIndex;
    private const string AUTO_PLAY_PREF_KEY = "AutoPlayFromBootstrap_Enabled";
    private const string RESOURCE_CONFIG_PATH = "Editor/BootstrapPlayConfig";
    private const string BOOTSTRAP_FILE_NAME = "BootstrapPlayConfig";
    private const string DEFAULT_SCENE_FOLDER = "Assets/Scenes/";

    [MenuItem("Tools/Bootstrap Play Config")]
    public static void OpenWindow()
    {
        var window = GetWindow<BootstrapSettingsWindow>("Bootstrap settings window");
        window.minSize = new Vector2(320, 120);
        window.Show();
    }

    private void OnEnable()
    {
        _config = GetOrCreateConfig();
        _scenes = EditorBuildSettings.scenes.Where(s=>s.enabled).Select(s=> System.IO.Path.GetFileNameWithoutExtension(s.path)).ToArray();
        
        _selectedIndex = Mathf.Max(0, System.Array.IndexOf(_scenes,_config.sceneName));
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("Auto Bootstrap Settings",EditorStyles.boldLabel);
        
        bool enabled = EditorPrefs.GetBool(AUTO_PLAY_PREF_KEY, true);
        bool newEnabled = EditorGUILayout.Toggle("Auto Bootstrap Enabled", enabled);

        if (newEnabled != enabled)
        {
            EditorPrefs.SetBool(AUTO_PLAY_PREF_KEY, newEnabled);
        }
        
        EditorGUILayout.Space();
        if (_scenes.Length == 0)
        {
            EditorGUILayout.HelpBox("No scenes found.", MessageType.Warning);
        }
        _selectedIndex = EditorGUILayout.Popup("BootstrapSettings", _selectedIndex, _scenes);
        
        string selectedScene = _scenes.Length>_selectedIndex? _scenes[_selectedIndex] : null;
        
        if (_config.sceneName != selectedScene)
        {
            _config.sceneName = selectedScene;
            EditorUtility.SetDirty(_config);
            AssetDatabase.SaveAssets();
            Debug.Log($"[AutoBootstrap] Scene set to: {selectedScene}");
        }

        
    }
    
    
    
    private static BootstrapPlayConfig GetOrCreateConfig()
    {
        var config = Resources.Load<BootstrapPlayConfig>(RESOURCE_CONFIG_PATH);
        if (config != null) return config;

        config = ScriptableObject.CreateInstance<BootstrapPlayConfig>();
        config.sceneName = "BootstrapScene";

        string folder = "Assets/Resources/Editor";
        string assetPath = $"{folder}/{BOOTSTRAP_FILE_NAME}.asset";
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