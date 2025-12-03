namespace AoC25.Day03
{
    internal class Solver
    {
        public static string Solve(List<string> input, int part)
            => part switch
            {
                1 => Solution(input, 1),
                2 => Solution(input, 2),
                _ => "Wrong part number - only 1 or 2 allowed"
            };

        static public IEnumerable<List<int>> GetBank(List<string> input)
        {
            foreach (var line in input)
            {
                var bank = line.Select(c => int.Parse(c.ToString())).ToList();
                yield return bank;
            }
        }

        private static string Solution(List<string> input, int part)
        {
            int result = 0;
            foreach (var bank in GetBank(input))
            {
                var first = bank.Take(bank.Count - 1).Max();
                var second = bank.Skip(bank.IndexOf(first) + 1).Max();
                result += first*10 + second;
            }
            return result.ToString();
        }
    }
}
