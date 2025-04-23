namespace LagrangeApp;
public static class ConsoleHelper
{  
    public static void WriteLine(string message,ConsoleColor color = ConsoleColor.White){
        var tmpColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = tmpColor;
    }
    
    public static void Write(string message,ConsoleColor color = ConsoleColor.White){
        var tmpColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = tmpColor;
    }
    
    public static int ReadIntValue(string requestText){
        int currentLine = Console.GetCursorPosition().Top;
        var inputRequired = true;
        int value = 0;
        while (inputRequired)
        {
            ClearLine(currentLine);
            Console.Write($"{requestText} ");
            string? userInput = Console.ReadLine();

            if (int.TryParse(userInput, out value))
            {
                inputRequired = false;
            }
            else
            {
                Console.SetCursorPosition(0, currentLine);
                Console.WriteLine("Введіть ціле число.");
                Console.WriteLine(new string(' ', 50));
            }
        }
        return value;
    }

    public static float ReadFloatValue(string requestText){
        int currentLine = Console.GetCursorPosition().Top;
        var inputRequired = true;
        float value = 0;
        while (inputRequired)
        {
            ClearLine(currentLine);
            Console.Write($"{requestText} ");
            string? userInput = Console.ReadLine();

            if (float.TryParse(userInput, out value))
            {
                inputRequired = false;
            }
            else
            {
                Console.SetCursorPosition(0, currentLine);
                Console.WriteLine("Введіть число.", ConsoleColor.DarkYellow);
                Console.WriteLine(new string(' ', 50));
            }
        }
        return value;
    }
    
    public static void SetCursorPosition(int left, int top){
        if (top >= Console.BufferHeight)
        {
            Console.BufferHeight = top + 1;
        }
        Console.SetCursorPosition(left, top);
    }
    
    public static void ClearLine(int top){
        SetCursorPosition(0,top);
        var str = new string(' ',Console.BufferWidth);
        Console.Write(str);
        SetCursorPosition(0,top);
    }

    public static string AsSuperscript(int value)
    {
        var result = "";
        if (value < 0) throw new ArgumentException("Значення має бути додатнім");
        var str = value.ToString();
        foreach (var ch in str)
        {
            result = result + NumberToSuperscipt(ch);;
        }
        return result;
    }

    public static string AsSubscript(int value)
    {
        var result = "";
        if (value < 0) throw new ArgumentException("Значення має бути додатнім");
        var str = value.ToString();
        foreach (var ch in str)
        {
            result = result + NumberToSubscipt(ch);;
        }
        return result;
    }

    private static string NumberToSuperscipt(char ch)
    {
        string code = "";
        switch (ch)
        {
            case '1':  //₁₄₇₂₅₈₀₃₆₉
                code = "\u00B9";
                break;
            case '2':
                code = "\u00B2";
                break;
            case '3':
                code = "\u00B3";
                break;
            case '4':
                code = "\u2074";
                break;
            case '5':
                code = "\u2075";
                break;
            case '6':
                code = "\u2076";
                break;
            case '7':
                code = "\u2077";
                break;
            case '8':
                code = "\u2078";
                break;
            case '9':
                code = "\u2079";
                break;
            case '0':
                code = "\u2070";
                break;
        }

        return code;
    }

    private static string NumberToSubscipt(char ch)
    {
        string code = "";
        switch (ch)
        {
            case '1':  //₁₄₇₂₅₈₀₃₆₉
                code = "\u2081";
                break;
            case '2':
                code = "\u2082";
                break;
            case '3':
                code = "\u2083";
                break;
            case '4':
                code = "\u2084";
                break;
            case '5':
                code = "\u2085";
                break;
            case '6':
                code = "\u2086";
                break;
            case '7':
                code = "\u2087";
                break;
            case '8':
                code = "\u2088";
                break;
            case '9':
                code = "\u2089";
                break;
            case '0':
                code = "\u2080";
                break;
        }

        return code;
    }
}