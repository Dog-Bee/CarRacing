using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryMapLoadSignal : MonoBehaviour
{
    public MapConfig Config { get; private set; }

    public TryMapLoadSignal(MapConfig config)
    {
        Config = config;
    }
}
