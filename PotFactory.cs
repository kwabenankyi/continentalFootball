using System.Collections.ObjectModel;
using ChampionsAlgo.Entities;

namespace ChampionsAlgo;

public static class PotFactory
{
    public static ObservableCollection<DrawPot> GeneratePots(ObservableCollection<Club> clubs, Club tournamentWinner, int numOfPots)
    {
        var potSize = clubs.Count / numOfPots;
        var neededPots = new ObservableCollection<DrawPot>();
        clubs.OrderBy(x=>x.Coefficient).ToList();
        clubs.Remove(tournamentWinner);
        clubs.Insert(0, tournamentWinner);
        for (var i = 0; i < numOfPots; i++)
        {
            var newPot = new DrawPot(clubs.Skip(potSize * i).Take(potSize).ToList(), i + 1);
            newPot.Clubs.ForEach(x => x.Pot = newPot);
            neededPots.Add(newPot);
        }
        return neededPots;
    }
}