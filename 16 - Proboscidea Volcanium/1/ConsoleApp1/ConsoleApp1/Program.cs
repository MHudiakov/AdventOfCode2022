using ConsoleApp1;
using static System.Int32;

var valves = new List<Valve>();

foreach (var line in File.ReadAllLines(@"c:\aoc/1.txt"))
{
    var values = line.Split(' ', '=', ';', ',').Where(i => !string.IsNullOrEmpty(i)).ToArray();
    
    valves.Add(new Valve
    {
        Name = values[1],
        FlowRate = Parse(values[5]),
        Neighbours = values[10..].ToList()
    });
}

var distances = new Dictionary<(string v1, string v2), int>();
foreach (var valve in valves)
{
    CalcDistance(valve);
    foreach (var v in valves.Where(v => v.FlowRate > 0))
    {
        distances[(valve.Name, v.Name)] = v.Distance;
    }
}

const int totalMinutes = 30;
var valveNames = valves.Where(v => v.FlowRate > 0).Select(v => v.Name).ToList();
var maxPressure = 0;

var node = new Node
{
    Name = "AA",
    RemainingTime = totalMinutes,
    TotalPressure = 0,
    ValvesToVisit = valveNames
};

ProcessNode(node);

Console.WriteLine(maxPressure);
Console.ReadLine();


void ProcessNode(Node node)
{
    if (maxPressure < node.TotalPressure)
    {
        maxPressure = node.TotalPressure;
    }

    foreach (var valveName in node.ValvesToVisit)
    {
        var remainingTime = node.RemainingTime - distances[(node.Name, valveName)] - 1;
        var totalPressure = node.TotalPressure + valves.FirstOrDefault(v => v.Name == valveName)!.FlowRate * remainingTime;

        if (remainingTime < 0)
        {
            continue;
        }

        ProcessNode(new Node
        {
            Name = valveName,
            RemainingTime = remainingTime,
            TotalPressure = totalPressure,
            ValvesToVisit = node.ValvesToVisit.Except(new List<string> { valveName }).ToList()
        });
    }
}

void CalcDistance(Valve v)
{
    foreach (var valve in valves)
    {
        valve.Visited = false;
        valve.Distance = MaxValue;
    }

    v.Distance = 0;

    while (valves.Any(v => !v.Visited))
    {
        var valve = valves.Where(v => !v.Visited).OrderBy(v => v.Distance).First();
        
        valve.Visited = true;
        foreach (var neighbor in valve.Neighbours)
        {
            var neighborValve = valves.First(v => v.Name.Equals(neighbor));
            neighborValve.Distance = Math.Min(valve.Distance + 1, neighborValve.Distance);
        }
    }
}