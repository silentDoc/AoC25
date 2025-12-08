using System.Diagnostics;

namespace AoC25
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            int day = 8;
            int part = 1;
            bool test = false;
            int testNum = 0;    // Used only to allow multiple test files per day (e.g., _test1, _test2, etc.)

            string input = "./Input/day" + day.ToString("00");
            input += (test) ? "_test" + (testNum > 0 ? testNum.ToString() : "") + ".txt" : ".txt";
            var inputLines = File.ReadAllLines(input).ToList();

            Console.WriteLine("AoC 2025 - Day {0} , Part {1} - Test Data {2}\n", day, part, test);

            Stopwatch st = new();
            st.Start();
            string result = day switch
            {
                1 => Day01.Solver.Solve(inputLines, part),
                2 => Day02.Solver.Solve(inputLines, part),
                3 => Day03.Solver.Solve(inputLines, part),
                4 => Day04.Solver.Solve(inputLines, part),
                5 => Day05.Solver.Solve(inputLines, part),
                6 => Day06.Solver.Solve(inputLines, part),
                7 => Day07.Solver.Solve(inputLines, part),
                8 => Day08.Solver.Solve(inputLines, part),
                _ => throw new ArgumentException("Wrong day number - unimplemented")
            };
            st.Stop();

            Console.WriteLine("Result : {0}\nElapsed : {1}", result, st.Elapsed.TotalSeconds);
        }
    }
}
