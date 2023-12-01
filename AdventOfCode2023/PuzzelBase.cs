abstract class PuzzleBase {
    public abstract string[] GetExampleInput();
    public abstract string GetExampleOutPutOfPuzzle1();
    public abstract string GetExampleOutPutOfPuzzle2();

    public abstract string[] GetTestingInput();

    public abstract string RunPuzzle1(string[] lines);
    public abstract string RunPuzzle2(string[] lines);

    public void Run() {
        Console.WriteLine("Running " + GetType().Name);

        var exampleInput = GetExampleInput();

        var resultFromPuzzle1WithExampleInput = RunPuzzle1(exampleInput);
        if (GetExampleOutPutOfPuzzle1() != resultFromPuzzle1WithExampleInput) {
            Console.WriteLine("Puzzle 1 failed.");
            Console.WriteLine("Expected result: " + GetExampleOutPutOfPuzzle1());
            Console.WriteLine("Actual result  : " + resultFromPuzzle1WithExampleInput);
            Environment.Exit(1);
        }

        Console.WriteLine("Puzzle 1 completed. Result = " + RunPuzzle1(GetTestingInput()));

        var resultFromPuzzle2WithExampleInput = RunPuzzle2(exampleInput);
        if (GetExampleOutPutOfPuzzle1() != resultFromPuzzle2WithExampleInput) {
            Console.WriteLine("Puzzle 2 failed.");
            Console.WriteLine("Expected result: " + GetExampleOutPutOfPuzzle2());
            Console.WriteLine("Actual result  : " + resultFromPuzzle2WithExampleInput);
            Environment.Exit(1);
        }

        Console.WriteLine("Puzzle 2 completed. Result = " + RunPuzzle2(GetTestingInput()));
    }
} 