using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable] public class CheckpointBehaviour : MonoBehaviour
{
    [SerializeField] private int lapCount;
    [SerializeField] private List<Checkpoint> playerCheckpoints;
    [SerializeField] private List<Checkpoint> botCheckpoints;
    [SerializeField] private List<Transform> finishPositions;
    
    private List<Vector3> _botCheckpointPositions = new();
    private List<Vector3> _playerCheckpointPositions = new();
    
    public int LapCount => lapCount;
    public Vector3 FirstBotCheckpoint => botCheckpoints[0].GetTarget();
    public Vector3 FirstPlayerCheckpoint => playerCheckpoints[0].GetMiddlePosition();
    public List<Vector3> BotCheckpoints => GetBotCheckpoints();
    public List<Vector3> PlayerCheckpoints => GetPlayerCheckpoints();

    private void Start()
    {
        StartInit();
    }

    public void CheckpointActivate(int index)
    {
        playerCheckpoints[index].Activate();
    }

    private void StartInit()
    {
        playerCheckpoints.ForEach(c => c.Deactivate());
        botCheckpoints.ForEach(c => c.Deactivate());
        playerCheckpoints[0].Activate();
    }

    public Vector3 GetNextCheckpoint(ref int index, bool isPlayer)
    {
        int prevIndex = index;
        
        if (isPlayer)
        {
            index = index >= playerCheckpoints.Count - 1 ? 0 : index + 1;
            playerCheckpoints[prevIndex].Deactivate();
            playerCheckpoints[index].Activate();
            return playerCheckpoints[index].GetMiddlePosition();
        }
        index = index >= botCheckpoints.Count - 1 ? 0 : index + 1;
        botCheckpoints[prevIndex].Deactivate();
        botCheckpoints[index].Activate();
        return botCheckpoints[index].GetTarget();
    }

    public List<Vector3> GetBotCheckpoints()
    {
        if (_botCheckpointPositions.Count == 0)
        {
            botCheckpoints.ForEach(c =>_botCheckpointPositions.Add(c.GetMiddlePosition()));
        }
        return _botCheckpointPositions;
    }
    public List<Vector3> GetPlayerCheckpoints()
    {
        if (_playerCheckpointPositions.Count == 0)
        {
            playerCheckpoints.ForEach(c =>_playerCheckpointPositions.Add(c.GetMiddlePosition()));
        }
        return _playerCheckpointPositions;
    }

    public Vector3 GetFinishCheckpoint()
    {
        return finishPositions[Random.Range(0, finishPositions.Count)].position;
    }
}