using System.Collections.Generic;
using System.Linq;

public class LeaderboardService
{
    private readonly List<IRaceProgress> _competitors = new();
    public IReadOnlyList<IRaceProgress> SortedCompetitors => _competitors.OrderByDescending(c=>c.TotalProgress).ToList();

    public void Register(IRaceProgress competitor)
    {
        if(!_competitors.Contains(competitor))
            _competitors.Add(competitor);
    }

    
}
