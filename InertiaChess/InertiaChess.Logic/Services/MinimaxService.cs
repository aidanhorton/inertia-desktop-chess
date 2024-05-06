using InertiaChess.Core.Enums;
using InertiaChess.Logic.DataTypes;
using System;
using System.Threading.Tasks;

namespace InertiaChess.Logic.Services
{
    public class MinimaxService : IMinimaxService
    {
        private PieceType blackPieces = PieceType.BlackKing | PieceType.BlackQueen | PieceType.BlackRook | PieceType.BlackBishop | PieceType.BlackKnight | PieceType.BlackPawn;
        private PieceType whitePieces = PieceType.WhiteKing | PieceType.WhiteQueen | PieceType.WhiteRook | PieceType.WhiteBishop | PieceType.WhiteKnight | PieceType.WhitePawn;

        public async Task<Move> CalculateMove(PieceType[] pieces)
        {
            await Task.Delay(500);

            // Calculate move here.

            return await CalculateRandomMove(pieces);
        }

        private async Task<Move> CalculateRandomMove(PieceType[] pieces)
        {
            var random = new Random();

            int startingLocation = 0;
            var startingPiece = PieceType.None;
            while (!blackPieces.HasFlag(startingPiece))
            {
                startingLocation = random.Next(pieces.Length);
                startingPiece = pieces[startingLocation];
            }

            int endLocation = 0;
            var endPiece = PieceType.BlackKing;
            while (!whitePieces.HasFlag(endPiece) || endPiece != PieceType.None)
            {
                endLocation = random.Next(pieces.Length);
                endPiece = pieces[endLocation];
            }

            return new Move(startingLocation % 8, startingLocation / 8, endLocation % 8, endLocation / 8);
        }

        private async Task<Move> MiniMax()
        {


            return new Move(0, 0, 5, 5);
        }
    }
}
