using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// https://www.codewars.com/kata/546d15cebed2e10334000ed9

public class Runes
{
    public static int solveExpression(string expression)
    {
        List<int> _opIndexes = new List<int>();
        HashSet<char> _blockedChars = new HashSet<char>();

        int index = 0;
        foreach (var c in expression)
        {
            if (_operationsFlags.Values.Contains(c))
                _opIndexes.Add(index);
            _blockedChars.Add(c);
            index++;
        }

        StringBuilder builder = new StringBuilder(expression);
        for (int i = 0; i < _opIndexes.Count - 1; i++)
        {
            if (_opIndexes[i] == 0)
                builder[_opIndexes[i]] = 'n';

            if (_opIndexes[i + 1] - _opIndexes[i] == 1)
                builder[_opIndexes[i + 1]] = 'n';
        }
        expression = builder.ToString();

        var head = new Node(expression);

        for (int i = head.IsZeroScipped ? 1 : 0; i < 10; i++)
        {
            var ch = i.ToString()[0];
            if (_blockedChars.Contains(ch))
                continue;

            if ((bool)head.Calculate(i))
                return i;
        }

        return -1;
    }

    public enum Operation
    {
        Add,
        Sub,
        Mult,
        Eq
    }

    private static Dictionary<Operation, char> _operationsFlags = new Dictionary<Operation, char>()
    {
        { Operation.Eq, '=' },
        { Operation.Mult, '*' },
        { Operation.Add, '+' },
        { Operation.Sub, '-' },
    };

    public class Node
    {
        public Node Left { get; set; } = null;
        public Node Right { get; set; } = null;

        public string Value { get; set; }
        public Operation Op { get; set; }

        public bool IsOperationNode { get; set; } = false;
        public bool IsZeroScipped { get; set; } = false;
        public bool IsValid { get; set; } = true;

        public Node(string val)
        {
            Value = val;
            BuildTree();
        }

        public void BuildTree()
        {
            if (Value.IndexOfAny(_operationsFlags.Values.ToArray()) == -1)
            {
                if (Value.First() == '?' && Value.Length > 1)
                    IsZeroScipped = true;

                return;
            }

            foreach (var pair in _operationsFlags)
            {
                var index = Value.LastIndexOf(pair.Value);
                if (index == -1)
                    continue;

                var left = Value.Substring(0, index);
                var right = Value.Substring(index + 1);

                if (pair.Key == Operation.Sub && string.IsNullOrEmpty(left))
                {
                    Value.Insert(0, "-");
                    break;
                }

                IsOperationNode = true;
                Op = pair.Key;

                Left = new Node(left);
                Right = new Node(right);

                if (Left.IsZeroScipped || Right.IsZeroScipped)
                    IsZeroScipped = true;

                break;
            }
        }

        public object Calculate(int currentNumber)
        {
            if (!IsOperationNode)
            {
                var str = Value;

                if (str.First() == 'n')
                    str = str.Replace('n', '-');

                return Convert.ToInt32(str.Replace("?", currentNumber.ToString()));
            }
            else
            {
                var leftV = Left.Calculate(currentNumber);
                var rightV = Right.Calculate(currentNumber);

                switch (Op)
                {
                    case Operation.Eq:
                        return leftV.Equals(rightV);
                    case Operation.Add:
                        return (int)leftV + (int)rightV;
                    case Operation.Mult:
                        return (int)leftV * (int)rightV;
                    case Operation.Sub:
                        return (int)leftV - (int)rightV;
                }
            }

            return null;
        }
    }
}