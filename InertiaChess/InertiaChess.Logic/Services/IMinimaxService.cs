using InertiaChess.Core.Enums;
using InertiaChess.Logic.DataTypes;
using System.Threading.Tasks;

namespace InertiaChess.Logic.Services
{
    public interface IMinimaxService
    {
        Task<Move> CalculateMove(PieceType[] pieces);
    }
}
