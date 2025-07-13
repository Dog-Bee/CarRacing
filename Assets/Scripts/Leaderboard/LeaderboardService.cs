using System.Collections.Generic;
using System.Linq;

public class LeaderboardService
{
    private readonly List<IRaceProgress> _competitors = new();
    public List<IRaceProgress> SortedCompetitors => _competitors.OrderByDescending(c=>c.TotalProgress).ToList();
    public List<IRaceProgress> TimeSortedCompetitors => _competitors.Where(c=>c.IsStop).OrderBy(c=>c.TimeTrack).ToList();
 
    public void Register(IRaceProgress competitor)
    {
        if(!_competitors.Contains(competitor))
            _competitors.Add(competitor);
    }

    public string GetPlayer()
    {
        var player = SortedCompetitors.FirstOrDefault(s=>s.IsPlayer);
        int index = SortedCompetitors.IndexOf(player);
        return $"{index + 1}.{player.Name}";
    }

    public int GetPlayerPlace()
    {
        return SortedCompetitors.IndexOf(SortedCompetitors.FirstOrDefault(s=>s.IsPlayer));
    }

    public int GetFinishPlayerPlace()
    {
        return TimeSortedCompetitors.IndexOf(TimeSortedCompetitors.FirstOrDefault(s=>s.IsPlayer));
    }
    

    
}
