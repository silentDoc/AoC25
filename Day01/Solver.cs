namespace AoC25.Day01
{
    public class Solver
    {
        public static string Solve(List<string> input, int part)
        {
            return part switch
            {
                1 => SolvePart1(input),
                2 => SolvePart2(input),
                _ => throw new ArgumentException("Wrong part number - only 1 or 2 allowed")
            };
        }
        private static string SolvePart1(List<string> input)
        {
            return "";
        }
        private static string SolvePart2(List<string> input)
        {
            return "";
        }
    }
}
