using UnityEngine;
using Zenject;

public class PlayerCheckpointController : ACheckpointController
{
    private SignalBus _signalBus;
    [Inject] private void Construct(CheckpointBehaviour checkpointBehaviour)
    {
        _checkpointBehaviour = checkpointBehaviour;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTrigger();
    }
    
    public override void OnTrigger()
    {
        base.OnTrigger();
        if(_lap!= _checkpointBehaviour.LapCount)
        _checkpointBehaviour.CheckpointActivate(_index);
        if(_index == 0)
            _signalBus.Fire(new LapFinishedSignal(_lap));
    }
}
