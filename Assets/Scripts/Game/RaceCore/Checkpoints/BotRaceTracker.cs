
using Zenject;

public class BotRaceTracker : ARaceTracker
{
    private bool _isFinish;
    [Inject] private void Construct(CheckpointBehaviour checkpointBehaviour,LeaderboardService leaderboardService)
    {
        _checkpointBehaviour = checkpointBehaviour;
        _currentCheckpoint = _checkpointBehaviour.FirstBotCheckpoint;
        _racePathService = new RacePathService(_checkpointBehaviour.BotCheckpoints);
        _leaderboardService = leaderboardService;
        _leaderboardService.Register(this);
        Name = "Bot";
        IsPlayer = false;
    }
    
    protected override void OnTrigger()
    {
        base.OnTrigger();
        if (_isFinish)
        {
            IsStop = true;
        }
        if (_lap >= _checkpointBehaviour.LapCount)
        {
            _currentCheckpoint = _checkpointBehaviour.GetFinishCheckpoint();
            ReachDistance = 5;
            _isFinish = true;
        }
        
    }

   
}
