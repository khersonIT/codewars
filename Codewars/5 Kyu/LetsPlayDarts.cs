using System;

// https://www.codewars.com/kata/5870db16056584eab0000006

public class Dartboard
{
    private static int[] values = new int[] { 11, 8, 16, 7, 19, 3, 17, 2, 15, 10, 6, 13, 4, 18, 1, 20, 5, 12, 9, 14, 11 };

    public string GetScore(double x, double y)
    {
        var l = Math.Sqrt(x * x + y * y);

        var rad = Math.Atan2(y, x) * 180 / Math.PI + 180;

        int pointScore = values[(int)Math.Round(rad / 18)];

        if (l > 170)
            return "X";
        if (l > 162)
            return $"D{pointScore}";
        if (l > 107)
            return pointScore.ToString();
        if (l > 99)
            return $"T{pointScore}";
        if (l > 15.9)
            return pointScore.ToString();
        if (l > 6.35)
            return "SB";

        return "DB";
    }
}