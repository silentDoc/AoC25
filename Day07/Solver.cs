namespace AoC25.Day07
{
    internal class Solver
    {
        public static string Solve(List<string> lines, int part = 1)
            => part == 1 ? SolvePart1(lines) : SolvePart2(lines);

        private static string SolvePart1(List<string> lines)
        {
            HashSet<int> tachyonCols = new();
            tachyonCols.Add(lines[0].IndexOf('S'));
            int splits = 0;

            for (int row = 1; row < lines.Count; row++)
            {
                var splitBeams = tachyonCols.Where(c => lines[row][c] == '^').ToHashSet();
                tachyonCols.RemoveWhere(x => splitBeams.Contains(x));
                splits += splitBeams.Count;
                var splitLeft = splitBeams.Select(c => c - 1).ToHashSet();
                var splitRight = splitBeams.Select(c => c + 1).ToHashSet();
                tachyonCols.UnionWith(splitLeft);
                tachyonCols.UnionWith(splitRight);
            }
            return splits.ToString();
        }

        private static string SolvePart2(List<string> lines)
            => "Not implemented";
    }
}
