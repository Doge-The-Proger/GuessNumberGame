/// <summary>
/// Логика отгадывания числа с плавающей точкой.
/// </summary>
public class GuessNumberDouble : IGuessNumber
{
    private double _myNumber;

    public bool IsNumberGuessed(string number)
    {
        if (_myNumber == double.Parse(number.Replace('.', ',')))
        {
            return true;
        }
        return false;
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