namespace microsoft_csharp_foundational;

public class HtmlParsing
{
    private void parseHtml()
    {
        const string input = "<div><h2>Widgets &trade;</h2><span>5000</span></div>";

        string quantity = "";
        string output = "";

// desired output
// Quantity: 5000
// Output: <h2>Widgets &reg;</h2><span>5000</span>

        string spanOpen = "<span>";
        string spanClose = "</span>";

// first we get the index of the spanOpen to get index which it finds at 29 index and adding length so we can select the
// part which is after <span>
        int spanStartIndex = input.IndexOf(spanOpen) + spanOpen.Length;

        int spanCloseIndex = input.IndexOf(spanClose);

        int spanLength = spanCloseIndex - spanStartIndex;
        quantity = input.Substring(spanStartIndex, spanLength);


        string openDiv = "<div>";
        string closeDiv = "</div>";

        int divStartIndex = input.IndexOf(openDiv) + openDiv.Length;
        int divCloseIndex = input.IndexOf(closeDiv);

        int divLength = divCloseIndex - divStartIndex;

        string tradeReplace = "&reg";
        input.Replace("&trade", tradeReplace);

        output = input.Substring(divStartIndex, divLength);


        Console.WriteLine(quantity);
        Console.WriteLine(output);
    }
}