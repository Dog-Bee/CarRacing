using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CoinHandler : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private List<Transform> coins;
    

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<LapFinishedSignal>(OnLapFinished);
    }
    void Update()
    {
        foreach (var c in coins.Where(c=>c.gameObject.activeSelf))
        {
            c.Rotate(Vector3.up,rotationSpeed*Time.deltaTime,Space.Self);
        }
    }

    private void OnLapFinished()
    {
        coins.ForEach(c => c.gameObject.SetActive(true));
    }
}
