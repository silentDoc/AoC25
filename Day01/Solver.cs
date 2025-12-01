using AoC25.Common;

namespace AoC25.Day01
{
    public class Solver
    {
        public static string Solve(List<string> input, int part)
            => part switch
            {
                1 => SolvePart1(input),
                2 => SolvePart2(input),
                _ => "Wrong part number - only 1 or 2 allowed"
            };

        private static string SolvePart1(List<string> input)
        {
            int pos = 50;
            int times_it_points_to_Zero = 0;

            foreach (var rotation in input)
            {
                var rotationDir = rotation[0];
                var rotationAmount = int.Parse(rotation[1..]);

                pos = pos + (rotationDir == 'L' ? (-1 * rotationAmount) : rotationAmount);
                pos = MathHelper.Modulo(pos, 100);

                if (pos == 0)
                    times_it_points_to_Zero++;
            }
            return times_it_points_to_Zero.ToString();
        }

        private static string SolvePart2(List<string> input)
        {
            int pos = 50;
            int times_it_points_to_Zero = 0;

            foreach (var rotation in input)
            {
                var rotationDir = rotation[0];
                var rotationAmount = int.Parse(rotation[1..]);

                var distance_to_Zero = pos == 0 ? 100
                                                : rotationDir == 'L' ? pos : (100 - pos);
                
                int fullTurns = rotationAmount / 100;
                int remainingRotation = rotationAmount % 100;

                times_it_points_to_Zero += fullTurns;
                times_it_points_to_Zero += (remainingRotation >= distance_to_Zero) ? 1 : 0;

                pos = pos + (rotationDir == 'L' ? (-1 * rotationAmount) : rotationAmount);
                pos = MathHelper.Modulo(pos, 100);
            }
            return times_it_points_to_Zero.ToString();
        }
    }
}
