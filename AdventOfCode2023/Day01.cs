using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

class Day01 : PuzzleBase
{
    public override List<string> GetExampleInputOfPuzzle1()
    {
        return [
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        ];
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return [
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        ];
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "142";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "281";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day01");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> lines)
    {
        return lines.Select(line =>
        {
            var firstDigit = line.FirstOrDefault(char.IsDigit).ToString();
            var lastDigit = line.LastOrDefault(char.IsDigit).ToString();
            return firstDigit + lastDigit;
        })
        .Select(digitAsString => int.Parse(digitAsString))
        .Sum()
        .ToString();
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> lines)
    {
        return lines
        .Select(line =>
        {
            return ReplaceDigits(line) + Reverse(ReverseReplaceDigits(Reverse(line)));
        })
        .Select(line =>
        {
            var firstDigit = line.FirstOrDefault(char.IsDigit).ToString();
            var lastDigit = line.LastOrDefault(char.IsDigit).ToString();
            return firstDigit + lastDigit;
        })
        .Select(digitAsString => int.Parse(digitAsString))
        .Sum()
        .ToString();
    }

    private static string ReplaceDigits(string line)
    {
        return Regex.Replace(line, "one|two|three|four|five|six|seven|eight|nine", match =>
        {
            return match.Value switch
            {
                "one" => "1",
                "two" => "2",
                "three" => "3",
                "four" => "4",
                "five" => "5",
                "six" => "6",
                "seven" => "7",
                "eight" => "8",
                "nine" => "9",
            };
        });
    }

    private static string ReverseReplaceDigits(string line)
    {
        return Regex.Replace(line, "eno|owt|eerht|ruof|evif|xis|neves|thgie|enin", match =>
        {
            return match.Value switch
            {
                "eno" => "1",
                "owt" => "2",
                "eerht" => "3",
                "ruof" => "4",
                "evif" => "5",
                "xis" => "6",
                "neves" => "7",
                "thgie" => "8",
                "enin" => "9",
            };
        });
    }

    private static string Reverse(string line)
    {
        char[] characterLine = line.ToCharArray();
        Array.Reverse(characterLine);
        return ReplaceDigits(new string(characterLine));
    }
}