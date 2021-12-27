using AdventOfCodeUtilities;

void P1(Dictionary<string, Location> locations)
{
    Int64 minDistance = Int64.MaxValue;
    foreach (var kVP in locations)
    {
        minDistance = Math.Min(minDistance, kVP.Value.ShortestDistanceToVisitEverywhere(locations, new HashSet<string>()));
    }
    Console.WriteLine(minDistance);
    Console.ReadLine();
}

void P2(Dictionary<string, Location> locations)
{
    Int64 maxDistance = Int64.MinValue;
    foreach (var kVP in locations)
    {
        maxDistance = Math.Max(maxDistance, kVP.Value.LongestDistanceToVisitEverywhere(locations, new HashSet<string>()));
    }
    Console.WriteLine(maxDistance);
    Console.ReadLine();
}

List<string> inputList = AoCUtilities.GetInputLines();

Dictionary<string, Location> locations = new Dictionary<string, Location>();
foreach (string path in inputList)
{
    string[] split = path.Split(' ');
    string a = split[0];
    string b = split[2];
    int distance = int.Parse(split[4]);

    Location aLocation;
    if (locations.ContainsKey(a))
        aLocation = locations[a];
    else
    {
        aLocation = new Location(a);
        locations[a] = aLocation;
    }

    Location bLocation;
    if (locations.ContainsKey(b))
        bLocation = locations[b];
    else
    {
        bLocation = new Location(b);
        locations[b] = bLocation;
    }

    aLocation.Distances[b] = distance;
    bLocation.Distances[a] = distance;
}

P1(locations);
P2(locations);

public class Location
{
    public string Name;
    public Dictionary<string, int> Distances = new Dictionary<string, int>();

    public Location(string name)
    {
        Name = name;
    }

    public Int64 ShortestDistanceToVisitEverywhere(Dictionary<string,Location> locations, HashSet<string> visitedLocations)
    {
        visitedLocations.Add(Name);
        if (visitedLocations.Count == locations.Count)
            return 0;
        Int64 minDistance = Int64.MaxValue;
        foreach (var kVP in Distances)
        {
            string dest = kVP.Key;
            if (!visitedLocations.Contains(dest))
            {
                HashSet<string> visitedLocationsClone = new HashSet<string>(visitedLocations);
                Int64 incDistance = locations[dest].ShortestDistanceToVisitEverywhere(locations, visitedLocationsClone);
                if (incDistance != Int64.MaxValue)
                    minDistance = Math.Min(minDistance, Distances[dest] + incDistance);
            }
        }
        return minDistance;
    }

    public Int64 LongestDistanceToVisitEverywhere(Dictionary<string, Location> locations, HashSet<string> visitedLocations)
    {
        visitedLocations.Add(Name);
        if (visitedLocations.Count == locations.Count)
            return 0;
        Int64 maxDistance = Int64.MinValue;
        foreach (var kVP in Distances)
        {
            string dest = kVP.Key;
            if (!visitedLocations.Contains(dest))
            {
                HashSet<string> visitedLocationsClone = new HashSet<string>(visitedLocations);
                Int64 incDistance = locations[dest].LongestDistanceToVisitEverywhere(locations, visitedLocationsClone);
                if (incDistance != Int64.MinValue)
                    maxDistance = Math.Max(maxDistance, Distances[dest] + incDistance);
            }
        }
        return maxDistance;
    }
}