using System.Collections.ObjectModel;

namespace ChampionsAlgo.Entities;

public class Club
{
    public string Name;
    public string Nation;
    public double Coefficient;
    public List<Fixture> Fixtures;
    public DrawPot Pot;

    public Club(string name, string nation, double coefficient)
    {
        this.Name = name;
        this.Nation = nation;
        this.Coefficient = coefficient;
        this.Fixtures = new List<Fixture>();
    }

    private bool IsSameNation(Club otherTeam) => otherTeam.Nation.Equals(Nation);

    public DrawPot GetPot() => Pot;

    public bool InSamePot(Club club) => Pot == club.Pot;

    private bool CanPlayFromThisNation(Club otherTeam) 
        => HomeFixtures().Count(f => f.AwayClub.Nation.Equals(otherTeam.Nation)) <= 1
        && AwayFixtures().Count(f => f.HomeClub.Nation.Equals(otherTeam.Nation)) <= 1;

    public bool HasFixtureAgainst(Club otherTeam) => Fixtures.Exists
        (f => f.GetOtherTeam(this) == otherTeam);
    
    public bool CanPlay(Club otherTeam) => !IsSameNation(otherTeam) 
                                           && !HasFixtureAgainst(otherTeam);

    public bool CanPlayAtHome(Club otherTeam) => CanPlay(otherTeam) 
                                                 && NeedsHomeFixtureFromPot(otherTeam.Pot)
                                                 && HomeFixtures().Count < 4
                                                 && otherTeam.NeedsAwayFixtureFromPot(Pot)
                                                 && otherTeam.AwayFixtures().Count < 4;
    
    public bool CanPlayAtAway(Club otherTeam) => CanPlay(otherTeam)
                                                 && NeedsAwayFixtureFromPot(otherTeam.Pot) 
                                                 && AwayFixtures().Count < 4
                                                 && otherTeam.NeedsHomeFixtureFromPot(Pot)
                                                 && otherTeam.HomeFixtures().Count < 4;

    public bool NeedsHomeFixtureFromPot(DrawPot pot) => !Fixtures.Exists(f => pot.Contains(f.AwayClub)
                                                                              && f.AwayClub != this);

    public bool NeedsAwayFixtureFromPot(DrawPot pot) => !Fixtures.Exists(f => pot.Contains(f.HomeClub)
                                                                              && f.HomeClub != this);

    public override string ToString() => Name;

    public List<Fixture> HomeFixtures() => Fixtures.Where(x => x.HomeClub.Equals(this)).ToList();
    
    public List<Fixture> AwayFixtures() => Fixtures.Where(x => x.AwayClub.Equals(this)).ToList();
}