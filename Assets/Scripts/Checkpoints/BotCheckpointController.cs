
using Zenject;

public class BotCheckpointController : ACheckpointController
{
    [Inject] private void Construct(CheckpointBehaviour checkpointBehaviour)
    {
        _checkpointBehaviour = checkpointBehaviour;
        _currentCheckpoint = _checkpointBehaviour.FirstCheckpoint;
    }
    
    public override void OnTrigger()
    {
        base.OnTrigger();
        if (_lap == _checkpointBehaviour.LapCount)
        {
            _currentCheckpoint = _checkpointBehaviour.GetFinishCheckpoint();
        }
    }
}
