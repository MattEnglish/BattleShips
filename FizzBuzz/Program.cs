using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
  class Program
  {
    static void Main(string[] args)
    {
      var limit = PromptForLimit();
      var enabledRules = CalculateEnabledRules(args);

      Console.WriteLine("Fizzing with rules: " + string.Join(", ", enabledRules));
      StretchGoals.FizzFezzBuzzBangBongReverseWithOptions(limit, enabledRules);

      Console.ReadLine();
    }

    private static int PromptForLimit()
    {
      int limit;

      while (true)
      {
        Console.Write("Please enter the number you'd like to FizzBuzz up to: ");
        string limitText = Console.ReadLine();

        if (int.TryParse(limitText, out limit))
        {
          break;
        }
      }
      return limit;
    }

    private static int[] CalculateEnabledRules(string[] commandLineArguments)
    {
      return commandLineArguments.Length == 0
        ? new[] { 3, 5, 7, 11, 13, 17 }
        : commandLineArguments.Select(int.Parse).ToArray();
    }
  }
}
