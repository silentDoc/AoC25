using AoC25.Common;

// For Part 2 - Implement AABB collision detection to find overlapping rectangles.
//              Merit goes to encse's approach, I was totally lost for this one.

namespace AoC25.Day09
{
    record Rectangle
    {
        public long top;
        public long left;
        public long bottom;
        public long right;

        public Rectangle(Coord2DL p1, Coord2DL p2)
        {
            top = Math.Min(p1.y, p2.y);
            left = Math.Min(p1.x, p2.x);
            bottom = Math.Max(p1.y, p2.y);
            right = Math.Max(p1.x, p2.x);
        }

        public Rectangle(long top, long left, long bottom, long right)
        {
            this.top = top;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
        }
        public long Area
            => (Math.Abs(right - left) + 1) * (Math.Abs(top - bottom) + 1);
    }

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

        static string SolvePart1(List<string> lines)
        {
            HashSet<long> areas = new(); ;
            ParseInput(lines);
            for (int i = 0; i < Points.Count - 1; i++)
                for (int j = i + 1; j < Points.Count; j++)
                    areas.Add( new Rectangle(Points[i], Points[j]).Area );

            return areas.Max().ToString();
        }

        static string SolvePart2(List<string> lines)
        {
            ParseInput(lines);
            var limits = Limits(Points.ToArray());
            return GetAllRectanglesByArea().Where(x => limits.All(l => !AabbCollision(x, l)))
                                           .Select(x => x.Area).First().ToString();
        }

        static IEnumerable<Rectangle> GetAllRectanglesByArea()
            => Points.SelectMany(it1 => Points.Select(it2 => new Rectangle(it1,it2)))
                     .OrderByDescending(x => x.Area);

        // also from https://aoc.csokavar.hu/2025/9/;
        // see https://kishimotostudios.com/articles/aabb_collision/
        static bool AabbCollision(Rectangle a, Rectangle b)
        {
            var aIsToTheLeft = a.right <= b.left;
            var aIsToTheRight = a.left >= b.right;
            var aIsAbove = a.bottom <= b.top;
            var aIsBelow = a.top >= b.bottom;
            return !(aIsToTheRight || aIsToTheLeft || aIsAbove || aIsBelow);
        }

        static IEnumerable<Rectangle> Limits(Coord2DL[] corners)
            => corners.Zip(corners.Prepend(corners.Last()))
                      .Select(pair => new Rectangle(pair.First, pair.Second));
    }
}
