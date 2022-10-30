using Serilog;
using System;

class Program
{
    static void Main()
    {
        var tryParseResult = false;
        var rubli = 0.0;
        var dollar = 80.0;
        var komissiya = 0.0;
        var komissiyasmall = 8.0;
        Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console().
        Enrich.WithProperty("Kurs", dollar).
        WriteTo.Seq("http://localhost:5341", apiKey: "xlup2LxC871SSGiyfLj9").
        CreateLogger();
        while (!tryParseResult)
        {
            Console.Write("Введите сумму в долларах: ");
            var userInput = Console.ReadLine();
            tryParseResult = double.TryParse(userInput, out rubli);
            if (!tryParseResult)
            {
                Log.Error("Пользователь ввел сумму некорректно");
            }
        }
        Log.Information($"Пользователь ввел {rubli} долларов");

        Console.WriteLine("Курс доллара сейчас составляет 80 рублей");
        rubli = rubli * dollar;
        Console.WriteLine("Введенная вами сумма долларов в рублях составляет: " + rubli);

        if (rubli < 500.0)
        {
            Console.WriteLine("Т.к. вы снимаете сумму меньше 500 рублей, то комиссия составит всего 8 рублей");
            Console.WriteLine("С учетом комиссии вы получаете: " + (rubli - komissiyasmall));
        }
        else 
        {
            Console.WriteLine("Т.к. вы снимаете сумму больше 500 рублей, то комиссия составляет 0.37%");
            komissiya = rubli / 100.0 * 0.37;
            Console.WriteLine("С учетом комиссии вы получаете: " + (rubli - komissiya));
        }
        Log.Information($"Пользователь получил {rubli} рублей");
        Log.CloseAndFlush();
    }  
}





