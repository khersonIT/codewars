using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

// https://www.codewars.com/kata/5235c913397cbf2508000048

public class Evaluator
{
    private List<int> flags = new List<int>();
    private char[] ops = new char[4] { '+', '-', '*', '/' };

    public double Evaluate(string expression)
    {
        flags.Clear();
        var raw = expression;
        expression = expression.Replace(" ", "");

        for (int i = 0; i < expression.Length; i++)
            if (ops.Contains(expression[i]) || expression[i] == '(')
                flags.Add(i);

        if (flags.Any())
        {
            if (flags[0] == 0 && expression[0] == '-')
            {
                expression = expression.Remove(0, 1);
                expression = expression.Insert(0, "n");
            }

            for (int i = 0; i < flags.Count - 1; i++)
            {
                if (flags[i + 1] - flags[i] == 1 && expression[flags[i + 1]] == '-')
                {
                    expression = expression.Remove(flags[i + 1], 1);
                    expression = expression.Insert(flags[i + 1], "n");
                }
            }
        }

        expression = expression.Replace(")n(", ")-(");

        return Math.Round(Calculate(expression), 6);
    }

    private double Calculate(string current)
    {
        int start = current.LastIndexOf('(');
        while (start != -1)
        {
            int close = current.IndexOf(')', start);

            var sub = Calculate(current.Substring(start + 1, close - start - 1));
            current = current.Remove(start, close - start + 1);
            current = current.Insert(start, sub.ToString().Replace('-', 'n'));

            start = current.LastIndexOf('(');
        }

        if (current.IndexOfAny(ops) == -1)
        {
            if (current[0] == 'n')
            {
                var lastN = current.LastIndexOf('n');

                if (current.Length > 1)
                    return ((lastN + 1) % 2 != 0 ? -1 : 1) * Convert.ToDouble(current.Substring(lastN + 1), CultureInfo.InvariantCulture);
                else
                    return -1;
            }
            else
                return Convert.ToDouble(current, CultureInfo.InvariantCulture);
        }

        foreach (var op in ops)
        {
            int index = current.IndexOf(op);
            if (index == -1)
                continue;

            var left = Calculate(current.Substring(0, index));
            var right = Calculate(current.Substring(index + 1));

            switch (op)
            {
                case '/':
                    return Math.Round(left / right, 8);
                case '*':
                    return left * right;
                case '-':
                    return left - right;
                case '+':
                    return left + right;
            }
        }

        return 0;
    }
}