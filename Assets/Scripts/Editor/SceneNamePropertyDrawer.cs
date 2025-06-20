#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;


    [CustomPropertyDrawer(typeof(SceneNameAttribute))]
    public class SceneNamePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.HelpBox(position, "[SceneName] Attribute can be used only for string fields",MessageType.Error);
                return;
            }
            
            var scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled)
                .Select(scene => System.IO.Path.GetFileNameWithoutExtension(scene.path)).ToArray();
            if (scenes.Length == 0)
            {
                EditorGUI.HelpBox(position, "No scenes found", MessageType.Warning);
                return;
            }
            int currentIndex = Mathf.Max(0, System.Array.IndexOf(scenes,property.stringValue));
            int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, scenes);

            if (selectedIndex >= 0 && selectedIndex < scenes.Length)
            {
                property.stringValue = scenes[selectedIndex];
            }
        }
        
    }
#endif