using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartUIState : AGameplayUIState
{
    [SerializeField] private List<Image> lightImages;
    [SerializeField] private float countDownTime;
    [SerializeField] private List<Color> lightColors;

    private float _timer;
    private int _index;
    private bool _isActive;

    private SignalBus _signalBus;

    [Inject] private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Update()
    {
        if (!_isActive) return;
        _timer += Time.deltaTime;
        if (_timer >= countDownTime)
        {
            _timer -= countDownTime;
            _index++;
            TrafficLightChange(_index);
        }
    }

    public override void EnterState()
    {
        base.EnterState();
        TrafficLightChange();
        _isActive = true;
    }

    public override void ExitState()
    {
        base.ExitState();
        _isActive = false;
    }

    private void TrafficLightChange(int index = 0)
    {
        if (index >= lightColors.Count)
        {
            _signalBus.Fire(new GameplayStateChangedSignal(GamePlayState.Loop));
            return;
        }

        lightImages.ForEach(l => l.color = lightColors[index]);
    }
}