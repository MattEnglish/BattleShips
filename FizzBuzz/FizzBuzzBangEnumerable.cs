using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz
{
  public class FizzBuzzBangEnumerable : IEnumerable<string>
  {
    public IEnumerator<string> GetEnumerator()
    {
      var rules = new Dictionary<int, string> { { 3, "Fizz" }, { 5, "Buzz" }, { 7, "Bang" } };

      for (int i = 1; i <= 200; i++)
      {
        if (rules.Any(rule => i % rule.Key == 0))
        {
          yield return string.Join("", rules.Where(rule => i%rule.Key == 0).Select(rule => rule.Value));
        }
        else
        {
          yield return i.ToString();
        }
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}