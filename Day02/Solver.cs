namespace AoC25.Day02
{
    internal class Solver
    {
        public static string Solve(List<string> input, int part)
            => part switch
            {
                1 => SolvePart1(input),
                2 => SolvePart2(input),
                _ => "Wrong part number - only 1 or 2 allowed"
            };

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

        private static string SolvePart1(List<string> input)
        { 
            long sum = 0;
            foreach (var (first, second) in ParsePairs(input))
                for (long i = first; i <= second; i++)
                    if(HasRepeats(i))
                        sum += i;
            return sum.ToString();
        }

        private static string SolvePart2(List<string> input)
            => "Not implemented";
    }
}
