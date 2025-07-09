using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Button unlockedButton;
    [SerializeField] private Image image;

    private ColorConfig _config;
    private SignalBus _signalBus;
    private ColorBlock _colorBlock;

    public void Init(ColorConfig config, SignalBus signalBus)
    {
        _config = config;
        _signalBus = signalBus;
        _colorBlock = unlockedButton.colors;
        
        ButtonsInit();
    }

    public void ViewUpdate(ColorConfig config)
    {
        _config = config;
        _colorBlock.normalColor = _config.IsUnlocked? Color.white : _colorBlock.disabledColor;
        unlockedButton.colors= _colorBlock;
        unlockedButton.image.color = _config.Color;
        image.gameObject.SetActive(!_config.IsUnlocked);
    }

    private void ButtonsInit()
    {
        ViewUpdate(_config);
        
        unlockedButton.onClick.AddListener(() =>
        {
            _signalBus.Fire(new TryColorChangeSignal(_config));
        });
    }
    
}
