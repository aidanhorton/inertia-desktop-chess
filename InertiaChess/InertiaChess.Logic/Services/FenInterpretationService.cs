using InertiaChess.Core.Enums;
using System;
using System.Collections.Generic;

namespace InertiaChess.Logic.Services
{
    public class FenInterpretationService : IFenInterpretationService
    {
        public PieceType[] Interpret(string data)
        {
            // FEN is always from whites perspective.
            var fenSections = data.Split(' ');

            return this.InterpretPieceSection(fenSections[0]);
        }

        private PieceType[] InterpretPieceSection(string pieceData)
        {
            var rows = pieceData.Split('/');

            var pieces = new PieceType[64];
            for (var rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                var currentColumn = 0;
                for (var charIndex = 0; charIndex < rows[rowIndex].Length; charIndex++)
                {
                    var pieceRepresentation = rows[rowIndex][charIndex];

                    if (this.pieceMap.TryGetValue(pieceRepresentation, out var piece))
                    {
                        pieces[rowIndex * 8 + currentColumn] = piece;
                        currentColumn++;

                        continue;
                    }

                    if (int.TryParse(pieceRepresentation.ToString(), out var emptySpaces))
                    {
                        for (var i = 0; i < emptySpaces; i++)
                        {
                            pieces[rowIndex * 8 + currentColumn] = PieceType.None;
                            currentColumn++;
                        }
                    }
                    else
                    {
                        throw new Exception($"Invalid piece detected during FEN interpretation - {pieceRepresentation}");
                    }
                }
            }

            return pieces;
        }

        private readonly Dictionary<char, PieceType> pieceMap = new Dictionary<char, PieceType>()
        {
            { 'k', PieceType.BlackKing },
            { 'q', PieceType.BlackQueen },
            { 'r', PieceType.BlackRook },
            { 'b', PieceType.BlackBishop },
            { 'n', PieceType.BlackKnight },
            { 'p', PieceType.BlackPawn },

            { 'K', PieceType.WhiteKing },
            { 'Q', PieceType.WhiteQueen },
            { 'R', PieceType.WhiteRook },
            { 'B', PieceType.WhiteBishop },
            { 'N', PieceType.WhiteKnight },
            { 'P', PieceType.WhitePawn }
        };
    }
}
