using InertiaChess.Core.Enums;
using InertiaChess.Logic.Services;
using InertiaChess.Presentation.Factories;
using InertiaChess.Presentation.ItemTypes;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InertiaChess.Presentation.ViewModels
{
    public class ChessBoardViewModel : BindableBase
    {
        private readonly IMinimaxService minimaxService;
        private readonly IBoardTileFactory boardTileFactory;
        private readonly IFenInterpretationService interpreter;

        private BoardTile pressedTile;

        private PieceType blackPieces = PieceType.BlackKing | PieceType.BlackQueen | PieceType.BlackRook | PieceType.BlackBishop | PieceType.BlackKnight | PieceType.BlackPawn;
        private PieceType whitePieces = PieceType.WhiteKing | PieceType.WhiteQueen | PieceType.WhiteRook | PieceType.WhiteBishop | PieceType.WhiteKnight | PieceType.WhitePawn;

        private bool canSelectPieces = true;

        public ChessBoardViewModel(IMinimaxService minimaxService, IBoardTileFactory boardTileFactory, IFenInterpretationService fenInterpreter)
        {
            this.minimaxService = minimaxService;
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

                tile.TilePressedEvent += (eventArgs) => Task.Run(() => this.TilePressed(eventArgs));
            }
        }

        private async Task TilePressed(BoardTile newPressedTile)
        {
            if (!this.canSelectPieces)
            {
                newPressedTile.IsTileSelected = false;
                return;
            }

            if (this.whitePieces.HasFlag(newPressedTile.PieceType))
            {
                this.pressedTile = newPressedTile;

                this.DeselectNonPressedTiles();
            }
            else if ((newPressedTile.PieceType == PieceType.None || this.blackPieces.HasFlag(newPressedTile.PieceType)) && this.pressedTile != null)
            {
                // Potentially valid move.
                this.canSelectPieces = false;

                await this.HandleRequestedMove(this.pressedTile, newPressedTile);

                this.pressedTile = null;
                this.canSelectPieces = true;
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

        private async Task HandleRequestedMove(BoardTile startingLocation, BoardTile destination)
        {
            destination.PieceType = startingLocation.PieceType;
            startingLocation.PieceType = PieceType.None;

            await this.minimaxService.CalculateMove(this.LightweightBoardRepresentation());

            // Perform move here. Do I need to perform any checks?
        }

        private PieceType[] LightweightBoardRepresentation()
        {
            return this.Tiles.Select(tile => tile.PieceType).ToArray();
        }
    }
}
