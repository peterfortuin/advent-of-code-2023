using System.Collections.ObjectModel;

class Day02 : PuzzleBase
{
    public override List<string> GetExampleInputOfPuzzle1()
    {
        return [
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        ];
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return [
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        ];
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "8";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "2286";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day02");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> lines)
    {
        var games = lines.Select(ParseLine).ToList();

        return games
        .Select(game =>
        {
            if (game.RedMax <= 12 && game.GreenMax <= 13 && game.BlueMax <= 14) {
                return game.GameId;
            }
            else
                return 0;
        })
        .Sum()
        .ToString();
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> lines)
    {
        var games = lines.Select(ParseLine).ToList();

        return games
        .Select(game =>
        {
            return game.RedMax * game.GreenMax * game.BlueMax;
        })
        .Sum()
        .ToString();
    }

    private Game ParseLine(string line)
    {
        var splittedLine = line.Split(':');
        var gameId = int.Parse(splittedLine[0][5..]);
        var cubeSetLines = splittedLine[1].Split(';');

        var cubeSets = cubeSetLines.Select(cubeSetLine =>
        {
            var red = 0;
            var green = 0;
            var blue = 0;

            var cubeLines = cubeSetLine.Split(',');
            foreach (var cubeLine in cubeLines)
            {
                var cubeLineSplitted = cubeLine.Split(' ');
                switch (cubeLineSplitted[2])
                {
                    case string color when color == "red": red = int.Parse(cubeLineSplitted[1]); break;
                    case string color when color == "green": green = int.Parse(cubeLineSplitted[1]); break;
                    case string color when color == "blue": blue = int.Parse(cubeLineSplitted[1]); break;
                }
            }

            return new CubeSet(red, green, blue);
        }).ToList();

        return new Game(gameId, cubeSets);
    }
}

internal class Game(int gameId, List<CubeSet> sets)
{
    public int GameId { get; } = gameId;
    public List<CubeSet> CubeSets { get; } = sets;

    public int RedMax { get; } = sets.Select(set => set.Red).Max();
    public int GreenMax { get; } = sets.Select(set => set.Green).Max();
    public int BlueMax { get; } = sets.Select(set => set.Blue).Max();

    public int RedMin { get; } = sets.Select(set => set.Red).Where(value => value > 0).DefaultIfEmpty(0).Min();
    public int GreenMin { get; } = sets.Select(set => set.Green).Where(value => value > 0).DefaultIfEmpty(0).Min();
    public int BlueMin { get; } = sets.Select(set => set.Blue).Where(value => value > 0).DefaultIfEmpty(0).Min();
}

internal class CubeSet(int red, int green, int blue)
{
    public int Red { get; } = red;
    public int Green { get; } = green;
    public int Blue { get; } = blue;
}