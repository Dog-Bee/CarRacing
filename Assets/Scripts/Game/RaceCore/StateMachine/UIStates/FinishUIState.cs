using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FinishUIState : AGameplayUIState
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI leaderboardText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SceneName]
    [SerializeField] private string sceneName;

    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
   
    
    private LeaderboardService _leaderboardService;
    
    [Inject] private void Construct(SceneLoader sceneLoader,LeaderboardService leaderboardService)
    {
        closeButton.onClick.AddListener(()=>sceneLoader.LoadScene(sceneName));
        _leaderboardService = leaderboardService;
    }

    public override void EnterState()
    {
        bool isWin = _leaderboardService.GetPlayerPlace() == 0;
        
        winUI.SetActive(isWin);
        loseUI.SetActive(!isWin);
        
        base.EnterState();
        leaderboardText.text = _leaderboardService.GetPlayer();
        
    }
    

}
