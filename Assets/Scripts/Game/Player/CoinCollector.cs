using UnityEngine;
using Zenject;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private bool availableAccumulate;
    private int _coinCount;
    private SignalBus _signalBus;
    private CoinService _coinService;
    [Inject]
    private void Construct(CoinService coinService)
    {
        _coinService = coinService;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Coin") return;
        other.gameObject.SetActive(false);
        
        if (!availableAccumulate) return;
        _coinCount++;
        _coinService.SetTempCoins(_coinCount);
    }
}
