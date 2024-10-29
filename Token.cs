using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum TokenType
    {
        Number,
        Operator,
        EOF
    }

    public readonly record struct Token(
        TokenType Type,
        ReadOnlyMemory<char> Value
    );
}

