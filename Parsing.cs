namespace WFC
{
    public class WFCParser
    {
        public static void ParseFile(string filename, out Dictionary<char, int> Weights, out HashSet<Rule> Rules)
        {
            string[] lines = File.ReadAllLines(filename);
            Weights = new Dictionary<char, int>();
            Rules = new HashSet<Rule>();
            int inputHeight = lines.Length;
            int inputWidth = lines[0].Length;
            for (int i = 0; i < inputHeight; i++)
            {
                for (int j = 0; j < inputWidth; j++)
                {
                    char cursor = lines[i][j];
                    if (!Weights.ContainsKey(cursor))
                    {
                        Weights.Add(cursor, 0);
                    }
                    Weights[cursor]++;
                    if (i < inputHeight - 1)
                    {
                        Rules.Add(new Rule(cursor, lines[i + 1][j], Direction.SOUTH));
                        Rules.Add(new Rule(lines[i + 1][j], cursor, Direction.NORTH));
                    }
                    if (j < inputWidth - 1)
                    {
                        Rules.Add(new Rule(cursor, lines[i][j + 1], Direction.EAST));
                        Rules.Add(new Rule(lines[i][j + 1], cursor, Direction.WEST));
                    }
                }
            }
        }
    }
}
