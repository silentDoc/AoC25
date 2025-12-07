using System.Linq;

namespace AoC25.Day06
{
    internal class Solver
    {
        static List<List<long>> rows = new();
        static List<List<long>> nums = new();
        static List<char> ops = new();

        public static string Solve(List<string> lines, int part = 1)
        {
            if (part == 1)
                ParseInput(lines);
            else
                ParseInputRightMost(lines);

            return Calculate(part);
        }

        static void ParseInput(List<string> lines)
        {
            foreach (var line in lines[..^1])
                rows.Add(line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList());
            ops = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => x[0]).ToList();
        }

        static void ParseInputRightMost(List<string> lines)
        {
            List<long> group = new();
            var linesNums = lines[..^1];
            
            for (int i = linesNums[0].Length - 1; i >= 0; i--)
            {
                string num = string.Join("", linesNums.Select(x => x[i]).ToArray());
                num = num.Replace(" ", "");

                if(num.Length>0)
                    group.Add(long.Parse(num));

                if ((num.Length == 0) || (i == 0))
                {
                    nums.Add(group);
                    group = new();
                }
            }
            ops = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => x[0]).ToList();
        }

        static string Calculate(int part)
        {
            var els = Enumerable.Range(0, ops.Count).ToList();
            List<long> results = part == 1 ? els.Select(col => ops[col] == '+' ? rows.Sum(row => row[col])
                                                                               : rows.Aggregate(1, (long acc, List<long> row) => acc * row[col]))
                                                 .ToList()
                                           : els.Select(col => ops[col] == '+' ? nums[ops.Count - col-1].Sum()
                                                                               : nums[ops.Count - col - 1].Aggregate(1, (long acc, long val) => acc * val))
                                                 .ToList();

            return results.Sum().ToString();
        }
    }
}
