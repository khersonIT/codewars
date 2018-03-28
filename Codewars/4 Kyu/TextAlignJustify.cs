using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// https://www.codewars.com/kata/537e18b6147aa838f600001b

namespace Solution
{
    public class Kata
    {
        public static string Justify(string str, int len)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var builder = new StringBuilder();

            var lines = new List<List<string>>() { new List<string>() };
            var words = new List<string>(str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            int ln = 0;
            int ll = 0;
            foreach (var w in words)
            {
                ll += w.Length;

                if (ll > len)
                {
                    lines.Add(new List<string>());
                    ll = w.Length + 1;
                    ln++;
                }
                else
                    ll++;

                lines[ln].Add(w);
            }

            for (int l = 0; l < lines.Count - 1; l++)
            {
                if (l != 0)
                    builder.AppendLine();

                double sp = len - string.Join("", lines[l]).Length;

                ll = 0;
                for (int i = 0; i < lines[l].Count; i++)
                {
                    if (i != 0)
                    {
                        int spLD = (int)Math.Ceiling(sp / (lines[l].Count - i));
                        builder.Append(' ', spLD);
                        sp -= spLD;
                        ll += spLD;
                    }
                    builder.Append(lines[l][i]);
                    ll += lines[l][i].Length;
                }
            }

            builder.AppendLine();
            builder.Append(string.Join(" ", lines.Last()));

            return builder.ToString();
        }
    }
}