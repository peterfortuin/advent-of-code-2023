using System.Collections.ObjectModel;

class Day04 : PuzzleBase
{
    public override List<string> GetExampleInputOfPuzzle1()
    {
        return [
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        ];
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return GetExampleInputOfPuzzle1();
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "13";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "30";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day04");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> lines)
    {
        var cards = lines
            .Select(line => ParseLine(line))
            .ToList();

        return cards
            .Select(card => card.Points)
            .Sum()
            .ToString();
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> lines)
    {
        var numberOfCards = 0;

        var cards = lines
            .Select(line => ParseLine(line))
            .ToList();

        Dictionary<int, Card> cardDictonary = [];

        cards.ForEach(card => cardDictonary.Add(card.CardNumber, card));

        var listToProcess = cards.ToList();

        while (listToProcess.Count > 0) {
            var currentCard = listToProcess[listToProcess.Count - 1];
            listToProcess.RemoveAt(listToProcess.Count - 1);
            numberOfCards++;

            for (int i = currentCard.CardNumber + 1; i < currentCard.CardNumber + currentCard.CorrectNumbers + 1; i++)
            {
                listToProcess.Add(cardDictonary[i]);
            }
        }

        return numberOfCards.ToString();
    }

    private Card ParseLine(string line)
    {
        var cardNumberSplit = line.Split(':');
        var cardNumber = int.Parse(cardNumberSplit[0].Substring(5));
        var numberSplit = cardNumberSplit[1].Split('|');
        var winningNumbers = new SortedSet<int>(
            numberSplit[0]
                .Split(' ')
                .Where(element => !string.IsNullOrEmpty(element))
                .Select(numberString => int.Parse(numberString))
                .ToList()
            );
        var owningNumbers = new SortedSet<int>(
            numberSplit[1]
                .Split(' ')
                .Where(element => !string.IsNullOrEmpty(element))
                .Select(numberString => int.Parse(numberString))
                .ToList()
            );

        return new Card(cardNumber, winningNumbers, owningNumbers);
    }
}

class Card(int cardNumber, SortedSet<int> winningNumbers, SortedSet<int> owningNumbers)
{
    public int CardNumber => cardNumber;
    public int CorrectNumbers => winningNumbers.Intersect(owningNumbers).Count();
    public int Points => (int)Math.Pow(2, CorrectNumbers - 1);
}