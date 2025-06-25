using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACheckpointController : MonoBehaviour
{
    protected int _index;
    protected int _lap;
    protected Transform _currentCheckpoint;
    
    protected CheckpointBehaviour _checkpointBehaviour;

    public Transform CurrentCheckpoint => _currentCheckpoint;
    
    public virtual void OnTrigger()
    {
      _currentCheckpoint=_checkpointBehaviour.GetNextCheckpoint(ref _index);
        if (_index == 0)
            _lap++;
    }

}
