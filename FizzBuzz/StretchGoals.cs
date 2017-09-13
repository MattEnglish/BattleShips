using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz
{
  public class StretchGoals
  {
    public static void FizzBuzzBangOneLiner()
    {
      Enumerable.Range(1, 100).Select(
          i => Tuple.Create(i, string.Join("", new Dictionary<int, string>
          {
            {3, "Fizz"},
            {5, "Buzz"},
            {7, "Bang"}
          }.Where(rule => i%rule.Key == 0).Select(rule => rule.Value))))
        .Select(p => p.Item2 == "" ? p.Item1.ToString() : p.Item2)
        .ToList()
        .ForEach(Console.WriteLine);
    }

    public static void FizzBuzzBangViaEnumerable()
    {
      var fizzBuzzer = new FizzBuzzBangEnumerable();

      foreach (var value in fizzBuzzer)
      {
        Console.WriteLine(value);
      }
    }

    public static void FizzFezzBuzzBangBongReverseWithOptions(int limit, IList<int> enabledRules)
    {
      var simpleRules = new Dictionary<int, string> { { 3, "Fizz" }, { 13, "Fezz" }, { 5, "Buzz" }, { 7, "Bang" }, { 11, "Bong" } };

      for (int i = 1; i <= limit; i++)
      {
        // Get the basic list of strings
        var outputs = simpleRules.Where(rule => enabledRules.Contains(rule.Key) && i % rule.Key == 0).Select(rule => rule.Value).ToList();

        // Apply more complex rules
        if (outputs.Contains("Bong"))
        {
          outputs.RemoveAll(s => s != "Bong" && s != "Fezz");
        }

        if (i % 17 == 0 && enabledRules.Contains(17))
        {
          outputs.Reverse();
        }

        Console.WriteLine(outputs.Count == 0 ? i.ToString() : string.Join("", outputs));
      }
    }

  }
}