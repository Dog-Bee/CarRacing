using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapLoadButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image lockImage;
    [SerializeField] private TextMeshProUGUI priceText;
    
    private const string TOKEN_NAME = "Token";
    
    private MapConfig _mapConfig;
    private ColorBlock _spriteState;
    
    private SignalBus _signalBus;
    
    public MapConfig MapConfig=>_mapConfig;
    
    public void Init(MapConfig mapConfig, SignalBus signalBus)
    {
        _mapConfig = mapConfig;
        _signalBus = signalBus;
        if(_mapConfig.MapSprite!=null)
        button.image.sprite = _mapConfig.MapSprite;
        _spriteState = button.colors;
        priceText.gameObject.SetActive(!_mapConfig.isUnlocked);
        priceText.text = $"<sprite name={TOKEN_NAME}>{_mapConfig.price}";
        button.onClick.AddListener(() =>
        {
            _signalBus.Fire(new TryMapLoadSignal(_mapConfig));
        });
        
        UpdateView(_mapConfig);
    }

    public void UpdateView(MapConfig mapConfig)
    {
        _mapConfig = mapConfig;
        _spriteState.normalColor = _mapConfig.isUnlocked ? Color.white : _spriteState.disabledColor;
        button.colors = _spriteState;
        priceText.gameObject.SetActive(!_mapConfig.isUnlocked);
        lockImage.gameObject.SetActive(!_mapConfig.isUnlocked);

    }
}
