using System;
using TMPro;
using UnityEngine;
using Zenject;

public class GlobalCoinUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private const string TOKEN_NAME = "Token";
    
    private SignalBus _signalBus;


    [Inject] private void Construct(SignalBus signalBus, CoinService coinService)
    {
        _signalBus = signalBus;
        
        coinText.text = $"<sprite name={TOKEN_NAME}> {coinService.Coins}";
        
        _signalBus.Subscribe<CoinChangedSignal>(OnCoinChanged);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<CoinChangedSignal>(OnCoinChanged);
    }


    private void OnCoinChanged(CoinChangedSignal signal)
    {
        coinText.text = $"<sprite name={TOKEN_NAME}> {signal.Coins}";
    }
    
}
