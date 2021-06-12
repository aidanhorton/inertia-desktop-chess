using InertiaChess.Core.Enums;
using InertiaChess.Logic.Services;
using InertiaChess.Presentation.Factories;
using InertiaChess.Presentation.ItemTypes;
using Prism.Mvvm;
using System.Collections.Generic;

namespace InertiaChess.Presentation.ViewModels
{
    public class ChessBoardViewModel : BindableBase
    {
        private readonly IBoardTileFactory boardTileFactory;
        private readonly IFenInterpretationService interpreter;

        private BoardTile pressedTile;

        private PieceType blackPieces = PieceType.BlackKing | PieceType.BlackQueen | PieceType.BlackRook | PieceType.BlackBishop | PieceType.BlackKnight | PieceType.BlackPawn;
        private PieceType whitePieces = PieceType.WhiteKing | PieceType.WhiteQueen | PieceType.WhiteRook | PieceType.WhiteBishop | PieceType.WhiteKnight | PieceType.WhitePawn;

        public ChessBoardViewModel(IBoardTileFactory boardTileFactory, IFenInterpretationService fenInterpreter)
        {
            this.boardTileFactory = boardTileFactory;
            this.interpreter = fenInterpreter;

            this.CreateTiles();
        }

        public IList<BoardTile> Tiles { get; } = new List<BoardTile>();

        private void CreateTiles()
        {
            var pieces = this.interpreter.Interpret("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");

            for (var i = 0; i < 64; i++)
            {
                var tile = this.boardTileFactory.CreateTile(i % 2 == (i / 8) % 2 ? TileType.Light : TileType.Dark, pieces[i]);
                this.Tiles.Add(tile);

                tile.TilePressedEvent += this.TilePressed;
            }
        }

        private void TilePressed(BoardTile newPressedTile)
        {
            if (this.whitePieces.HasFlag(newPressedTile.PieceType))
            {
                this.pressedTile = newPressedTile;

                this.DeselectNonPressedTiles();
            }
            else if ((newPressedTile.PieceType == PieceType.None || this.blackPieces.HasFlag(newPressedTile.PieceType)) && this.pressedTile != null)
            {
                // Potentially valid move.

                newPressedTile.PieceType = this.pressedTile.PieceType;
                this.pressedTile.PieceType = PieceType.None;

                this.pressedTile = null;
            }
            else
            {
                this.pressedTile = null;

                this.DeselectNonPressedTiles();
            }            
        }

        private void DeselectNonPressedTiles()
        {
            foreach (var tile in this.Tiles)
            {
                if (tile != this.pressedTile)
                {
                    tile.IsTileSelected = false;
                }
                else
                {
                    tile.IsTileSelected = true;
                }
            }
        }
    }
}
