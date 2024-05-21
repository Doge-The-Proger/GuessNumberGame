/// <summary>
/// Настройка параметров игры.
/// </summary>
public static class GameSettings
{
    /// <summary>
    /// Подготовка игровой логики
    /// </summary>
    /// <returns>Готовый к игре экзмепляр логики</returns>
    public static GameLogic PrepareSession()
    {
        IGuessNumber guessNumber = new GuessNumberInt();
        GameLogic game;
        var startNumber = "0";
        var endNumber = "10";
        var attemptCount = "5";
        var numberType = "0";

        Console.Write("Давай настроим игру? (Да - Y/Нет - N): ");
        var request = Console.ReadLine()?.ToUpper();
        if (request == "Y")
        {
            Console.Write("Введи левую границу отрезка чисел: ");
            startNumber = Console.ReadLine()?.Trim();
            Console.Write("Введи правую границу отрезка чисел: ");
            endNumber = Console.ReadLine()?.Trim();
            Console.Write("Введи кол-во попыток на угадывание: ");
            attemptCount = Console.ReadLine()?.Trim();
            Console.Write("Введи тип числа (0, если целое и 1 если с плавающей точкой): ");
            numberType = Console.ReadLine()?.Trim();

            if (int.TryParse(numberType, out var numType))
            {
                if (numType == (int)NumberType.IntNum)
                {
                    guessNumber = new GuessNumberInt();
                }
                else if (numType == (int)NumberType.DoubleNum)
                {
                    guessNumber = new GuessNumberDouble();
                }
                else
                {
                    Console.WriteLine("Передан неверный тип числа (четвёртое число в настройках)");
                    return null;
                }
            }
        }
        else
        {
            var numType = numberType == "0" ? "Целое" : "С плавающей точкой";
            Console.WriteLine($"Была выбрана игра без предварительных настроек. Правила следующие:" +
                $"\n Левая граница - {startNumber}" +
                $"\n Правая граница - {endNumber}" +
                $"\n Кол-во попыток - {attemptCount}" +
                $"\n Тип числа - {numType}");

            guessNumber = new GuessNumberInt();
        }

        guessNumber.PrepareNumber(startNumber, endNumber);
        game = new GameLogic(guessNumber, attemptCount);
        return game;
    }
}