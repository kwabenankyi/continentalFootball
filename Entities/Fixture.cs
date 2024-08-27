namespace ChampionsAlgo.Entities;

public class Fixture
{
    public Club HomeClub;
    public Club AwayClub;

    public Fixture(Club homeClub, Club awayClub)
    {
        this.HomeClub = homeClub;
        this.AwayClub = awayClub;
    }

    public void Flip() => (HomeClub, AwayClub) = (AwayClub, HomeClub);

    public Club GetOtherTeam(Club inputTeam) => inputTeam == HomeClub ? AwayClub : 
                                                inputTeam == AwayClub ? HomeClub: null;

    public bool Equals(Fixture fixture)
    {
        return fixture.HomeClub.Equals(HomeClub) && fixture.AwayClub.Equals(AwayClub);
    }

    public override string ToString()
    {
        return $"{HomeClub} vs {AwayClub}";
    }
}