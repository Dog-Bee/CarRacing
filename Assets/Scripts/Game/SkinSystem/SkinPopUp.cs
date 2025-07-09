using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    private SignalBus _signalBus;
    private ColorConfig _config;
    public Action<ColorConfig> OnBuyCallback;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        Deactivate();
    }
    
    public void Activate(ColorConfig config)
    {
        buyButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        
        ButtonsInitialize();
        
        canvasGroup.DOFade(1,fadeDuration).SetEase(Ease.Linear);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        _config = config;
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
        buyButton.onClick.AddListener(() =>
        {
            _config.IsUnlocked = true;
            _signalBus.Fire(new TryColorChangeSignal(_config));
            Deactivate();
        });
        
        cancelButton.onClick.AddListener(Deactivate);
    }
}
