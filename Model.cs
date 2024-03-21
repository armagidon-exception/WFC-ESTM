namespace WFC
{
    public class Model
    {
        public readonly List<char>[,] matrix;
        public readonly Wavefunction wavefunction;
        public readonly (int w, int h) size;

        public Model(Dictionary<char, int> Weights, HashSet<Rule> Rules, int outputWidth, int outputHeight)
        {
            matrix = new List<char>[outputHeight, outputWidth];
            for (int i = 0; i < outputHeight; i++)
            {
                for (int j = 0; j < outputWidth; j++)
                {
                    matrix[i, j] = new List<char>(Weights.Keys);
                }
            }
            size = (outputWidth, outputHeight);
            wavefunction = new Wavefunction(matrix, Weights, Rules, size);
        }

        public bool isFullyCollapsed()
        {
            foreach (var cell in matrix)
            {
                if (cell.Count > 1)
                {
                    return false;
                }
            }
            return true;
        }


        private bool isCellCollapsed(Point p)
        {
            return wavefunction.getAt(p).Count <= 1;
        }

        public Point findMinEntropyCell()
        {
            Random random = new Random();
            double minEntropy = Double.PositiveInfinity;
            Point point = new Point(-1, -1);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Point currentPoint = new Point(j, i);
                    if (isCellCollapsed(currentPoint))
                        continue;

                    double entropy = wavefunction.calcEntropyForCell(currentPoint);
                    double noise = (random.NextSingle() - 0.5) / 1000;
                    if (entropy - noise < minEntropy)
                    {
                        minEntropy = entropy;
                        point = currentPoint;
                    }
                }
            }
            return point;
        }

        public string[] Render()
        {
            string[] lines = new string[size.h];
            for (int i = 0; i < size.h; i++)
            {
                lines[i] = "";
                for (int j = 0; j < size.w; j++) {
                    lines[i] += matrix[i,j][0];
                }
            }
            return lines;
        }
    }
}
