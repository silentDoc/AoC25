using AoC25.Common;

namespace AoC25.Day08
{
    record PairInfo
    {
        public int Item1;
        public int Item2;
        public double Distance;
    }

    internal class Solver
    {
        static List<Coord3DL> BoxPositions = new();
        static List<PairInfo> Pairs = new();
        static Dictionary<int, int> BoxInCircuit = new();

        public static string Solve(List<string> lines, int part = 1)
        {
            ParseInput(lines);
            return part == 1 ? SolvePart1() : SolvePart2();
        }
           
        private static void ParseInput(List<string> lines)
        {
            lines.ForEach(x =>
            {
                var parts = x.Split(',').Select(int.Parse).ToArray();
                BoxPositions.Add(new Coord3DL(parts[0], parts[1], parts[2]));
            });

            for (int i = 0; i < BoxPositions.Count - 1; i++)
                for (int j = i + 1; j < BoxPositions.Count; j++)
                    Pairs.Add(new PairInfo
                    {
                        Item1 = i,
                        Item2 = j,
                        Distance = BoxPositions[i].DistanceTo(BoxPositions[j])
                    });
            
            // Lookup list to find closest pairs faster
            // Sort pairs by distance, faster to find closest pairs using "First" later
            Pairs = Pairs.OrderBy(p => p.Distance).ToList();

            // Labels that indicate which circuit each box belongs to
            for (int i = 0; i < BoxPositions.Count; i++)
                BoxInCircuit[i] = i;
        }

        private static string SolvePart1()
        {
            for (int i = 0; i < 1000; i++)
            {
                var pair = Pairs[i];

                if (BoxInCircuit[pair.Item1] != BoxInCircuit[pair.Item2])
                {
                    var secondCircuitId = BoxInCircuit[pair.Item2];
                    var boxesOfSecond = BoxInCircuit.Keys.Where(x => BoxInCircuit[x] == secondCircuitId);
                    foreach (var ind in boxesOfSecond)
                        BoxInCircuit[ind] = BoxInCircuit[pair.Item1];
                }
            }
            return BoxInCircuit.GroupBy(x => x.Value)
                               .Select(g => g.Count())
                               .OrderByDescending(x => x).Take(3)
                               .Aggregate(1, (acc, val) => acc * val)
                               .ToString();
        }

        private static string SolvePart2()
        {
            var pairToMerge = Pairs.Where(x => BoxInCircuit[x.Item1] != BoxInCircuit[x.Item2]).FirstOrDefault();
            long x1 = 0;
            long x2 = 0;
            while (pairToMerge!=null)
            {
                x1 = BoxPositions[pairToMerge.Item1].x;
                x2 = BoxPositions[pairToMerge.Item2].x;
                var firstCircuitId = BoxInCircuit[pairToMerge.Item1];
                var secondCircuitId = BoxInCircuit[pairToMerge.Item2];
                var boxesOfSecond = BoxInCircuit.Keys.Where(x => BoxInCircuit[x] == secondCircuitId).ToList();
                boxesOfSecond.ForEach(x => BoxInCircuit[x] = firstCircuitId);
                pairToMerge = Pairs.Where(x => BoxInCircuit[x.Item1] != BoxInCircuit[x.Item2]).FirstOrDefault();
            }
            return (x1 * x2).ToString();
        }
    }
}
