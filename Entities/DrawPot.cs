namespace ChampionsAlgo.Entities;

public class DrawPot
{
    public List<Club> Clubs;
    public int PotNumber;
    public Random rnd;
    
    public DrawPot(List<Club> clubs, int potNumber)
    {
        this.Clubs = clubs;
        this.PotNumber = potNumber;
        this.rnd = new Random();
    }

    public DrawPot(int potNumber) : this(new List<Club>(), potNumber) {}

    public bool Contains(Club club) => Clubs.Contains(club);

    public List<Club> GetPossibleHomeOpponents(Club inputTeam, DrawPot inputPot)
    {
        /*var disallowedClubs = Clubs.Where
        (thisPotClub => 
            inputPot.Clubs.Any(inputPotClub => 
                inputPotClub.NeedsHomeFixtureFromPot(this) 
                && Clubs.Where(c => inputPotClub.CanPlayAtHome(c)).Contains(thisPotClub)
                && Clubs.Count(c => inputPotClub.CanPlayAtHome(c)) == 1));
        
        return Clubs.Where(c => inputTeam.CanPlayAtHome(c) && !disallowedClubs.Contains(c)).ToList();*/
        return Clubs.Where(c => inputTeam.CanPlayAtHome(c) && c.CanPlayAtAway(inputTeam)).ToList();
    }
    public List<Club> GetPossibleAwayOpponents(Club inputTeam, DrawPot inputPot)
    {
        /*var disallowedClubs = Clubs.Where
        (thisPotClub => 
            inputPot.Clubs.Any(inputPotClub => 
                inputPotClub.NeedsAwayFixtureFromPot(this) 
                && Clubs.Where(c => inputPotClub.CanPlayAtAway(c)).Contains(thisPotClub)
                && Clubs.Count(c => inputPotClub.CanPlayAtAway(c)) == 1));
        
        return Clubs.Where(c => inputTeam.CanPlayAtAway(c) && !disallowedClubs.Contains(c)).ToList();*/
        return Clubs.Where(c => inputTeam.CanPlayAtAway(c) && c.CanPlayAtHome(inputTeam)).ToList();
        // and if there are no clubs from pot that will have empty opponents if this club is selected
    }
    public Club GetClubToPlayAtHome(Club inputTeam, DrawPot pot)
    {
        var potentialOpps = GetPossibleHomeOpponents(inputTeam, pot); 
        return potentialOpps[rnd.Next(potentialOpps.Count)];
    }
    public Club GetClubToPlayAtAway(Club inputTeam, DrawPot pot)
    {
        var potentialOpps = GetPossibleAwayOpponents(inputTeam, pot); 
        return potentialOpps[rnd.Next(potentialOpps.Count)];
    }

    public override string ToString()
    {
        return string.Join(", ", Clubs);
    }
}