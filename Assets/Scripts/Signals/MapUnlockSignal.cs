public class MapUnlockSignal 
{
    public MapConfig MapConfig { get; private set; }

    public MapUnlockSignal(MapConfig mapConfig)
    {
        MapConfig = mapConfig;
    }
}
