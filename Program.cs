using System.Collections.ObjectModel;
using ChampionsAlgo.Entities;

namespace ChampionsAlgo
{
    class Program
    {
        public static string filename = "/Users/anthonynkyi/Documents/footWork/ChampionsAlgo/Resources/clubs2425.csv";
        public static void PrintClubsInPots(Collection<DrawPot> allPots)
        {
            foreach (var drawPot in allPots)
            {
                Console.Write(drawPot.PotNumber + ": ");
                foreach (var club in drawPot.Clubs)
                {
                    Console.Write(club + ". ");
                }
                Console.WriteLine();
            }
        }
        
        public static void PrintFixtures(List<Fixture> fixtures)
        {
            fixtures.ForEach(Console.WriteLine);
        }
        
        public static void Main()
        {
            var allClubs = ClubsFactory.GenerateClubs(filename);
            var champ = allClubs.First(c => c.Name.Equals("Real Madrid"));
            var allPots = PotFactory.GeneratePots
                (allClubs, champ, 4);
            
            var fixtures = new List<Fixture>();
            
            
            PrintClubsInPots(allPots);
            foreach (var club in allPots[0].Clubs)
            {
                FixtureFactory.GenerateFixturesFor(club, allPots, fixtures);
                //backtrack when error to solve
            }
            Console.WriteLine(allClubs.Count(c => c.AwayFixtures().Count == 4 && c.HomeFixtures().Count == 4));
            Console.WriteLine(allClubs.Count(c => c.NeedsHomeFixtureFromPot(allPots[0]) || c.NeedsAwayFixtureFromPot(allPots[0])));
        }
    }
}