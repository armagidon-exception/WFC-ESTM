namespace WFC
{

    public enum Direction
    {
        SOUTH, NORTH, WEST, EAST
    }


    public record class Rule
    {
        public char First { get; init; }
        public char Second { get; init; }
        public Direction Direction { get; init; }

        public Rule(char first, char second, Direction direction)
        {
            First = first;
            Second = second;
            Direction = direction;
        }
    }


    public record class Point
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }
}
