using InertiaChess.Core.Enums;
using InertiaChess.Logic.DataTypes;
using System.Threading.Tasks;

namespace InertiaChess.Logic.Services
{
    public class MinimaxService : IMinimaxService
    {
        public async Task<Move> CalculateMove(PieceType[] pieces)
        {
            await Task.Delay(1000);

            // Calculate move here.

            return null;
        }
    }
}
