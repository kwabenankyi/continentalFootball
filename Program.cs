using System.Collections.ObjectModel;
using ChampionsAlgo.Entities;

namespace ChampionsAlgo
{
    class Program
    {
        public static string filename = "clubs2425.csv";
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
            var generatedFixtures = new List<Fixture>();
            
            PrintClubsInPots(allPots);
            foreach (var pot in allPots)
            {
                while (pot.Clubs.Any(c => c.Fixtures.Count < 8))
                {
                    try
                    {
                        foreach (var club in pot.Clubs)
                        {
                            FixtureFactory.GenerateFixturesFor(club, allPots, fixtures);
                            //backtrack when error to solve
                        }
                    }
                    catch (Exception e)
                    {
                        foreach (Fixture f in fixtures.Where(f => !generatedFixtures.Contains(f)))
                        {
                            FixtureFactory.RemoveFixture(f);
                        }
                        fixtures = generatedFixtures;
                    }
                    generatedFixtures.AddRange(fixtures);
                    fixtures = new List<Fixture>();
                }
            }
           
            fixtures.ForEach(Console.WriteLine);
            Console.WriteLine(allClubs.Count(c => c.AwayFixtures().Count == 4 && c.HomeFixtures().Count == 4));
            Console.WriteLine(fixtures.Count);
        }
    }
}