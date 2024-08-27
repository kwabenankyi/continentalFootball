using System.Collections.ObjectModel;
using ChampionsAlgo.Entities;

namespace ChampionsAlgo;

public static class FixtureFactory
{
    private static readonly Random rnd;
    public static void SetFixture(Fixture fixture, Club home, Club away)
    {
        home.Fixtures.Add(fixture);
        away.Fixtures.Add(fixture);
        Console.WriteLine(fixture);
    }

    public static void RemoveFixture(Fixture fixture, Club home, Club away)
    {
        home.Fixtures.Remove(fixture);
        away.Fixtures.Remove(fixture);
    }

    public static void GenerateFixturesFor(Club inClub, Collection<DrawPot> allPots, List<Fixture> allFix)
    {
        var prevFix = new Fixture(null, null);
        if (inClub.HomeFixtures().Count < 4)
        {
            foreach (var pot in allPots)
            {
                if (inClub.NeedsHomeFixtureFromPot(pot))
                {
                    var opponent = pot.GetClubToPlayAtHome(inClub, inClub.GetPot());
                    var newFix = new Fixture(inClub, opponent);
                    SetFixture(newFix, inClub, opponent);
                    allFix.Add(newFix);
                    /*Club possibleOpponent = null;
                    try
                    {
                        possibleOpponent = pot.GetClubToPlayAtHome(inClub, inClub.GetPot());
                    }
                    catch (Exception e)
                    {
                        var currentIndex = allFix.IndexOf(prevFix);
                        var homeOpps = pot.GetPossibleAwayOpponents(prevFix.HomeClub, pot)
                            .Where(x => x != prevFix.AwayClub).ToList();
                        while (!homeOpps.Any())
                        {
                            prevFix = allFix[currentIndex--];
                            homeOpps = pot.GetPossibleAwayOpponents(prevFix.HomeClub, pot)
                                .Where(x => x != prevFix.AwayClub).ToList();
                        }

                        var resetFix = new Fixture(prevFix.HomeClub,
                            homeOpps[rnd.Next(homeOpps.Count)]);

                        RemoveFixture(prevFix, prevFix.HomeClub, prevFix.AwayClub);
                        allFix.Remove(prevFix);

                        SetFixture(resetFix, resetFix.HomeClub, resetFix.AwayClub);
                        allFix.Add(resetFix);
                        prevFix = resetFix;
                    }
                    finally
                    {
                        possibleOpponent ??= pot.GetClubToPlayAtHome(inClub, inClub.GetPot());
                        var newFix = new Fixture(inClub, possibleOpponent);
                        SetFixture(newFix, inClub, possibleOpponent);
                        allFix.Add(newFix);
                        prevFix = newFix;
                    }*/
                }
            }
        }
        while (inClub.AwayFixtures().Count < 4)
        {
            foreach (var pot in allPots)
            {
                if (inClub.NeedsAwayFixtureFromPot(pot))
                {
                    var opponent = pot.GetClubToPlayAtAway(inClub, inClub.GetPot());
                    var newFix = new Fixture(opponent, inClub);
                    SetFixture(newFix, opponent, inClub);
                    allFix.Add(newFix);
                }
            }
        }
    }
    
    /*public static void RearrangeFixtures(List<Club> allClubs, ObservableCollection<DrawPot> allPots,
                                                     List<Fixture> allFixtures)
        {
            var clubsWithTooManyFixtures = allClubs.Where(x => x.Fixtures.Count > 8).ToList();
            var clubsWithTooFewFixtures = allClubs.Where(x => x.Fixtures.Count < 8).ToList();
            foreach (var club in clubsWithTooManyFixtures)
            {
                // get club 1's pot, then first eligible club in same pot that
                // needs fixture against club 1's opponent
                var currentPot = club.GetPot(allPots);
                
                var currentClubHomeSurplus = club.HomeFixtures().
                    Where(f => club.HomeFixtures().
                        Select(g => g.AwayClub.GetPot(allPots)).
                        Count(h => h == f.AwayClub.GetPot(allPots)) > 2);
                var currentClubAwaySurplus = club.AwayFixtures().
                    Where(f => club.AwayFixtures().
                        Select(g => g.HomeClub.GetPot(allPots)).
                        Count(h => h == f.HomeClub.GetPot(allPots)) > 2);
    
                foreach (var fix in currentClubHomeSurplus)
                {
                    try
                    {
                        var replacementClub = clubsWithTooFewFixtures.First(x => currentPot.Contains(x)
                                                                                 && x.NeedsHomeFixtureFromPot(
                                                                                     fix.AwayClub.GetPot(allPots)));
                        RemoveFixture(fix, fix.HomeClub, fix.AwayClub);
                        fix.HomeClub = replacementClub;
                        SetFixture(fix, replacementClub, fix.AwayClub);
                    }
                    catch (Exception e) {}
                }
                foreach (var fix in currentClubAwaySurplus)
                {
                    try
                    {
                        var replacementClub = clubsWithTooFewFixtures.First(x => currentPot.Contains(x)
                                                                                 && x.NeedsAwayFixtureFromPot(
                                                                                     fix.HomeClub.GetPot(allPots)));
                        RemoveFixture(fix, fix.HomeClub, fix.AwayClub);
                        fix.AwayClub = replacementClub;
                        SetFixture(fix, fix.HomeClub, replacementClub);
                    }
                    catch (Exception e) {}
                }
            }
        }*/
}