using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class SyncPositionsMenu : MonoBehaviour
{
    [SerializeField] private List<Transform> source;
    [SerializeField] private List<Transform> target;
    
    #if UNITY_EDITOR
    [ContextMenu("Sync Positions")] public void SyncPositions()
    {
        Undo.RegisterFullObjectHierarchyUndo(gameObject, "Sync Positions");
        int sourceCount = source.Count;
        int targetCount = target.Count;

        if (targetCount > sourceCount)
        {
            for (int i = targetCount - 1; i >= sourceCount; i--)
            {
                if (target[i] != null)
                {
                    Undo.DestroyObjectImmediate(target[i].gameObject);
                }
                target.RemoveAt(i);
            } 
        }

        for (int i = 0; i < sourceCount; i++)
        {
            target[i].position = source[i].position;
            target[i].gameObject.SetActive(true);
            EditorUtility.SetDirty(target[i]);
        }
        
        EditorUtility.SetDirty(this);
            
    }
    #endif
}
