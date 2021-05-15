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

        private Brush tileColor;
        private bool isTileSelected;

        public BoardTile(Brush tileColor)
        {
            this.TileColor = tileColor;

            this.TilePressedCommand = new DelegateCommand(this.OnTilePressed);
        }

        public ICommand TilePressedCommand { get; }

        public Brush TileColor 
        {
            get => this.tileColor;
            set => this.SetProperty(ref this.tileColor, value);
        }

        public bool IsTileSelected
        {
            get => this.isTileSelected;
            set => this.SetProperty(ref this.isTileSelected, value);
        }

        private void OnTilePressed()
        {
            this.IsTileSelected = true;
            this.TilePressedEvent?.Invoke(this);
        }
    }
}
