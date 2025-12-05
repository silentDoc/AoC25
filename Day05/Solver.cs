namespace AoC25.Day05
{
    record IdRange
    {
        public long Low;
        public long High;
        
        public bool InRange(long num)
            => num >= Low && num <= High;

        public bool Contains(IdRange other)
            => other.Low >= Low && other.High <= High;

        public bool Overlaps(IdRange other)
            => other.High >= Low && other.Low <= High;

        public bool Intersects(IdRange other)
            => Contains(other) || Overlaps(other);

        public IdRange Merge(IdRange other)
            => new IdRange
            {
                Low = Math.Min(Low, other.Low),
                High = Math.Max(High, other.High)
            };

        public long Count()
            => High - Low + 1;
    }

    internal class Solver
    {
        static List<IdRange> ranges = new();
        static List<long> nums = new();

        static void ParseInput(List<string> input)
        {
            var separator = input.IndexOf("");
            var ruleLines = input.Take(separator).ToList();
            var numLines = input.Skip(separator + 1).ToList();

            foreach (var line in ruleLines)
            {
                var parts = line.Split("-");
                ranges.Add(new IdRange
                {
                    Low = long.Parse(parts[0]),
                    High = long.Parse(parts[1])
                });
            }

            foreach (var line in numLines)
                nums.Add(long.Parse(line));
        }

        public static string Solve(List<string> input, int part)
        {
            ParseInput(input);
            return part == 1 ? SolvePart1() : SolvePart2();
        }

        private static string SolvePart1()
            => nums.Count(num => ranges.Any(range => range.InRange(num))).ToString();

        private static void MergeAllYouCan()
        {
            List<IdRange> mergedRanges = new();
            ranges.ForEach(mergedRanges.Add);
            
            bool mergedAny = true;
            while(mergedAny)
            {
                mergedAny = false;
                var newMergedRanges = new List<IdRange>();
                mergedRanges.ForEach(newMergedRanges.Add);

                foreach (var range in mergedRanges)
                {
                    foreach (var other in mergedRanges)
                    {
                        if (range != other && range.Intersects(other))
                        {
                            var merged = range.Merge(other);
                            newMergedRanges.Remove(range);
                            newMergedRanges.Remove(other);
                            newMergedRanges.Add(merged);
                            mergedAny = true;
                            break;
                        }
                    }
                    if (mergedAny)
                        break;
                }
                mergedRanges = newMergedRanges;
            }
            ranges = mergedRanges.Distinct().ToList();
        }

        private static string SolvePart2()
        {
            MergeAllYouCan();
            return ranges.Sum(r => r.Count()).ToString();
        }
    }
}
