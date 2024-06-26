﻿using InertiaChess.Core.Enums;

namespace InertiaChess.Logic.Services
{
    public class PieceService : IPieceService
    {
        public string GetImagePathFromPieceType(PieceType pieceType)
        {
            if (pieceType == PieceType.None) return string.Empty;

            return $"../Images/{pieceType}.png";
        }
    }
}
