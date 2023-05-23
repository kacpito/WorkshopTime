using TimeStructures;

class WorkshopApp
{
    static void Main()
    {
        var timeOne = new Time("12:12:12");
        var timeTwo = new Time(0, 0, 0);

        var timePeriodOne = new TimePeriod("1:00:00");
        var timePeriodTwo = new TimePeriod("24:00:00");

        Console.WriteLine("ToString()");
        Console.WriteLine(timeOne);
        Console.WriteLine(timeTwo);

        Console.WriteLine();

        Console.WriteLine(timePeriodOne);
        Console.WriteLine(timePeriodTwo);

        Console.WriteLine("\nEquals\n");

        Console.WriteLine(timeOne ==  timeTwo);                     // false
        Console.WriteLine(timeOne != timeTwo);                      // true
        Console.WriteLine(timeOne == timeOne);                      // true

        Console.WriteLine();

        Console.WriteLine(timePeriodOne == timePeriodTwo);          // false
        Console.WriteLine(timePeriodOne != timePeriodTwo);          // true
        Console.WriteLine(timePeriodOne == timePeriodOne);          // true

        Console.WriteLine("\nCompare\n");

        Console.WriteLine(timeOne <  timeTwo);
        Console.WriteLine(timeOne > timeTwo);

        Console.WriteLine(timePeriodOne < timePeriodTwo);
        Console.WriteLine(timePeriodOne > timePeriodTwo);

        Console.WriteLine("\nArithmetic operations");

        Console.WriteLine(timeTwo + timeOne);
        Console.WriteLine(timeTwo - timeOne);

        Console.WriteLine(timeOne.Plus(timePeriodOne));
        Console.WriteLine(timeTwo.Plus(timePeriodOne));

        Console.WriteLine(timeOne.Minus(timePeriodOne));
        Console.WriteLine(timeTwo.Minus(timePeriodOne));

        Console.WriteLine(timePeriodOne + timePeriodTwo);
        Console.WriteLine(timePeriodOne - timePeriodTwo);

        Console.WriteLine(timePeriodOne * 2);
        Console.WriteLine(2 * timePeriodTwo);

    }
}

