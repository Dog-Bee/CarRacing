using System;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Vector3 = System.Numerics.Vector3;

public class MenuStartState : AMenuState,IMenuState
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;
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
        Debug.Log("MenuStartState.EnterState");
        canvasGroup.DOFade(1,fadeDuration).SetEase(Ease.Linear);
        rectTransform.DOScale(Vector2.one,fadeDuration).From(Vector2.zero).SetEase(Ease.Linear);
    }

    public override void ExitState()
    {
        canvasGroup.DOFade(1,fadeDuration).SetEase(Ease.Linear);
        rectTransform.DOScale(Vector2.zero,fadeDuration).From(Vector2.one).SetEase(Ease.Linear);
    }

    public override void EnterOverlap()
    {
    }

    public override void ExitOverlap()
    {
    }

    public override void EnterImmediately()
    {
        canvasGroup.alpha = 1;
        rectTransform.localScale = Vector2.one;
    }

    public override void ExitImmediately()
    {
        canvasGroup.alpha = 0;
        rectTransform.localScale = Vector2.zero;
    }
}
