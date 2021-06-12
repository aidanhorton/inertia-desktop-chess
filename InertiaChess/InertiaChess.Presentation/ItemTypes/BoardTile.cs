using InertiaChess.Core.Enums;
using InertiaChess.Logic.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using System.Windows.Media;

namespace InertiaChess.Presentation.ItemTypes
{
    public delegate void TilePressed(BoardTile pressedTile);

    public class BoardTile : BindableBase
    {
        public event TilePressed TilePressedEvent;

        private readonly Brush lightColor = new SolidColorBrush(Color.FromRgb(232, 235, 239));
        private readonly Brush darkColor = new SolidColorBrush(Color.FromRgb(125, 135, 150));
        private readonly Brush lightHoverColor = new SolidColorBrush(Color.FromRgb(240, 255, 220));
        private readonly Brush darkHoverColor = new SolidColorBrush(Color.FromRgb(135, 155, 130));
        private readonly Brush lightSelectionColor = new SolidColorBrush(Color.FromRgb(225, 255, 170));
        private readonly Brush darkSelectionColor = new SolidColorBrush(Color.FromRgb(110, 155, 100));

        private readonly IPieceService pieceService;

        private PieceType pieceType;
        private string pieceImage;
        private Brush tileColor;
        private Brush hoverColor;
        private Brush selectionColor;
        private bool isTileSelected;

        public BoardTile(TileType tileType, PieceType startingPiece, IPieceService pieceService)
        {
            this.TileColor = tileType == TileType.Light ? lightColor : darkColor;
            this.HoverColor = tileType == TileType.Light ? lightHoverColor : darkHoverColor;
            this.SelectionColor = tileType == TileType.Light ? lightSelectionColor : darkSelectionColor;

            this.pieceService = pieceService;

            this.PieceType = startingPiece;
            this.PieceImage = this.pieceService.GetImagePathFromPieceType(startingPiece);

            this.TilePressedCommand = new DelegateCommand(this.TilePressed);
        }

        public ICommand TilePressedCommand { get; }

        public PieceType PieceType 
        {
            get => this.pieceType;
            set 
            {
                this.pieceType = value;

                if (this.pieceType == PieceType.None)
                {
                    this.PieceImage = null;
                    return;
                }

                this.UpdatePiece();
            }
        }

        public string PieceImage
        {
            get => this.pieceImage;
            set => this.SetProperty(ref this.pieceImage, value);
        }

        public Brush TileColor 
        {
            get => this.tileColor;
            set => this.SetProperty(ref this.tileColor, value);
        }

        public Brush HoverColor
        {
            get => this.hoverColor;
            set => this.SetProperty(ref this.hoverColor, value);
        }

        public Brush SelectionColor 
        {
            get => this.selectionColor;
            set => this.SetProperty(ref this.selectionColor, value);
        }

        public bool IsTileSelected
        {
            get => this.isTileSelected;
            set => this.SetProperty(ref this.isTileSelected, value);
        }

        private void TilePressed()
        {
            this.TilePressedEvent?.Invoke(this);
        }

        private void UpdatePiece()
        {
            this.PieceImage = this.pieceService.GetImagePathFromPieceType(this.PieceType);
        }
    }
}
