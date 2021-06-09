using InertiaChess.Core.Enums;
using InertiaChess.Logic.Services;
using InertiaChess.Presentation.ItemTypes;

namespace InertiaChess.Presentation.Factories
{
    public class BoardTileFactory : IBoardTileFactory
    {
        private readonly IPieceService pieceService;

        public BoardTileFactory(IPieceService pieceService)
        {
            this.pieceService = pieceService;
        }

        public BoardTile CreateTile(TileType tileType)
        {
            return new BoardTile(tileType, this.pieceService);
        }
    }
}
