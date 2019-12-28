namespace Indy500
{
    public class GridCell
    {
        public int Row { get; }
        public int Column { get; }

        public GridCell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
