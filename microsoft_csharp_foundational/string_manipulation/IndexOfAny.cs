namespace microsoft_csharp_foundational;

public class IndexOfAny
{
    private static void UseOfIndexAny()
    {
        string message = "Help (find) {opening symbols}";
        Console.WriteLine($"$Searching THIS Message: {message}");

        char[] openSymbols = { '[', '{', '(' };
        int startPosition = 5;

        // IndexOfAny\\\\\\\
        int openingPosition = message.IndexOfAny(openSymbols);
        Console.WriteLine(openingPosition);
        Console.WriteLine($"Found WITHOUT using startPosition: {message.Substring(openingPosition)}");


        // start looking for the symbols from the startPosition
        // IndexOfAny(char[] anyOf, int startIndex)

        openingPosition = message.IndexOfAny(openSymbols, startPosition); // {
        Console.WriteLine(openingPosition);
        Console.WriteLine($"Found  WITH using startPosition {startPosition}: {message.Substring(openingPosition)}");
    }
}