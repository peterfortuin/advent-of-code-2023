using System.Collections.ObjectModel;

abstract class PuzzleBase
{
    public abstract List<string> GetExampleInputOfPuzzle1();
    public abstract List<string> GetExampleInputOfPuzzle2();
    public abstract string GetExampleOutPutOfPuzzle1();
    public abstract string GetExampleOutPutOfPuzzle2();

    public abstract List<string> GetTestingInput();

    public abstract string RunPuzzle1(ReadOnlyCollection<string> lines);
    public abstract string RunPuzzle2(ReadOnlyCollection<string> lines);

    public void Run()
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("Running " + GetType().Name);
        Console.WriteLine();

        var exampleInputForPuzzle1 = GetExampleInputOfPuzzle1().AsReadOnly();
        var resultFromPuzzle1WithExampleInput = RunPuzzle1(exampleInputForPuzzle1);
        if (GetExampleOutPutOfPuzzle1() != resultFromPuzzle1WithExampleInput)
        {
            Console.WriteLine("Puzzle 1 failed.");
            Console.WriteLine("Expected result : " + GetExampleOutPutOfPuzzle1());
            Console.WriteLine("Actual result   : " + resultFromPuzzle1WithExampleInput);
            Console.WriteLine();
            Environment.Exit(1);
        }

        Console.WriteLine();
        Console.WriteLine("Running Puzzle 1 ....");
        Console.WriteLine("Puzzle 1 completed. Result = " + RunPuzzle1(GetTestingInput().AsReadOnly()));
        Console.WriteLine();

        var exampleInputForPuzzle2 = GetExampleInputOfPuzzle2().AsReadOnly();
        var resultFromPuzzle2WithExampleInput = RunPuzzle2(exampleInputForPuzzle2);
        if (GetExampleOutPutOfPuzzle2() != resultFromPuzzle2WithExampleInput)
        {
            Console.WriteLine("Puzzle 2 failed.");
            Console.WriteLine("Expected result : " + GetExampleOutPutOfPuzzle2());
            Console.WriteLine("Actual result   : " + resultFromPuzzle2WithExampleInput);
            Console.WriteLine();
            Environment.Exit(1);
        }

        Console.WriteLine();
        Console.WriteLine("Running Puzzle 2 ....");
        Console.WriteLine("Puzzle 2 completed. Result = " + RunPuzzle2(GetTestingInput().AsReadOnly()));
        Console.WriteLine();
    }

    public List<String> ReadLines(string puzzle) => [.. File.ReadAllLines("PuzzleInput/" + puzzle + ".txt")];
}