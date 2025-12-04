using AoC25.Common;

namespace AoC25.Day04
{
    internal class Solver
    {
        static Dictionary<Coord2D, char> grid = new();

        public static string Solve(List<string> input, int part)
        { 
            ParseGrid(input);
            return part == 1 ? GetRemovable().Count.ToString() : SolvePart2(input);
        }

        private static void ParseGrid(List<string> input)
        {
            Dictionary<Coord2D, char> retVal = new();
            foreach (var (row, line) in input.Index())
                foreach(var (col, ch) in line.Index())
                    grid[new Coord2D(col, row)] = ch;
        }

        private static List<Coord2D> GetRemovable()
            => grid.Keys.Where(x => grid[x] == '@')
                        .Where(key => key.GetNeighbors8().Where(n => grid.ContainsKey(n)).Count(x => grid[x] == '@') < 4)
                        .ToList();

        private static string SolvePart2(List<string> input)
        {
            List<Coord2D> toRemove = new();
            int removed = 0;
            while ((toRemove = GetRemovable()).Any())
            {
                removed += toRemove.Count;
                toRemove.ForEach(x => grid[x] = '.');
                toRemove.Clear();
            }
            return removed.ToString();
        }
    }
}
