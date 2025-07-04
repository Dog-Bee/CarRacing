using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameUIStateMachine : MonoBehaviour,IGameStateUser
{
    [SerializeField] private AGameplayUIState start;
    [SerializeField] private AGameplayUIState loop;
    [SerializeField] private AGameplayUIState finish;
    
    private Dictionary<GamePlayState,AGameplayUIState> statesDictionary;

    private AGameplayUIState _currentGameplayUI;
    
    [Inject]
    private void Construct(GameplayObserver observer)
    {
        statesDictionary = new Dictionary<GamePlayState, AGameplayUIState>
        {
            { GamePlayState.Start ,start},
            { GamePlayState.Loop,loop},
            { GamePlayState.Finish,finish},
        };
        
        observer.Register(this);
    }

    public void OnGameStateChanged(GamePlayState newState)
    {
        _currentGameplayUI?.ExitState();
        _currentGameplayUI = statesDictionary[newState];
        _currentGameplayUI?.EnterState();
    }
}
