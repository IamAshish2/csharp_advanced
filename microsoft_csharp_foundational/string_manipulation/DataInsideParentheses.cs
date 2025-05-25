namespace microsoft_csharp_foundational;

public struct DataInsideParentheses
{
    public static void getDataInsideParentheses()
    {
        // get all the data inside the parentheses
        string message = "(What if) there are (more than) one (set of parentheses)?";

        while (true)
        {
            int openingPosition = message.IndexOf('(');
            if (openingPosition < 0) return;

            int closingPosition = message.IndexOf(')');

            int length = closingPosition - openingPosition;

            Console.WriteLine(message.Substring(openingPosition + 1, length- 1));
    
            message = message.Substring(closingPosition + 1);
        }
    }
}