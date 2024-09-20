using System;
using System.Collections.Generic;
using System.Linq;
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

    class Token
    {
        public TokenType Type { get; }
        public ReadOnlyMemory<char> Value { get; }

        public Token(TokenType type, ReadOnlyMemory<char> value)
        {
            Type = type;
            Value = value;
        }
    }
}

