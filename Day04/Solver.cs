using AoC25.Common;

namespace AoC25.Day04
{
    internal class Solver
    {
        static Dictionary<Coord2D, char> grid = new();

        public static string Solve(List<string> input, int part)
        { 
            ParseGrid(input);
            return part == 1 ? SolvePart1(input) : SolvePart2(input);
        }

        private static void ParseGrid(List<string> input)
        {
            Dictionary<Coord2D, char> retVal = new();
            foreach (var (row, line) in input.Index())
                foreach(var (col, ch) in line.Index())
                    grid[new Coord2D(col, row)] = ch;
        }

        private static string SolvePart1(List<string> input)
        {
            var sum = 0;
            foreach(var key in grid.Keys.Where(x => grid[x]=='@'))
                sum += key.GetNeighbors8().Where(n => grid.ContainsKey(n)).Count(x => grid[x] == '@') < 4 ? 1 : 0;
            return sum.ToString();
        }

        private static string SolvePart2(List<string> input)
        {
            List<Coord2D> toRemove = new();
            bool someRemoved = true;
            int removed = 0;

            while (someRemoved)
            {
                foreach (var key in grid.Keys.Where(x => grid[x] == '@'))
                    if (key.GetNeighbors8().Where(n => grid.ContainsKey(n)).Count(x => grid[x] == '@') < 4)
                        toRemove.Add(key);
                
                removed += toRemove.Count;
                someRemoved = toRemove.Count > 0;
                toRemove.ForEach(x => grid[x] = '.');
                toRemove.Clear();
            }
            return removed.ToString();
        }
    }
}
