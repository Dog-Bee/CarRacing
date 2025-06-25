using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectedSignal
{
    public int CoinCount { get; }

    public CoinCollectedSignal(int coinCount)
    {
        CoinCount = coinCount;
    }
    
}
