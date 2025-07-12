using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

public class SkinSystem : MonoBehaviour
{
    [SerializeField] private VehicleColorConfig vehicleColorConfig;
    [SerializeField] private SkinPopUp skinPopUp;
    [SerializeField] private ASkinChanger aSkinChanger;
    
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<TryColorChangeSignal>(OnColorChanged);
        vehicleColorConfig.Init();
    }


    public List<ColorConfig> GetConfigs(PartType partType)
    {
        switch (partType)
        {
            case PartType.Body:
                return vehicleColorConfig.BodyColorConfigs.Value;
            case PartType.Wheels:
                return vehicleColorConfig.WheelColorConfigs.Value;
            case PartType.Steering:
                return vehicleColorConfig.SteeringColorConfigs.Value;
            case PartType.Tube:
                return vehicleColorConfig.TubeColorConfigs.Value;
            default:
                return null;
        }
    }

    private void OnColorChanged(TryColorChangeSignal signal)
    {
        var config = signal.Config;
        
        if (!config.IsUnlocked)
        {
            skinPopUp.Activate(config);
            return;
        }
        
        vehicleColorConfig.SaveColor(config);
        aSkinChanger.UpdateSkin(config);
        _signalBus.Fire(new ColorChangedSignal(config));

    }

    
    
    
    
    


}
