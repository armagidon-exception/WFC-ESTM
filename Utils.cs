namespace WFC
{
    public class Utils
    {
        private Utils() { }

        public static Point GetAdjacent(Point point, Direction d)
        {
            (int x, int y) = point;
            switch (d)
            {
                case Direction.SOUTH:
                    return new Point(x, y + 1);
                case Direction.NORTH:
                    return new Point(x, y - 1);
                case Direction.WEST:
                    return new Point(x - 1, y);
                case Direction.EAST:
                    return new Point(x + 1, y);
                default:
                    throw new ArgumentException();
            }
        }


        public static List<Direction> CellSides(Point point, (int width, int height) size)
        {

            List<Direction> dirs = new List<Direction>();
            (int x, int y) = point;
            if (x > 0)
            {
                dirs.Add(Direction.WEST);
            }
            if (x < size.width - 1)
            {
                dirs.Add(Direction.EAST);
            }
            if (y > 0)
            {
                dirs.Add(Direction.NORTH);
            }
            if (y < size.height - 1)
            {
                dirs.Add(Direction.SOUTH);
            }
            return dirs;
        }

        public static void printTiles(string[] lines, Dictionary<char, string> colorMappings)
        {

            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    Console.Write(colorMappings[line[i]] + "█");
                }
                Console.WriteLine();
            }
        }

    }
}
