/// <summary>
/// Логика отгадывания целого числа.
/// </summary>
public class GuessNumberInt : IGuessNumber
{
    private int _myNumber;

    public bool IsNumberGuessed(string number)
    {
        if (_myNumber == int.Parse(number))
        {
            return true;
        }
        return false;
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