using System.Collections.ObjectModel;
using ChampionsAlgo.Entities;

namespace ChampionsAlgo
{
    class Program
    {
        private static string filename = @"../../../Resources/uclclubs2425.csv";
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

        public static List<Fixture> GenerateFixtures(Collection<DrawPot> allPots)
        {
            var generatedFixtures = new List<Fixture>();
            
            foreach (var pot in allPots)
            {
                Console.WriteLine($"Generating fixtures for {pot}.");
                while (true)
                {
                    var fixtures = new List<Fixture>();
                    try
                    {
                        pot.Clubs.ForEach(club => FixtureFactory.GenerateFixturesFor(club, allPots, fixtures));
                        generatedFixtures.AddRange(fixtures);
                        break;
                    }
                    catch (Exception)
                    {
                        fixtures.Where(f => !generatedFixtures.Contains(f)).ToList()
                            .ForEach(f => FixtureFactory.RemoveFixture(f));
                    }
                }
            }

            return generatedFixtures;
        }
        
        public static void Main()
        {
            var allClubs = ClubsFactory.GenerateClubs(filename);
            var champ = allClubs.First(c => c.Name.Equals("Real Madrid"));
            
            var generatedFixtures = GenerateFixtures(PotFactory.GeneratePots(allClubs, champ, 4));
            Console.WriteLine();
            FixtureFactory.ExportFixtures(generatedFixtures, "fixtures2.csv");
        }
    }
}