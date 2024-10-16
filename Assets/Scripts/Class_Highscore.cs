using System;

[Serializable]
public class Highscore 
{
    public string pseudo;
    public int points;

    public Highscore(string pseudo, int points)
    {
        this.pseudo = pseudo;
        this.points = points;
    }
}
