namespace AoC25.Day07
{
    internal class Solver
    {
        public static string Solve(List<string> lines, int part = 1)
            => RunGrid(lines, part);

        private static string RunGrid(List<string> lines, int part)
        {
            int splits = 0; 

            HashSet<int> tachyonCols = new();
            tachyonCols.Add(lines[0].IndexOf('S'));
            long[] timelines = new long[lines[0].Length];
            timelines[lines[0].IndexOf('S')] = 1;

            for (int row = 1; row < lines.Count; row++)
            {
                long[] next = new long[lines[0].Length];

                var splitBeams = tachyonCols.Where(c => lines[row][c] == '^').ToHashSet();
                tachyonCols.RemoveWhere(x => splitBeams.Contains(x));
                splits += splitBeams.Count;
                var splitLeft = splitBeams.Select(c => c - 1).ToHashSet();
                var splitRight = splitBeams.Select(c => c + 1).ToHashSet();
                tachyonCols.UnionWith(splitLeft);
                tachyonCols.UnionWith(splitRight);

                for(var col =0; col < lines[0].Length; col++)
                {
                    if (lines[row][col] == '.')
                        next[col] += timelines[col];
                    else
                    {
                        next[col - 1] += timelines[col];
                        next[col + 1] += timelines[col];
                    }
                }
                timelines = next;
            }
            return part == 1 ? splits.ToString() : timelines.Sum().ToString(); 
        }
    }
}
