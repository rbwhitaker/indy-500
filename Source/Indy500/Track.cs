namespace Indy500
{
    public class Track
    {
        private TrackTileType[,] tiles;

        public Track(int rows, int columns)
        {
            tiles = new TrackTileType[rows, columns];
        }

        public TrackTileType this[int row, int column]
        {
            get => tiles[(row + Rows) % Rows, (column + Columns) % Columns];
            set => tiles[(row + Rows) % Rows, (column + Columns) % Columns] = value;
        }

        public int Rows => tiles.GetLength(0);
        public int Columns => tiles.GetLength(1);
    }
}
