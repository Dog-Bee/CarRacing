public class CoinChangedSignal
{
    public int Coins { get; private set; }

    public CoinChangedSignal(int coins)
    {
        Coins = coins;
    }
}
