namespace FEV
{
    public class GridFactory
    {
        public Cell[,] CreateGrid(int width, int height)
        {
            var cells = new Cell[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }
            return cells;
        }
    }
}