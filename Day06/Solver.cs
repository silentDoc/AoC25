namespace AoC25.Day06
{
    internal class Solver
    {
        static List<List<long>> rows = new();
        static List<char> ops = new();


        public static string Solve(List<string> lines, int part = 1)
        {
            ParseInput(lines);
            return part == 1 ? SolvePart1() : SolvePart2();
        }

        static void ParseInput(List<string> lines)
        {
            foreach (var line in lines[..^1])
                rows.Add(line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList());
            ops = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => x[0]).ToList();
        }

        static string SolvePart1()
        {
            var els = Enumerable.Range(0, ops.Count).ToList();
            List<long> results = els.Select(col => ops[col] == '+' ? rows.Sum(row => row[col])
                                                                   : rows.Aggregate(1, (long acc, List<long> row) => acc * row[col]))
                                    .ToList();
            return results.Sum().ToString();
        }

        static string SolvePart2()
            => "Part 2 solution";
    }
}
