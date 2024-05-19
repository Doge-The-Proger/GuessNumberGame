#region Точка входа

Console.WriteLine("Привет, давай сыграем в Угадай число!");
while (true)
{
    Console.Write("Выбери: Игра(Y) или Выход(N)?: ");
    var startRequest = Console.ReadLine()?.Trim().ToUpper();
    if (startRequest == "N")
    {
        break;
    }
    else if (startRequest == "Y")
    {
        //Настройка
        var game = GameSettings.PrepareSession();
        if (game == null)
        {
            Console.WriteLine("Во время формирования сессии возникли проблемы. Попробуй ещё раз.");
        }
        else
        {
            //Игра
            game.PlayGame();
        }
    }
    else
    {
        Console.WriteLine("Введен неправильный вариант выбора! Начинаем сначала.");
    }
}

#endregion

#region Игра: настройка и запуск

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

/// <summary>
/// Логика самой игры.
/// </summary>
public class GameLogic
{
    private IGuessNumber _guessNumber { get; set; }

    private int _attemptCount { get; set; }

    public GameLogic(IGuessNumber guessNumber, string attemptCount)
    {
        _guessNumber = guessNumber;
        if (!int.TryParse(attemptCount, out _))
        {
            Console.WriteLine("Кол-во попыток задано некорректно (третье число в настройках).");
        }
        else
        {
            _attemptCount = int.Parse(attemptCount);
        }
    }

    /// <summary>
    /// Запуск игры
    /// </summary>
    public void PlayGame()
    {
        if (_attemptCount == 0)
        {
            Console.WriteLine("Играть без попыток невозможно, сделайте хотя бы одну!");
        }
        else
        {
            Console.WriteLine("Если желаешь завершить игру досрочно - введи N. Игра начинается!");
            while (_attemptCount > 0)
            {
                var request = Console.ReadLine()?.ToUpper();
                if (request != null)
                {
                    if (request == "N")
                    {
                        break;
                    }
                    else
                    {
                        var isSuccess = _guessNumber.IsNumberGuessed(request);
                        if (isSuccess.HasValue)
                        {
                            if (isSuccess.Value)
                            {
                                Console.WriteLine("Поздравляю, ты отгадал число!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("К сожалению ты не угадал число, -1 попытка!");
                                _attemptCount--;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Игра завершена!");
        }
    }
}

#endregion

#region Логика отгадывания числа

/// <summary>
/// Интерфейс числа для отгадывания
/// </summary>
public interface IGuessNumber
{
    /// <summary>
    /// Проверка, отгадано ли число.
    /// </summary>
    /// <param name="number">Число в формате строки.</param>
    /// <returns>True - число отгадано; False - число не отгадано.</returns>
    public bool? IsNumberGuessed(string number);

    /// <summary>
    /// Подготовка рандомного числа для отгадывания.
    /// </summary>
    /// <param name="leftNumber">Левая граница диапазона.</param>
    /// <param name="rightNumber">Правая граниа диапазона</param>
    public void PrepareNumber(string leftNumber, string rightNumber);
}

/// <summary>
/// Логика отгадывания целого числа.
/// </summary>
public class GuessNumberInt : IGuessNumber
{
    private int _myNumber;

    public bool? IsNumberGuessed(string number)
    {
        if (int.TryParse(number, out var num))
        {
            if (_myNumber == num)
            {
                return true;
            }

            return false;
        }
        else
        {
            return null;
        }
    }

    public void PrepareNumber(string leftNumber, string rightNumber)
    {
        if (!int.TryParse(leftNumber, out var firstNumber))
        {
            Console.WriteLine("Первое число в настройках передано неверно.");
        }

        if (!int.TryParse(rightNumber, out var secondNumber))
        {
            Console.WriteLine("Второе число в настройках число передано неверно.");
        }
        _myNumber = new Random().Next(firstNumber, secondNumber);
    }
}

/// <summary>
/// Логика отгадывания числа с плавающей точкой.
/// </summary>
public class GuessNumberDouble : IGuessNumber
{
    private double _myNumber;

    public bool? IsNumberGuessed(string number)
    {
        if (double.TryParse(number.Replace('.', ','), out var num))
        {
            if (_myNumber == num)
            {
                return true;
            }

            return false;
        }
        else
        {
            return null;
        }
    }

    public void PrepareNumber(string leftNumber, string rightNumber)
    {
        if (!double.TryParse(leftNumber.Replace('.', ','), out var firstNumber))
        {
            Console.WriteLine("Первое число в настройках передано неверно.");
        }

        if (!double.TryParse(rightNumber.Replace('.', ','), out var secondNumber))
        {
            Console.WriteLine("Второе число в настройках число передано неверно.");
        }
        _myNumber = new Random().NextDouble() * (firstNumber - secondNumber) + firstNumber;
    }
}

#endregion

#region Вспомогательные классы

/// <summary>
/// Тип числа для игры - целое (Int) или с плавующей точкой (Double)
/// </summary>
public enum NumberType
{
    IntNum,
    DoubleNum
}

#endregion