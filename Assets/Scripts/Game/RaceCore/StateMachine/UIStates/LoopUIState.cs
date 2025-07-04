using TMPro;
using UnityEngine;
using Zenject;

public class LoopUIState : AGameplayUIState
{
    [SerializeField] private TextMeshProUGUI lapCounter;

    private int _lapCount;
    private SignalBus _signalBus;

    [Inject] private void Construct(SignalBus signalBus,CheckpointBehaviour checkpointBehaviour)
    {
        _signalBus = signalBus;
        _lapCount = checkpointBehaviour.LapCount;
        signalBus.Subscribe<LapFinishedSignal>(OnLapFinished);
        lapCounter.text = $"LAP\n0/{_lapCount}";
    }

    private void OnLapFinished(LapFinishedSignal signal)
    {
        lapCounter.text = $"LAP\n{signal.LapNumber}/{_lapCount}";
        if (signal.LapNumber == _lapCount)
        {
            _signalBus.Fire(new GameplayStateChangedSignal(GamePlayState.Finish));
        }
    }
}
