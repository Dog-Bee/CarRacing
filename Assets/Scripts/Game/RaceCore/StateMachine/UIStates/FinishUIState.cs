using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FinishUIState : AGameplayUIState
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI leaderboardText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SceneName]
    [SerializeField] private string sceneName;

    [Header("ResultPanels")]
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private List<TextMeshProUGUI> prizeTexts;
   
    
    private const string TOKEN_NAME = "Token";
    
    private LeaderboardService _leaderboardService;
    private CoinService _coinService;
    [Inject] private void Construct(SceneLoader sceneLoader,LeaderboardService leaderboardService,CoinService coinService)
    {
        mainMenuButton.onClick.AddListener(()=>sceneLoader.LoadScene(sceneName));
        restartButton.onClick.AddListener(()=>sceneLoader.RestartScene());
        _coinService = coinService;
        _leaderboardService = leaderboardService;
    }

    public override void EnterState()
    {
        Debug.Log($"Player place {_leaderboardService.GetPlayerPlace()}");
        bool isWin = _leaderboardService.GetPlayerPlace() == 0;
        
        if (isWin)
        {
            _coinService.AddCoins(_coinService.TempCoins);
        }
        
        prizeTexts.ForEach(t =>
        {
            t.text = $"<sprite name={TOKEN_NAME}> +{_coinService.TempCoins} Tokens";
        });
        
        winUI.SetActive(isWin);
        loseUI.SetActive(!isWin);
        
        base.EnterState();
        leaderboardText.text = _leaderboardService.GetPlayer();
        _coinService.ResetTempCoins();

    }
    

}
