using InertiaChess.Core.Enums;
using InertiaChess.Presentation.ItemTypes;

namespace InertiaChess.Presentation.Factories
{
    public interface IBoardTileFactory
    {
        public BoardTile CreateTile(TileType tileType);
    }
}
