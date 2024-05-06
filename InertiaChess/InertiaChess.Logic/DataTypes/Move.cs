namespace InertiaChess.Logic.DataTypes
{
    public class Move
    {
        public Move(int startX, int startY, int endX, int endY)
        {
            this.StartX = startX;
            this.StartX = startX;
            this.EndX = endX;
            this.EndY = endY;
        }

        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
    }
}
