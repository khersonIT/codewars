using System;

// https://www.codewars.com/kata/56a14b6b56e5917073000022

public static class Kata
{
    public static double calculate(string s)
    {
        return (rec(s.Replace(" ", "")));
    }

    private static char[] ops = new char[]
        { '+', '-', '*', '/', '^' };
    private static double rec(string str)
    {
        var openIndex = str.LastIndexOf('(');
        while (openIndex != -1)
        {
            var closeIndex = str.IndexOf(')', openIndex);

            var sub = rec(str.Substring(openIndex + 1, closeIndex - openIndex - 1));
            str = str.Remove(openIndex, closeIndex - openIndex + 1).Insert(openIndex, sub.ToString());

            openIndex = str.LastIndexOf('(');
        }

        var opIndex = str.IndexOfAny(ops);

        if (opIndex == -1)
            return Convert.ToDouble(str);
        else
        {
            foreach (var op in ops)
            {
                opIndex = str.IndexOf(op);

                if (opIndex == -1)
                    continue;

                var left = rec(str.Substring(0, opIndex));
                var right = rec(str.Substring(opIndex + 1));

                switch (str[opIndex])
                {
                    case '+':
                        return left + right;

                    case '-':
                        return left - right;

                    case '*':
                        return left * right;

                    case '/':
                        return left / right;

                    case '^':
                        return Math.Pow(left, right);
                }
            }
        }

        throw new Exception("Unknown operation.");
    }
}