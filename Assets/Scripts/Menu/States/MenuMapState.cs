using System;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Vector3 = System.Numerics.Vector3;

public class MenuMapState : AMenuState,IMenuState
{
    [SerializeField] private CanvasGroup canvasGroup;
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
        //Vector2 targetPosition =  Vector2.right*selfRect.sizeDelta.x/2;
        
        Debug.Log("MenuMapState.EnterState");
        
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        canvasGroup.DOFade(1,fadeDuration).SetEase(Ease.Linear);
        //selfRect.DOAnchorPos(targetPosition,fadeDuration).SetEase(Ease.Linear);
    }

    public override void ExitState()
    {
        //Vector2 targetPosition =  Vector2.right*Screen.width/2;
        
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
            
        canvasGroup.DOFade(0,fadeDuration).SetEase(Ease.Linear);
        //selfRect.DOAnchorPos(targetPosition,fadeDuration).SetEase(Ease.Linear);
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
        //Vector2 targetPosition =  Vector2.right*selfRect.sizeDelta.x/2;;
        
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        
        //selfRect.anchoredPosition = targetPosition;
    }

    public override void ExitImmediately()
    {
        canvasGroup.alpha = 0;
        //Vector2 targetPosition = Vector2.right*Screen.width/2;
        
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        //selfRect.anchoredPosition = targetPosition;
    }
}
