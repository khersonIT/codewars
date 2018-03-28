using System.Text;

// https://www.codewars.com/kata/52742f58faf5485cae000b9a

public class HumanTimeFormat
{
    public static string formatDuration(int seconds)
    {
        var delta = new int[5] { 60, 60, 24, 365, 365 };
        var timeValues = new int[5] { 0, 0, 0, 0, 0 };
        var timeNames = new string[5] { "second", "minute", "hour", "day", "year" };
        var builder = new StringBuilder();

        if (seconds == 0)
            builder.Append("now");

        for (int i = 0; i < 5; i++)
        {
            timeValues[i] = seconds % delta[i];
            seconds = seconds / delta[i];
        }

        for (int i = 4; i >= 0; i--)
        {
            if (timeValues[i] != 0)
            {
                builder.Append($"{timeValues[i]} {timeNames[i]}");
                if (timeValues[i] != 1)
                    builder.Append($"s");
                builder.Append(", ");
            }
        }

        var res = builder.ToString().Trim(new char[] { ',', ' ' });
        int lastComma = res.LastIndexOf(',');

        if (lastComma != -1)
        {
            res = res.Remove(lastComma, 1);
            res = res.Insert(lastComma, " and");
        }

        return res;
    }
}