using TMPro;
using UnityEngine;
using Zenject;

public class LoopUIState : AGameplayUIState
{
    [SerializeField] private TextMeshProUGUI lapCounter;
    [SerializeField] private TextMeshProUGUI coinText;

    private int _lapCount;
    private SignalBus _signalBus;
    private CoinService _coinService;

    [Inject] private void Construct(SignalBus signalBus,CheckpointBehaviour checkpointBehaviour,CoinService coinService)
    {
        _signalBus = signalBus;
        _lapCount = checkpointBehaviour.LapCount;
        _coinService = coinService;
        signalBus.Subscribe<LapFinishedSignal>(OnLapFinished);
        lapCounter.text = $"LAP\n0/{_lapCount}";
    }

    private void Update()
    {
        coinText.text = $"<sprite name=Token>{_coinService.TempCoins}";
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
