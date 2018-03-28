using System;

// https://www.codewars.com/kata/555615a77ebc7c2c8a0000b8

public class Line
{
    public static string Tickets(int[] peopleInLine)
    {
        int c25, c50;
        c25 = c50 = 0;

        for (int i = 0; i < peopleInLine.Length; i++)
        {
            switch (peopleInLine[i])
            {
                case 25:
                    c25++;
                    break;
                case 50:
                    c25--;
                    c50++;
                    break;
                case 100:
                    if (c50 > 0)
                    {
                        c25--;
                        c50--;
                    }
                    else
                        c25 -= 3;
                    break;
            }

            if (c25 < 0)
                return "NO";
        }

        return "YES";
    }
}