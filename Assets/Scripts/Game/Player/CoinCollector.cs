using UnityEngine;
using Zenject;

public class CoinCollector : MonoBehaviour
{
    private int _coinCount;
    private SignalBus _signalBus;
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Coin") return;
        _coinCount++;
        other.gameObject.SetActive(false);
        _signalBus.Fire(new CoinCollectedSignal(_coinCount));
    }
}
