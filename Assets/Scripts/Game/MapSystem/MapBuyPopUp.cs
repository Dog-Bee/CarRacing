using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapBuyPopUp : MonoBehaviour
{
    [SerializeField] private float fadeDuration = .25f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private TextMeshProUGUI priceText;
    
    
    private const string TOKEN_NAME = "Token";
    private SignalBus _signalBus;
    private CoinService _coinService;
    private MapConfig _mapConfig;



    [Inject] private void Construct(SignalBus signalBus, CoinService coinService)
    {
        _signalBus = signalBus;
        _coinService = coinService;
        Deactivate();
    }

    public void Activate(MapConfig mapConfig)
    {
        _mapConfig = mapConfig;
        buyButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        priceText.text = $"for {_mapConfig.price}   <sprite name={TOKEN_NAME}>";
        
        ButtonsInitialize();
        
        canvasGroup.DOFade(1,fadeDuration).SetEase(Ease.Linear);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        mapImage.sprite = _mapConfig.MapSprite;

    }

    public void Deactivate()
    {
        canvasGroup.DOFade(0,fadeDuration).SetEase(Ease.Linear);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ButtonsInitialize()
    {
        buyButton.interactable = _coinService.IsEnoughCoins(_mapConfig.price);
        
        buyButton.onClick.AddListener(() =>
        {
            _mapConfig.isUnlocked = true;
            _signalBus.Fire(new MapUnlockSignal(_mapConfig));
            Deactivate();
        });
        
        cancelButton.onClick.AddListener(Deactivate);
    }
}
