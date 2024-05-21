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
    public bool IsNumberGuessed(string number);

    /// <summary>
    /// Подготовка рандомного числа для отгадывания.
    /// </summary>
    /// <param name="leftNumber">Левая граница диапазона.</param>
    /// <param name="rightNumber">Правая граниа диапазона</param>
    public void PrepareNumber(string leftNumber, string rightNumber);
}