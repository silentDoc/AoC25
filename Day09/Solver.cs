using AoC25.Common;

namespace AoC25.Day09
{
    internal class Solver
    {
        static List<Coord2DL> Points = new();

        public static string Solve(List<string> lines, int part = 1)
            => part == 1 ? SolvePart1(lines) : SolvePart2(lines);

        private static void ParseInput(List<string> lines)
        {
            lines.ForEach(x =>
            {
                var parts = x.Split(',').Select(int.Parse).ToArray();
                Points.Add(new Coord2DL(parts[0], parts[1]));
            });

        }

        private static string SolvePart1(List<string> lines)
        {
            HashSet<long> areas = new(); ;
            ParseInput(lines);
            for (int i= 0; i < Points.Count-1; i++)
                for (int j = i+1; j < Points.Count; j++)
                {
                    Coord2DL dif = new Coord2DL(Math.Abs(Points[j].x - Points[i].x) + 1, Math.Abs(Points[j].y - Points[i].y) + 1);
                    areas.Add(dif.x * dif.y);
                }
            return areas.Max().ToString();
        }

        private static string SolvePart2(List<string> lines)
            => "Part 2 solution not implemented.";
    }
}
