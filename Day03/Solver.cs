namespace AoC25.Day03
{
    internal class Solver
    {
        public static string Solve(List<string> input, int part)
            => ParseBank(input).Sum(x => MaxJoltage(x, (part == 1) ? 2 : 12)).ToString();

        static public IEnumerable<List<int>> ParseBank(List<string> input)
        {
            foreach (var line in input)
            {
                var bank = line.Select(c => int.Parse(c.ToString())).ToList();
                yield return bank;
            }
        }

        private static (int selected, List<int> remainingList) SelectMax(List<int> bank, int numDigit)
        {
            var selected = bank.Take(bank.Count - numDigit).Max();
            var remainingList = bank.Skip(bank.IndexOf(selected) + 1).ToList();
            return (selected, remainingList);
        }

        private static long MaxJoltage(List<int> bank, int numDigits)
        {
            long result = 0;
            List<int> remainingBank = bank;
            for (int i= numDigits-1; i >= 0; i--)
            {
                var (digit, rest) = SelectMax(remainingBank, i);
                remainingBank = rest;
                result = result * 10 + digit;
            }
            return result;
        }
    }
}
