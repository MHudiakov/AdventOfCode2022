using System.Text.RegularExpressions;
using ConsoleApp1;

const int workTime = 32;
var blueprints = ReadInput().Take(3);

int qualityLevelComposition = 1;

foreach (var blueprint in blueprints)
{
    var maxGeodeNumber = DetermineMaxGeodeNumber(blueprint);
    qualityLevelComposition *= maxGeodeNumber;
}

Console.WriteLine(qualityLevelComposition);

int DetermineMaxGeodeNumber(Blueprint blueprint)
{
    var initState = new State
    {
        OreRobotAmount = 1
    };

    var states = new List<State> { initState };
    for (int minute = 0; minute < workTime; minute++)
    {
        states = CalcStates(states, blueprint, minute);
    }

    return states.Max(s => s.GeodeAmount);
}

List<State> CalcStates(List<State> states, Blueprint blueprint)
{
    var newStates = new List<State>();

    foreach (var state in states)
    {
        var candidateStates = new List<State>();

        if (state.ObsidianAmount >= blueprint.GeodeRobotObsidianPrice &&
            state.OreAmount >= blueprint.GeodeRobotOrePrice)
        {
            candidateStates.Add(new State
            {
                OreRobotAmount = state.OreRobotAmount,
                ObsidianRobotAmount = state.ObsidianRobotAmount,
                ClayRobotAmount = state.ClayRobotAmount,
                GeodeRobotAmount = state.GeodeRobotAmount + 1,
                ObsidianAmount = state.ObsidianAmount + state.ObsidianRobotAmount - blueprint.GeodeRobotObsidianPrice,
                GeodeAmount = state.GeodeAmount + state.GeodeRobotAmount,
                ClayAmount = state.ClayAmount + state.ClayRobotAmount,
                OreAmount = state.OreAmount + state.OreRobotAmount - blueprint.GeodeRobotOrePrice
            });
        }
        else
        {
            if (state.ObsidianRobotAmount < blueprint.GeodeRobotObsidianPrice &&
                state.ClayAmount >= blueprint.ObsidianRobotClayPrice &&
                state.OreAmount >= blueprint.ObsidianRobotOrePrice)
            {
                candidateStates.Add(new State
                {
                    OreRobotAmount = state.OreRobotAmount,
                    ObsidianRobotAmount = state.ObsidianRobotAmount + 1,
                    ClayRobotAmount = state.ClayRobotAmount,
                    GeodeRobotAmount = state.GeodeRobotAmount,
                    ObsidianAmount = state.ObsidianAmount + state.ObsidianRobotAmount,
                    GeodeAmount = state.GeodeAmount + state.GeodeRobotAmount,
                    ClayAmount = state.ClayAmount + state.ClayRobotAmount - blueprint.ObsidianRobotClayPrice,
                    OreAmount = state.OreAmount + state.OreRobotAmount - blueprint.ObsidianRobotOrePrice
                });
            }

            if (state.ClayRobotAmount < blueprint.ObsidianRobotClayPrice &&
                state.OreAmount >= blueprint.ClayRobotOrePrice)
            {
                candidateStates.Add(new State
                {
                    OreRobotAmount = state.OreRobotAmount,
                    ObsidianRobotAmount = state.ObsidianRobotAmount,
                    ClayRobotAmount = state.ClayRobotAmount + 1,
                    GeodeRobotAmount = state.GeodeRobotAmount,
                    ObsidianAmount = state.ObsidianAmount + state.ObsidianRobotAmount,
                    GeodeAmount = state.GeodeAmount + state.GeodeRobotAmount,
                    ClayAmount = state.ClayAmount + state.ClayRobotAmount,
                    OreAmount = state.OreAmount + state.OreRobotAmount - blueprint.ClayRobotOrePrice
                });
            }

            var maxOreRobotAmount = new[]
            {
                blueprint.OreRobotOrePrice, blueprint.ClayRobotOrePrice, blueprint.GeodeRobotOrePrice,
                blueprint.ObsidianRobotOrePrice
            }.Max();

            if (state.OreRobotAmount < maxOreRobotAmount &&
                state.OreAmount >= blueprint.OreRobotOrePrice)
            {
                candidateStates.Add(new State
                {
                    OreRobotAmount = state.OreRobotAmount + 1,
                    ObsidianRobotAmount = state.ObsidianRobotAmount,
                    ClayRobotAmount = state.ClayRobotAmount,
                    GeodeRobotAmount = state.GeodeRobotAmount,
                    ObsidianAmount = state.ObsidianAmount + state.ObsidianRobotAmount,
                    GeodeAmount = state.GeodeAmount + state.GeodeRobotAmount,
                    ClayAmount = state.ClayAmount + state.ClayRobotAmount,
                    OreAmount = state.OreAmount + state.OreRobotAmount - blueprint.OreRobotOrePrice
                });
            }

            if (state.OreAmount <= blueprint.OreRobotOrePrice || state.OreAmount <= blueprint.ClayRobotOrePrice ||
                state.OreAmount <= blueprint.ObsidianRobotOrePrice || state.OreAmount <= blueprint.GeodeRobotOrePrice ||
                state.ClayAmount <= blueprint.ObsidianRobotClayPrice ||
                state.ObsidianAmount <= blueprint.GeodeRobotObsidianPrice)
            {
                candidateStates.Add(new State
                {
                    OreRobotAmount = state.OreRobotAmount,
                    ObsidianRobotAmount = state.ObsidianRobotAmount,
                    ClayRobotAmount = state.ClayRobotAmount,
                    GeodeRobotAmount = state.GeodeRobotAmount,
                    ObsidianAmount = state.ObsidianAmount + state.ObsidianRobotAmount,
                    GeodeAmount = state.GeodeAmount + state.GeodeRobotAmount,
                    ClayAmount = state.ClayAmount + state.ClayRobotAmount,
                    OreAmount = state.OreAmount + state.OreRobotAmount
                });
            }
        }
        
        foreach (var candidate in candidateStates)
        {
            bool addState = true;

            foreach (var s in newStates)
            {
                if (candidate <= s)
                {
                    addState = false;
                    break;
                }
            }

            if (!addState)
            {
                continue;
            }

            newStates.RemoveAll(s => s <= candidate);
            newStates.Add(candidate);
        }
    }

    return newStates;
}

List<Blueprint> ReadInput()
{
    return File.ReadAllLines(@"c:\aoc/1.txt").Select(line =>
    {
        var values = Regex.Matches(line, @"-?\d+").Select(m => int.Parse(m.Value)).ToList();
        return new Blueprint
        {
            Id = values[0],
            OreRobotOrePrice = values[1],
            ClayRobotOrePrice = values[2],
            ObsidianRobotOrePrice = values[3],
            ObsidianRobotClayPrice = values[4],
            GeodeRobotOrePrice = values[5],
            GeodeRobotObsidianPrice = values[6]
        };
    }).ToList();
}