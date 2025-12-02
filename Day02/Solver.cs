using AoC25.Common;

namespace AoC25.Day02
{
    internal class Solver
    {
        public static string Solve(List<string> input, int part)
        {
            long sum = 0;
            foreach (var (first, second) in ParsePairs(input))
                for (long i = first; i <= second; i++)
                    if (part == 1 ? HasRepeats(i) : HasRepeatsPart2(i))
                        sum += i;
            return sum.ToString();
        }

        private static IEnumerable<(long, long)> ParsePairs(List<string> input)
        {
            var pairs = input[0].Split(',');
            foreach (var pair in pairs)
            {
                var parts = pair.Split('-');
                yield return (long.Parse(parts[0]), long.Parse(parts[1]));
            }
        }

        private static bool HasRepeats(long number)
        {
            string str = number.ToString();
            return str.Length % 2 == 0 && str[..(str.Length / 2)] == str[(str.Length / 2)..];
        }

        private static bool HasRepeatsPart2(long number)
        {
            string str = number.ToString();
            for (int i = 1; i <= str.Length / 2; i++)
            {
                if(str.Length % i != 0)
                    continue;
                var parts = str.Windowed(i,i).Select(x => new string(x)).ToList();
                if(parts.Any(x => x.Length!=parts.First().Length))
                    continue;
                if (parts.Distinct().Count()==1)
                    return true;

            }
            return false;
        }
    }
}
