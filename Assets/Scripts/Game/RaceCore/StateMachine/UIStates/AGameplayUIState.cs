using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class AGameplayUIState : MonoBehaviour,IGameplayUI
{
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float fadeTime = 0.2f;

    public virtual void EnterState()
    {
        canvasGroup.DOFade(1,fadeTime).SetEase(Ease.Linear);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void ExitState()
    {
        canvasGroup.DOFade(0,fadeTime).SetEase(Ease.Linear);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
