using InertiaChess.Core.Enums;

namespace InertiaChess.Logic.Services
{
    public interface IFenInterpretationService
    {
        PieceType[] Interpret(string data);
    }
}
