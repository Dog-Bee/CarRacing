using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum GamePlayState
{
    Start,
    Loop,
    Finish,
}
public class GameplayObserver : MonoBehaviour
{
    private SignalBus _signalBus;
    private List<IGameStateUser> _users =new();
    
    [Inject] private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<GameplayStateChangedSignal>(ChangeState);
    }

    private void Start()
    {
        _signalBus.Fire(new GameplayStateChangedSignal(GamePlayState.Start));
    }

    private void ChangeState(GameplayStateChangedSignal signal)
    {
        _users.ForEach(u=>u.OnGameStateChanged(signal.GamePlayState));
    }

    public void Register(IGameStateUser user)
    {
        if (_users.Contains(user)) return;
        _users.Add(user);
    }
    
}
