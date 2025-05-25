namespace microsoft_csharp_foundational;

public class MatchingOpeningBracket
{
    private static void MatchingBracketFinder()
    {
        string message = "(What if) I have [different symbols] but every {open symbol} needs a [matching closing symbol]?";

// The IndexOfAny() helper method requires a char array of characters. 
// You want to look for:

        char[] openSymbols = { '[', '{', '(' };

// You'll use a slightly different technique for iterating through 
// the characters in the string. This time, use the closing 
// position of the previous iteration as the starting index for the 
//next open symbol. So, you need to initialize the closingPosition 
// variable to zero:

        int closingPositon = 0;

        while (true)
        {
            int openingPosition = message.IndexOfAny(openSymbols);

            if (openingPosition == -1) break;

            string currentSymbol = message.Substring(openingPosition, 1);
    
            // now find thes matching  closing statement
            string matchingSymbol = "";
    
            switch (currentSymbol)
            {
                case "[" :
                    matchingSymbol = "]";
                    break;
        
                case "{":
                    matchingSymbol = "}";
                    break;
                case "(":
                    matchingSymbol = ")";
                    break;
                default:
                    break;
            }

            openingPosition += 1;
            closingPositon = message.IndexOf(matchingSymbol,openingPosition);

            int length = closingPositon - openingPosition;
    
            // print the content inside of the parenthesis
            Console.WriteLine(message.Substring(openingPosition,length));

            message = message.Substring(closingPositon + 1);
        }
    }
}