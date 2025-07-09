using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkinShopSection : MonoBehaviour
{
    [SerializeField] private List<SkinButton> skinButtons;
    [SerializeField] private PartType partType;

    private List<ColorConfig> _colorConfigs;
    
    private SignalBus _signalBus;
    private SkinSystem _skinSystem;
    
    [Inject]
    private void Construct(SignalBus signalBus, SkinSystem skinSystem)
    {
        _signalBus = signalBus;
        _skinSystem = skinSystem;
        _signalBus.Subscribe<ColorChangedSignal>(OnColorChanged);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _colorConfigs = _skinSystem.GetConfigs(partType);
        for (int i = 0; i < skinButtons.Count; i++)
        {
            skinButtons[i].Init(_colorConfigs[i],_signalBus);
        }
    }

    private void ButtonsUpdate()
    {
        _colorConfigs = _skinSystem.GetConfigs(partType);
        for (int i = 0; i < skinButtons.Count; i++)
        {
            skinButtons[i].ViewUpdate(_colorConfigs[i]);
        }
    }

    private void OnColorChanged(ColorChangedSignal signal)
    {
        ColorConfig config = signal.Config;
        if (config.PartType != partType) return;
        ButtonsUpdate();
        
    }
    
}
