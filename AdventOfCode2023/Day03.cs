using System.Collections.ObjectModel;

class Day03 : PuzzleBase
{
    private ReadOnlyCollection<string> lines;

    public override List<string> GetExampleInputOfPuzzle1()
    {
        return [
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        ];
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return GetExampleInputOfPuzzle1();
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "4361";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "467835";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day03");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> l)
    {
        lines = l;

        var numberFound = 0;
        var searchfield = new List<Tuple<int, int>>();
        var sum = 0;

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                var symbol = Get(x, y);

                if (char.IsDigit(symbol))
                {
                    int digit = symbol - '0';
                    numberFound = numberFound * 10 + digit;

                    searchfield.Add(Tuple.Create(x + 1, y));
                    searchfield.Add(Tuple.Create(x + 1, y + 1));
                    searchfield.Add(Tuple.Create(x, y + 1));
                    searchfield.Add(Tuple.Create(x - 1, y + 1));
                    searchfield.Add(Tuple.Create(x - 1, y));
                    searchfield.Add(Tuple.Create(x - 1, y - 1));
                    searchfield.Add(Tuple.Create(x, y - 1));
                    searchfield.Add(Tuple.Create(x + 1, y - 1));
                }
                else if (!char.IsDigit(symbol) && numberFound > 0)
                {
                    if (DoesSearchFieldContainSymbol(searchfield))
                    {
                        sum += numberFound;
                    }
                    numberFound = 0;
                    searchfield.Clear();
                }
            }

            if (numberFound > 0)
            {
                if (DoesSearchFieldContainSymbol(searchfield))
                {
                    sum += numberFound;
                }
                numberFound = 0;
                searchfield.Clear();
            }
        }

        return sum.ToString();
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> l)
    {
        lines = l;

        var numberFound = 0;
        var searchfield = new List<Tuple<int, int>>();
        var gearList = new Dictionary<Tuple<int, int>, List<int>>();
        var sum = 0;

        for (int y = 0; y < lines.Count; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                var symbol = Get(x, y);

                if (char.IsDigit(symbol))
                {
                    int digit = symbol - '0';
                    numberFound = numberFound * 10 + digit;

                    searchfield.Add(Tuple.Create(x + 1, y));
                    searchfield.Add(Tuple.Create(x + 1, y + 1));
                    searchfield.Add(Tuple.Create(x, y + 1));
                    searchfield.Add(Tuple.Create(x - 1, y + 1));
                    searchfield.Add(Tuple.Create(x - 1, y));
                    searchfield.Add(Tuple.Create(x - 1, y - 1));
                    searchfield.Add(Tuple.Create(x, y - 1));
                    searchfield.Add(Tuple.Create(x + 1, y - 1));
                }
                else if (!char.IsDigit(symbol) && numberFound > 0)
                {
                    var possibleFind = FindGearInSearchField(searchfield);
                    if (possibleFind != null)
                    {
                        if (!gearList.ContainsKey(possibleFind))
                        {
                            gearList.Add(possibleFind, []);
                        }
                        gearList[possibleFind].Add(numberFound);
                    }
                    numberFound = 0;
                    searchfield.Clear();
                }
            }

            if (numberFound > 0)
            {
                var possibleFind = FindGearInSearchField(searchfield);
                if (possibleFind != null)
                {
                    if (!gearList.ContainsKey(possibleFind))
                    {
                        gearList.Add(possibleFind, []);
                    }
                    gearList[possibleFind].Add(numberFound);
                }
                numberFound = 0;
                searchfield.Clear();
            }
        }

        foreach (var gear in gearList)
        {
            if (gear.Value.Count == 2)
                sum += gear.Value[0] * gear.Value[1];
        }

        return sum.ToString();
    }

    private char Get(int x, int y)
    {
        if (y < 0 || y >= lines.Count || x < 0 || x >= lines[y].Length) return ' ';
        return lines[y][x];
    }

    private Boolean DoesSearchFieldContainSymbol(List<Tuple<int, int>> searchfield)
    {
        foreach (var field in searchfield)
        {
            var symbol = Get(field.Item1, field.Item2);

            if (!char.IsDigit(symbol) && symbol != '.' && symbol != ' ')
            {
                return true;
            }
        };

        return false;
    }

    private Tuple<int, int>? FindGearInSearchField(List<Tuple<int, int>> searchfield)
    {
        foreach (var field in searchfield)
        {
            var symbol = Get(field.Item1, field.Item2);

            if (symbol == '*')
            {
                return Tuple.Create(field.Item1, field.Item2);
            }
        };

        return null;
    }
}