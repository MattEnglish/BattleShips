using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzBuzz
{
  public class Part2
  {
    public static void FizzBuzzBang()
    {
      for (int i = 1; i <= 200; i++)
      {
        var builder = new StringBuilder();

        if (i%3 == 0)
        {
          builder.Append("Fizz");
        }

        if (i%5 == 0)
        {
          builder.Append("Buzz");
        }

        if (i%7 == 0)
        {
          builder.Append("Bang");
        }

        Console.WriteLine(builder.Length == 0 ? i.ToString() : builder.ToString());
      }
    }

    public static void FizzBuzzBangGoesTheDictionary()
    {
      var rules = new Dictionary<int, string> {{3, "Fizz"}, {5, "Buzz"}, {7, "Bang"}};

      for (int i = 1; i <= 200; i++)
      {
        if (rules.Any(rule => i%rule.Key == 0))
        {
          Console.WriteLine(string.Join("", rules.Where(rule => i%rule.Key == 0).Select(rule => rule.Value)));
        }
        else
        {
          Console.WriteLine(i);
        }
      }
    }

    public static void FizzBuzzBangBong()
    {
      for (int i = 1; i <= 200; i++)
      {
        var builder = new StringBuilder();

        if (i % 3 == 0)
        {
          builder.Append("Fizz");
        }

        if (i % 5 == 0)
        {
          builder.Append("Buzz");
        }

        if (i % 7 == 0)
        {
          builder.Append("Bang");
        }

        if (i % 11 == 0)
        {
          builder.Clear();
          builder.Append("Bong");
        }

        Console.WriteLine(builder.Length == 0 ? i.ToString() : builder.ToString());
      }
    }

    public static void FizzFezzBuzzBangBong()
    {
      for (int i = 1; i <= 200; i++)
      {
        var builder = new StringBuilder();
        bool fezz = false;

        if (i%3 == 0)
        {
          builder.Append("Fizz");
        }

        if (i%13 == 0)
        {
          builder.Append("Fezz");
          fezz = true;
        }

        if (i%5 == 0)
        {
          builder.Append("Buzz");
        }

        if (i%7 == 0)
        {
          builder.Append("Bang");
        }

        if (i%11 == 0)
        {
          builder.Clear();
          if (fezz) builder.Append("Fezz");
          builder.Append("Bong");
        }

        Console.WriteLine(builder.Length == 0 ? i.ToString() : builder.ToString());
      }
    }

    public static void FizzFezzBuzzBangBongLazily()
    {
      var simpleRules = new Dictionary<int, string> { { 3, "Fizz" }, { 13, "Fezz" }, { 5, "Buzz" }, { 7, "Bang" }, {11, "Bong"} };

      for (int i = 1; i <= 200; i++)
      {
        // Get the basic list of strings
        var outputs = simpleRules.Where(rule => i%rule.Key == 0).Select(rule => rule.Value).ToList();

        // Apply more complex rules
        if (outputs.Contains("Bong"))
        {
          outputs = outputs.Where(s => s == "Bong" || s == "Fezz").ToList();
        }

        Console.WriteLine(outputs.Count == 0 ? i.ToString() : string.Join("", outputs));
      }
    }

    public static void FizzFezzBuzzBangBongReverse()
    {
      var simpleRules = new Dictionary<int, string> { { 3, "Fizz" }, { 13, "Fezz" }, { 5, "Buzz" }, { 7, "Bang" }, { 11, "Bong" } };

      for (int i = 1; i <= 300; i++)
      {
        // Get the basic list of strings
        var outputs = simpleRules.Where(rule => i % rule.Key == 0).Select(rule => rule.Value).ToList();

        // Apply more complex rules
        if (outputs.Contains("Bong"))
        {
          outputs.RemoveAll(s => s != "Bong" && s != "Fezz");
        }

        if (i%17 == 0)
        {
          outputs.Reverse();
        }

        Console.WriteLine(outputs.Count == 0 ? i.ToString() : string.Join("", outputs));
      }
    }
  }
}