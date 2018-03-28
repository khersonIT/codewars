using System;
using System.Collections;
using System.Linq;
using System.Text;

// https://www.codewars.com/kata/5aa41082fd8c060a51000043

namespace Kata
{
    public enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    public enum AttackType
    {
        Light = 0,
        Normal = 1,
        Heavy = 2,
        Special = 3
    }

    public class CharacterData
    {
        public byte ID { get; set; }
        public byte Health { get; set; }
        public byte Energy { get; set; }
        public bool Moving { get; set; }
        public bool Jumping { get; set; }
        public bool Sprinting { get; set; }
        public bool Attacking { get; set; }
        public Direction Direction { get; set; }
        public AttackType AttackType { get; set; }
        public short X { get; set; }
        public short Y { get; set; }


        public Int64 Save()
        {
            var bits = new BitArray(64);
            int offset = 0;

            SetBits(bits, 8, ID, ref offset);
            SetBits(bits, 8, Health, ref offset);
            SetBits(bits, 8, Energy, ref offset);
            SetBits(bits, 1, Moving, ref offset);
            SetBits(bits, 1, Jumping, ref offset);
            SetBits(bits, 1, Sprinting, ref offset);
            SetBits(bits, 1, Attacking, ref offset);
            SetBits(bits, 2, Direction, ref offset);
            SetBits(bits, 2, AttackType, ref offset);
            SetBits(bits, 1, X >= 0, ref offset);
            SetBits(bits, 15, (X < 0) ? Math.Abs(X + 1) : Math.Abs(X), ref offset);
            SetBits(bits, 1, Y >= 0, ref offset);
            SetBits(bits, 15, (Y < 0) ? Math.Abs(Y + 1) : Math.Abs(Y), ref offset);

            return GetInt(bits);
        }

        private void SetBits(BitArray array, int size, object value, ref int offset)
        {
            var str = Convert.ToString(Convert.ToInt32(value), 2);
            str = str.PadLeft(size, '0');

            bool[] bits = str.Select(c => c == '1').ToArray();

            for (int i = 0; i < bits.Length; i++)
                array.Set(offset + i, bits[i]);

            offset += bits.Length;
        }

        private T ReadBits<T>(BitArray array, int size, ref int offset)
        {
            bool[] res = new bool[size];

            for (int i = 0; i < size; i++)
                res[i] = array[i + offset];

            int result = Convert.ToInt32(string.Join("", res.Select(b => b ? "1" : "0")), 2);
            offset += size;

            if (typeof(T).IsEnum)
                return (T)(object)result;
            else
                return (T)Convert.ChangeType(result, typeof(T));
        }

        private static long GetInt(BitArray array)
        {
            var builder = new StringBuilder();

            foreach (bool b in array)
                builder.Insert(0, b ? "1" : "0");

            return Convert.ToInt64(builder.ToString(), 2);
        }

        private static BitArray GetBitArray(long value)
        {
            var res = new BitArray(64);

            var boolArr = Convert.ToString(value, 2).Select(c => c == '1').Reverse().ToArray();

            for (int i = 0; i < boolArr.Length; i++)
                res[i] = boolArr[i];

            return res;
        }

        public void Load(Int64 value)
        {
            int offset = 0;

            var bits = GetBitArray(value); // new long[] { value });

            ID = ReadBits<byte>(bits, 8, ref offset);
            Health = ReadBits<byte>(bits, 8, ref offset);
            Energy = ReadBits<byte>(bits, 8, ref offset);
            Moving = ReadBits<bool>(bits, 1, ref offset);
            Jumping = ReadBits<bool>(bits, 1, ref offset);
            Sprinting = ReadBits<bool>(bits, 1, ref offset);
            Attacking = ReadBits<bool>(bits, 1, ref offset);
            Direction = ReadBits<Direction>(bits, 2, ref offset);
            AttackType = ReadBits<AttackType>(bits, 2, ref offset);
            var xsign = ReadBits<bool>(bits, 1, ref offset);
            X = ReadBits<short>(bits, 15, ref offset);
            var ysign = ReadBits<bool>(bits, 1, ref offset);
            Y = ReadBits<short>(bits, 15, ref offset);

            if (!xsign)
            {
                X = (short)(X * -1);
                X--;
            }
            if (!ysign)
            {
                Y = (short)(Y * -1);
                Y--;
            }
        }
    }
}