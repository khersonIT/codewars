using System;
using System.Linq;
using System.Text;

// https://www.codewars.com/kata/58ad317d1541651a740000c5

namespace myjinxin
{

    public class Kata
    {
        public string MiddlePermutation(string s)
        {
            s = new string(s.OrderBy(c => c).ToArray());
            var res = new StringBuilder();

            if (s.Length % 2 == 0)
            {
                res.Append(s[s.Length / 2 - 1]);

                for (int i = s.Length - 1; i >= 0; i--)
                    if (i != s.Length / 2 - 1)
                        res.Append(s[i]);
            }
            else
            {
                var first = (int)Math.Ceiling(s.Length / 2.0) - 1;
                var sec = (int)Math.Floor(s.Length / 2.0) - 1;

                res.Append(s[first]);
                res.Append(s[sec]);

                for (int i = s.Length - 1; i >= 0; i--)
                    if (i != first && i != sec)
                        res.Append(s[i]);

            }
            return res.ToString();
        }
    }
}