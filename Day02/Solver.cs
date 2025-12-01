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
        private static string SolvePart1(List<string> input)
            => "Not implemented";

        private static string SolvePart2(List<string> input)
            => "Not implemented";
    }
}
