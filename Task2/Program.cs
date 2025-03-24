using System.Net.NetworkInformation;

public delegate bool NumberCheck(int number);

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Console.WriteLine("Четные числа: ");
        List<int> evenNumbers = FilterNumbers(numbers, IsEven);
        foreach (var num in evenNumbers)
        {
            Console.WriteLine(num);
        }

        Console.WriteLine("Нечетные числа: ");
        List<int> oddNumbers = FilterNumbers(numbers, IsOdd);
        foreach (var num in oddNumbers)
        {
            Console.WriteLine(num);
        }
    }

    public static List<int> FilterNumbers(int[] numbers, NumberCheck check)
    {
        List<int> result = new List<int>();
        foreach (var number in numbers)
        {
            if (check(number))
            {
                result.Add(number);
            }
        }
        return result;
    }

    public static bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    public static bool IsOdd(int number)
    {
        return number % 2 != 0;
    }
}