using System;

namespace InertiaChess.Core.Enums
{
    [Flags]
    public enum PieceType
    {
        WhiteKing = 1,
        WhiteQueen = 2,
        WhiteRook = 4,
        WhiteBishop = 8,
        WhiteKnight = 16,
        WhitePawn = 32,
        BlackKing = 64,
        BlackQueen = 128,
        BlackRook= 256,
        BlackBishop= 512,
        BlackKnight = 1024,
        BlackPawn = 2048,
        None = 4096
    }
}
