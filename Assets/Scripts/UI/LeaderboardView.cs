using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class LeaderboardView : MonoBehaviour
{
   [SerializeField] private List<TextMeshProUGUI> leaderboardTexts;
   [SerializeField] private float updateInterval = 2f;

   private float _timer;
   private LeaderboardService _leaderboardService;

   [Inject] private void Construct(LeaderboardService leaderboardService)
   {
      _leaderboardService = leaderboardService;
   }

   private void Update()
   {
      _timer+=Time.deltaTime;
      if (_timer > updateInterval)
      {
         _timer = 0;
         UpdateLeaderboard();
      }
   }

   private void UpdateLeaderboard()
   {
      var competitorsList = _leaderboardService.SortedCompetitors;
      for (int i = 0; i < competitorsList.Count; i++)
      {
         leaderboardTexts[i].text = $"{i+1}. {competitorsList[i].Name}     {competitorsList[i].TotalProgress}";
         
      }
   }

}
