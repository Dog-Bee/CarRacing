using UnityEngine;
using Zenject;

public class CoinService
{
    private int _coins;

    private int _tempCoins;

    public int TempCoins => _tempCoins;
    public int Coins => saveCoins.Value;
    private JsonSavableValue<int> saveCoins;

    [Inject] private SignalBus _signalBus;

    public CoinService()
    {
        saveCoins = new("Coins", 9999);
        Debug.Log($"Coin service init coin value: {saveCoins.Value}");
        _coins = saveCoins.Value;
    }

    public void SetTempCoins(int coins)
    {
        _tempCoins = coins;
    }

    public void ResetTempCoins()
    {
        _tempCoins = 0;
    }

    public bool IsEnoughCoins(int amount)
    {
        return _coins >= amount;
    }
    
    public void AddCoins(int coins)
    {
        Debug.Log($"Add coins now is: {saveCoins.Value}");

        _coins += coins;
        SaveCoins();
        
    }

    public void SpendCoins(int coins)
    {
        if (_coins < coins) return;
        _coins -= coins;
        SaveCoins();
    }

    private void SaveCoins()
    {
        saveCoins.Value = _coins;
        _signalBus.Fire(new CoinChangedSignal(_coins));
    }
}