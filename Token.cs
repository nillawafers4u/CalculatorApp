using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    enum TokenType
    {
        Number,
        Operator,
        EOF
    }

    record struct Token
    {


        public readonly TokenType Type { get; }
        public readonly ReadOnlyMemory<char> Value { get; }

        public Token(TokenType type, ReadOnlyMemory<char> value)
        {
            Type = type;
            Value = value;
        }
    }
}

