using UnityEngine;

public class TryColorChangeSignal
{
    public ColorConfig Config { get; private set; }

    public TryColorChangeSignal(ColorConfig config)
    {
        Config = config;
    }
}
