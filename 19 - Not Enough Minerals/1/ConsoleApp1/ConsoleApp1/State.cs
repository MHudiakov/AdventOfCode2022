namespace ConsoleApp1;

public class State
{
    public int OreAmount { get; set; }

    public int ClayAmount { get; set; }

    public int ObsidianAmount { get; set; }

    public int GeodeAmount { get; set; }

    public int OreRobotAmount { get; set; }

    public int ClayRobotAmount { get; set; }

    public int ObsidianRobotAmount { get; set; }

    public int GeodeRobotAmount { get; set; }

    public static bool operator <=(State left, State right)
    {
        return left.OreAmount <= right.OreAmount &&
               left.ClayAmount <= right.ClayAmount &&
               left.ObsidianAmount <= right.ObsidianAmount &&
               left.GeodeAmount <= right.GeodeAmount &&
               left.OreRobotAmount <= right.OreRobotAmount &&
               left.ClayRobotAmount <= right.ClayRobotAmount &&
               left.ObsidianRobotAmount <= right.ObsidianRobotAmount &&
               left.GeodeRobotAmount <= right.GeodeRobotAmount;
    }

    public static bool operator >=(State l, State f)
    {
        throw new NotImplementedException();
    }
}