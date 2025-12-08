using AoC25.Common;

namespace AoC25.Day08
{
    internal class Solver
    {
        static List<Coord3DL> BoxPositions = new();
        static Dictionary<(int, int), double> Distances = new();

        public static string Solve(List<string> lines, int part = 1)
        {
            ParseInput(lines);
            return part == 1 ? SolvePart1(lines) : SolvePart2(lines);
        }
           
        private static void ParseInput(List<string> lines)
        {
            lines.ForEach(x =>
            {
                var parts = x.Split(',').Select(int.Parse).ToArray();
                BoxPositions.Add(new Coord3DL(parts[0], parts[1], parts[2]));
            });

            for (int i = 0; i < BoxPositions.Count-1; i++)
                for (int j = i + 1; j < BoxPositions.Count; j++)
                {
                    Distances[(i,j)] = BoxPositions[i].DistanceTo(BoxPositions[j]);
                    Distances[(j,i)] = Distances[(i, j)];
                }
        }
       
        private static string SolvePart1(List<string> lines)
        {
            HashSet<HashSet<int>> Circuits = new();

            for (int i = 0; i < 1000; i++)
            {
                var minDist = Distances.Values.Min();
                var closestPair = Distances.Keys.Where(k => Distances[k] == minDist).First();
                
                var circuitContainsFirst = Circuits.Where(c => c.Contains(closestPair.Item1)).FirstOrDefault();
                var circuitContainsSecond = Circuits.Where(c => c.Contains(closestPair.Item2)).FirstOrDefault();

                if(circuitContainsFirst is null && circuitContainsSecond is null)
                    Circuits.Add(new HashSet<int> { closestPair.Item1, closestPair.Item2 });
                else if (circuitContainsFirst is not null && circuitContainsSecond is null)
                    circuitContainsFirst.Add(closestPair.Item2);
                else if (circuitContainsFirst is null && circuitContainsSecond is not null)
                    circuitContainsSecond.Add(closestPair.Item1);
                else if (circuitContainsFirst != circuitContainsSecond)
                { 
                    var both = circuitContainsFirst.Union(circuitContainsSecond);
                    Circuits.Remove(circuitContainsSecond);
                    Circuits.Remove(circuitContainsFirst);
                    Circuits.Add(new HashSet<int>(both));
                }

                Distances.Remove(closestPair);
                Distances.Remove((closestPair.Item2, closestPair.Item1));
            }

            return Circuits.Select(c => c.Count)
                           .OrderByDescending(x => x).Take(3)
                           .Aggregate( 1, (acc, val) => acc * val)
                           .ToString();
        }

        private static string SolvePart2(List<string> lines)
           => "Not implemented";
    }
}
