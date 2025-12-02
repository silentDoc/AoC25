using AoC25.Common;

namespace AoC25.Day01
{
    internal class Solver
    {
        public static string Solve(List<string> input, int part)
            => part switch
            {
                1 => Solution(input,1),
                2 => Solution(input,2),
                _ => "Wrong part number - only 1 or 2 allowed"
            };

        private static string Solution(List<string> input, int part)
        {
            int pos = 50;
            int times_it_points_to_Zero = 0;

            foreach (var rotation in input)
            {
                var (rotationDir, rotationAmount) = (rotation[0], int.Parse(rotation[1..]));
                var distance_to_Zero = pos == 0 ? 100
                                                : rotationDir == 'L' ? pos : (100 - pos);

                pos = pos + (rotationDir == 'L' ? (-1 * rotationAmount) : rotationAmount);
                pos = MathHelper.Modulo(pos, 100);

                if (part == 2)
                {
                    int fullTurns = rotationAmount / 100;
                    int remainingRotation = rotationAmount % 100;
                    times_it_points_to_Zero += fullTurns;
                    times_it_points_to_Zero += (remainingRotation >= distance_to_Zero) ? 1 : 0;
                }
                else
                    times_it_points_to_Zero += (pos == 0) ? 1 : 0;
            }
            return times_it_points_to_Zero.ToString();
        }
    }
}
