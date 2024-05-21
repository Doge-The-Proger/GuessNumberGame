/// <summary>
/// Логика самой игры.
/// </summary>
public class GameLogic
{
    private readonly IGuessNumber _guessNumber;

    private readonly int _attemptCount;

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
            for (int i = 0; i < _attemptCount; i++)
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
                        if (isSuccess)
                        {
                            Console.WriteLine("Поздравляю, ты отгадал число!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("К сожалению ты не угадал число, -1 попытка!");
                        }
                    }
                }
            }
            Console.WriteLine("Игра завершена!");
        }
    }
}