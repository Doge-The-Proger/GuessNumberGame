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