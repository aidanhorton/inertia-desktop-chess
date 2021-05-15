using InertiaChess.Presentation.Enums;
using InertiaChess.Presentation.ItemTypes;
using Prism.Mvvm;
using System.Collections.Generic;

namespace InertiaChess.Presentation.ViewModels
{
    public class ChessBoardViewModel : BindableBase
    {
        public ChessBoardViewModel()
        {
            this.CreateTiles();
        }

        public IList<BoardTile> Tiles { get; } = new List<BoardTile>();

        private void CreateTiles()
        {
            for (var i = 0; i < 64; i++)
            {
                var tile = new BoardTile(i % 2 == (i / 8) % 2 ? TileType.Light : TileType.Dark);
                this.Tiles.Add(tile);

                tile.TilePressedEvent += this.TilePressed;
            }
        }

        private void TilePressed(BoardTile pressedTile)
        {
            foreach (var tile in this.Tiles)
            {
                if (tile != pressedTile)
                {
                    tile.IsTileSelected = false;
                }
            }
        }
    }
}
