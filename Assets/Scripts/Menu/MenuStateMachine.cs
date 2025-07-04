using UnityEngine;
using Zenject;

public class MenuStateMachine : MonoBehaviour
{
    private IMenuState _prevState;
    private IMenuState _currentState;
    
    private SignalBus _signalBus;

    [Inject] private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<MenuStateChangeSignal>(OnMenuStateChangedSignal);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MenuStateChangeSignal>(OnMenuStateChangedSignal);
    }

    private void OnMenuStateChangedSignal(MenuStateChangeSignal signal)
    {
        if(signal.MenuState == _currentState)
        {
            _currentState.ExitState();
            _currentState = null;
            return;
        }
        _prevState = _currentState;
        _currentState = signal.MenuState;
        _prevState?.ExitState();
        _currentState.EnterState();
    }
}
