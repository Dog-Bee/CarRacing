using System;
using UnityEngine;
using Zenject;

public class PlayerRaceTracker : ARaceTracker
{
    private SignalBus _signalBus;
    [Inject] private void Construct(CheckpointBehaviour checkpointBehaviour, SignalBus signalBus, LeaderboardService leaderboardService)
    {
        _checkpointBehaviour = checkpointBehaviour;
        _currentCheckpoint = _checkpointBehaviour.FirstPlayerCheckpoint;
        _racePathService = new RacePathService(_checkpointBehaviour.PlayerCheckpoints);
        _leaderboardService = leaderboardService;
        _leaderboardService.Register(this);
        _signalBus = signalBus;
        Name = "PlayerRaceTracker";
        IsPlayer = true;
    }
    

    protected override void OnTrigger()
    {
        base.OnTrigger();
        if (_lap != _checkpointBehaviour.LapCount)
        {
            _checkpointBehaviour.CheckpointActivate(_index);
        }
        else
        {
            IsStop = true;
        }
        if(_index == 0)
            _signalBus.Fire(new LapFinishedSignal(_lap));
    }
}
