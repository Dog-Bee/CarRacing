using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public abstract class ARaceTracker : MonoBehaviour,IRaceProgress
{
    protected int _index;
    protected int _lap;
    protected Vector3 _currentCheckpoint;
    
    protected CheckpointBehaviour _checkpointBehaviour;
    protected RacePathService _racePathService;
    protected LeaderboardService _leaderboardService;

    public Vector3 CurrentCheckpoint => _currentCheckpoint;
    public float ReachDistance;
    public bool IsStop { get; protected set; }
    public float TotalProgress => _lap*_racePathService.TotalLength+_racePathService.GetProgressAlongPath(transform.position, _index);
    public string Name { get; protected set; }
    public bool IsPlayer { get; protected set; }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _currentCheckpoint) < ReachDistance)
        {
            OnTrigger();
        }
    }
    protected virtual void OnTrigger()
    {
        _currentCheckpoint=_checkpointBehaviour.GetNextCheckpoint(ref _index,IsPlayer);
        if (_index == 0)
            _lap++;
    }

    


}
