using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class CheckpointGroup
{
    public List<Checkpoint> Checkpoints;
}
public class CheckpointBehaviour : MonoBehaviour
{
    [SerializeField] private int lapCount;
    [SerializeField] private List<Transform> checkpointGroups;
    [SerializeField] private List<Transform> finishPositions;
    public int LapCount => lapCount;
    public Transform FirstCheckpoint => checkpointGroups[0];

    private void Start()
    {
        StartInit();
    }

    public void CheckpointActivate(int index)
    {
        checkpointGroups[index].gameObject.SetActive(true);
    }

    private void StartInit()
    {
        checkpointGroups.ForEach(c=>c.gameObject.SetActive(false));
        checkpointGroups[0].gameObject.SetActive(true);
    }

    public Transform GetNextCheckpoint(ref int index)
    {
        index = index >= checkpointGroups.Count - 1? 0: index + 1;
        
            return checkpointGroups[index].transform;
    }

    public Transform GetFinishCheckpoint()
    {
        return finishPositions[Random.Range(0, finishPositions.Count)];
    }
    
    
}
