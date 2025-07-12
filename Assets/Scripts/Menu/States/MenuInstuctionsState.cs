using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class MenuInstuctionsState : AMenuState, IMenuState
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    
    [Inject] private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    private void Start()
    {
        ExitImmediately();
    }

    public override void EnterState()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        canvasGroup.DOFade(1,fadeDuration).SetEase(Ease.Linear);
    }

    public override void ExitState()
    {
        
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
            
        canvasGroup.DOFade(0,fadeDuration).SetEase(Ease.Linear);
    }

    public override void EnterOverlap()
    {
    }

    public override void ExitOverlap()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0,fadeDuration).SetEase(Ease.Linear);
    }

    public override void EnterImmediately()
    {
        canvasGroup.alpha = 1;
        
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
    }

    public override void ExitImmediately()
    {
        canvasGroup.alpha = 0;
        
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
    }
}
