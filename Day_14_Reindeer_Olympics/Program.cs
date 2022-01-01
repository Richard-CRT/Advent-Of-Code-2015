using AdventOfCodeUtilities;

void P1(List<Reindeer> reindeers)
{
    List<Reindeer> TravellingReindeer = new List<Reindeer>(reindeers);
    List<Reindeer> RestingReindeer = new List<Reindeer>();

    const int finishTime = 2503;

    for (int timePassed = 0; timePassed < finishTime;)
    {
        int minDurationUntilStateChange = finishTime - timePassed;
        foreach (Reindeer rd in reindeers)
            minDurationUntilStateChange = Math.Min(rd.DurationUntilStateChange, minDurationUntilStateChange);
        //Console.WriteLine($"{timePassed} - {finishTime - timePassed} - {minDurationUntilStateChange}");

        foreach (Reindeer rd in reindeers)
        {
            if (!rd.Resting)
                rd.Distance += minDurationUntilStateChange * rd.Speed;

            rd.DurationUntilStateChange -= minDurationUntilStateChange;
            if (rd.DurationUntilStateChange == 0)
            {
                if (rd.Resting)
                    rd.DurationUntilStateChange = rd.TravelDuration;
                else
                    rd.DurationUntilStateChange = rd.RestDuration;
                rd.Resting = !rd.Resting;
            }
        }
        timePassed += minDurationUntilStateChange;
    }

    Int64 furthestDistance = Int64.MinValue;
    foreach (Reindeer rd in reindeers)
        furthestDistance = Math.Max(rd.Distance, furthestDistance);

    Console.WriteLine(furthestDistance);
    Console.ReadLine();
}

void P2(List<Reindeer> reindeers)
{
    List<Reindeer> TravellingReindeer = new List<Reindeer>(reindeers);
    List<Reindeer> RestingReindeer = new List<Reindeer>();

    const int finishTime = 2503;

    for (int timePassed = 0; timePassed < finishTime;)
    {
        Int64 winningDistance = Int64.MinValue;
        List<Reindeer> winningReindeer = new List<Reindeer>();
        foreach (Reindeer rd in reindeers)
        {
            if (!rd.Resting)
                rd.Distance += rd.Speed;

            if (rd.Distance > winningDistance)
            {
                winningDistance = rd.Distance;
                winningReindeer = new List<Reindeer>() { rd };
            }
            else if (rd.Distance == winningDistance)
            {
                winningReindeer.Add(rd);
            }

            rd.DurationUntilStateChange -= 1;
            if (rd.DurationUntilStateChange == 0)
            {
                if (rd.Resting)
                    rd.DurationUntilStateChange = rd.TravelDuration;
                else
                    rd.DurationUntilStateChange = rd.RestDuration;
                rd.Resting = !rd.Resting;
            }
        }

        foreach (Reindeer rd in winningReindeer)
            rd.Score++;

        timePassed += 1;
    }

    int highestScore = int.MinValue;
    foreach (Reindeer rd in reindeers)
        highestScore = Math.Max(rd.Score, highestScore);

    Console.WriteLine(highestScore);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

List<Reindeer> reindeers = inputList.Select(x => new Reindeer(x)).ToList();

P1(reindeers);

foreach (Reindeer rd in reindeers)
{
    rd.Distance = 0;
    rd.DurationUntilStateChange = rd.TravelDuration;
    rd.Resting = false;
}

P2(reindeers);

public class Reindeer
{
    public string Name;
    public int Speed;
    public int TravelDuration;
    public int RestDuration;

    public int DurationUntilStateChange;
    public bool Resting = false;
    public Int64 Distance = 0;
    public int Score = 0;

    public Reindeer(string desc)
    {
        string[] split = desc.Split(' ');
        Name = split[0];
        Speed = int.Parse(split[3]);
        TravelDuration = int.Parse(split[6]);
        RestDuration = int.Parse(split[13]);

        DurationUntilStateChange = TravelDuration;
    }

    public override string ToString()
    {
        return $"{Name} | {Speed} km/s for {TravelDuration} s | Rest {RestDuration} s";
    }
}