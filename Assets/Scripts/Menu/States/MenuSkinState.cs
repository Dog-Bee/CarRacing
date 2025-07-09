using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class MenuSkinState : AMenuState, IMenuState
{
    [SerializeField] private List<CanvasGroup> canvasGroup;
    [SerializeField] private RectTransform targetRect;
    [SerializeField] private RectTransform selfRect;
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
        Vector2 targetPosition =  Vector2.right*selfRect.sizeDelta.x/2;
        Debug.Log("MenuStartState.EnterState");
        
        canvasGroup.ForEach(c =>
        {
            c.interactable = true;
            c.blocksRaycasts = true;
            c.DOFade(1,fadeDuration).SetEase(Ease.Linear);
        });
        
        selfRect.DOAnchorPos(targetPosition,fadeDuration).SetEase(Ease.Linear);
    }

    public override void ExitState()
    {
        Vector2 targetPosition =  Vector2.right*Screen.width/2;
        
        canvasGroup.ForEach(c =>
        {
            c.interactable = false;
            c.blocksRaycasts = false;
            c.DOFade(0,fadeDuration).SetEase(Ease.Linear);
        });
        selfRect.DOAnchorPos(targetPosition,fadeDuration).SetEase(Ease.Linear);
    }

    public override void EnterOverlap()
    {
        
    }

    public override void ExitOverlap()
    {
        canvasGroup.ForEach(c =>
        {
            c.interactable = false;
            c.blocksRaycasts = false;
            c.DOFade(0,fadeDuration).SetEase(Ease.Linear);
        });
    }

    public override void EnterImmediately()
    {
        Vector2 targetPosition =  Vector2.right*selfRect.sizeDelta.x/2;
        
        canvasGroup.ForEach(c =>
        {
            c.interactable = false;
            c.blocksRaycasts = false;
            c.alpha = 1;
        });
        
        selfRect.anchoredPosition = targetPosition;
    }

    public override void ExitImmediately()
    {
        Vector2 targetPosition = Vector2.right*Screen.width/2;
        
        canvasGroup.ForEach(c =>
        {
            c.interactable = false;
            c.blocksRaycasts = false;
            c.alpha = 0;
        });
        
        selfRect.anchoredPosition = targetPosition;
    }
}
