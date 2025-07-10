using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkinPopUp : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.25f;
    [SerializeField] private Image colorImage;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI priceText;

    private const string TOKEN_NAME = "Token";
    
    private SignalBus _signalBus;
    private CoinService _coinService;
    private ColorConfig _config;
    
    public Action<ColorConfig> OnBuyCallback;

    [Inject]
    private void Construct(SignalBus signalBus, CoinService coinService)
    {
        _signalBus = signalBus;
        _coinService = coinService;
        Deactivate();
    }
    
    public void Activate(ColorConfig config)
    {
        _config = config;
        
        buyButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        
        priceText.text = $"for {_config.Price}    <sprite name={TOKEN_NAME}>";
        
        ButtonsInitialize();
        
        canvasGroup.DOFade(1,fadeDuration).SetEase(Ease.Linear);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        colorImage.color = _config.Color;
    }

    private void Deactivate()
    {
        canvasGroup.DOFade(0,fadeDuration).SetEase(Ease.Linear);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ButtonsInitialize()
    {
        buyButton.interactable = _coinService.IsEnoughCoins(_config.Price);
        
        buyButton.onClick.AddListener(() =>
        {
            _config.IsUnlocked = true;
            _signalBus.Fire(new TryColorChangeSignal(_config));
            Deactivate();
        });
        
        cancelButton.onClick.AddListener(Deactivate);
    }
}
