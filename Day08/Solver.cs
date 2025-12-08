using AoC25.Common;

namespace AoC25.Day08
{
    internal class Solver
    {
        static List<Coord3DL> BoxPositions = new();
        static Dictionary<(int, int), double> Distances = new();
        static Dictionary<int, int> BoxInCircuit = new();

        public static string Solve(List<string> lines, int part = 1)
        {
            ParseInput(lines);
            return part == 1 ? SolvePart1Alt() : SolvePart2(lines);
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

            for (int i = 0; i < BoxPositions.Count; i++)
                BoxInCircuit[i] = i;
        }

        private static string SolvePart1Alt()
        {
            for (int i = 0; i < 1000; i++)
            {
                var minDist = Distances.Values.Min();
                var closestPair = Distances.Keys.Where(k => Distances[k] == minDist).First();

                // We find 2 boxes in different circuits - we need to merge circuits
                if (BoxInCircuit[closestPair.Item1] != BoxInCircuit[closestPair.Item2])
                {
                    
                    var secondCircuitId = BoxInCircuit[closestPair.Item2];
                    var boxesOfSecond = BoxInCircuit.Keys.Where(x => BoxInCircuit[x] == secondCircuitId);
                    foreach(var ind in boxesOfSecond)
                        BoxInCircuit[ind] = BoxInCircuit[closestPair.Item1];
                }
                Distances.Remove(closestPair);
                Distances.Remove((closestPair.Item2, closestPair.Item1));
            }

            return BoxInCircuit.GroupBy(x => x.Value)
                               .Select(g => g.Count())
                               .OrderByDescending(x => x).Take(3)
                               .Aggregate(1, (acc, val) => acc * val)
                               .ToString(); 

        }

        private static string SolvePart2(List<string> lines)
           => "Not implemented";
    }
}
