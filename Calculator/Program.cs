using System;

namespace Calculator
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      // evaluate arguments given by user
      EvaluteArgs(args[0]);
    }

    /// <summary>
    ///   evaluates argument given by user, outputs sum or error
    /// </summary>
    /// <param name="sumString">argument given by user</param>
    private static void EvaluteArgs(string sumString)
    {
      // catch all for errors
      try
      {
        // split string to check if more than just a number entered
        if (sumString.Split(' ').Length > 1)
        {
          // characters to separate string by
          char[] separators =
          {
            '(',
            ' ',
            ')'
          };

          // get last instance of ( to indicate start of first expression
          var expStart = sumString.LastIndexOf('(');
          // get first instance of ) to indicate end of first expression
          var expEnd = sumString.IndexOf(')');

          // expression is the substring of string starting at first ( and ending at )
          var expression = sumString.Substring(expStart, expEnd - expStart + 1);

          // split the expression by separators
          var splitExpression = expression.Split(separators);

          var newInt = 0;

          // if operator is add, add integers to newInt
          if (splitExpression[1] == "add")
          {
            // this allows for more than two integers to be added
            for (var i = 2; i < splitExpression.Length - 1; i++)
            {
              newInt += int.Parse(splitExpression[i]);
            }
          }
          // if operator is multiply, add first integer to newInt, multiply after that
          else if (splitExpression[1] == "multiply")
          {
            // this allows for more than two integers to be multiplied
            for (var i = 2; i < splitExpression.Length - 1; i++)
            {
              if (newInt == 0)
              {
                newInt = int.Parse(splitExpression[i]);
              }
              else
              {
                newInt *= int.Parse(splitExpression[i]);
              }
            }
          }

          // edit string to remove previous expression and insert sum of it
          var editedString = sumString.Remove(expStart, expEnd - expStart + 1).Insert(expStart, newInt.ToString());

          // call EvaluateArgs again with editedString to see if there is another expression we need to evaluate
          EvaluteArgs(editedString);
        }
        // output string if length is 1
        else
        {
          Console.WriteLine(sumString);
        }
      }
      catch (Exception e)
      {
        // generic error
        Console.WriteLine("Error!");
      }
    }
  }
}