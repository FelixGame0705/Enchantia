public class LeaderboardItemData
{
    public LeaderboardItemData(int rank, string name, int time, int wave, int score)
    {
        this.rank = rank;
        this.name = name;
        this.time = time;
        this.wave = wave;
        this.score = score;
    }
    public int rank;
    public string name;
    public int time;
    public int wave;
    public int score;
}