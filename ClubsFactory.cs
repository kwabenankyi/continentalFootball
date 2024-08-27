using System.Collections.ObjectModel;
using ChampionsAlgo.Entities;

namespace ChampionsAlgo;

public static class ClubsFactory
{
    public static ObservableCollection<Club> GenerateClubs(String fileName)
    {
        var Clubs = new ObservableCollection<Club>();
        
        using (var reader = new StreamReader(fileName))
        {
            while (!reader.EndOfStream)
            {
                var values = reader.ReadLine().Split(',');
                Clubs.Add(new Club(values[0], values[1], Double.Parse(values[2])));
            }
        }

        return Clubs;
    }
}