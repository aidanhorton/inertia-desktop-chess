using InertiaChess.Core.Enums;

namespace InertiaChess.Logic.Services
{
    public interface IPieceService
    {
        string GetImagePathFromPieceType(PieceType pieceType, bool isWhite);
    }
}
