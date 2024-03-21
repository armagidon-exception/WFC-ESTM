namespace WFC {
    public class Wavefunction {
        private List<char>[,] Matrix;
        private Dictionary<char, int> Weights;
        private HashSet<Rule> Rules;
        private (int w, int h) OutputSize;

        public Wavefunction(List<char>[,] matrix, Dictionary<char, int> weights, HashSet<Rule> rules, (int w, int h) size)
        {
            Matrix = matrix;
            Weights = weights;
            Rules = rules;
            OutputSize = size;
        }

        public List<char> getAt(Point point) {
            return Matrix[point.Y, point.X];
        }


        public void Propagate(Point point) {
            Queue<Point> propagationQueue = new Queue<Point>(1);
            propagationQueue.Enqueue(point);
            while (propagationQueue.Count > 0) {
                Point current = propagationQueue.Dequeue();

                foreach (Direction currentDir in Utils.CellSides(current, OutputSize)) {
                    Point adjacentPoint = Utils.GetAdjacent(current, currentDir);
                    foreach (char adjacentState in getAt(adjacentPoint).ToArray()) {
                        bool compat = false;
                        foreach(char currentState in getAt(current)) {
                            if (hasRule(currentState, adjacentState, currentDir)) {
                                compat = true;
                                break;
                            }
                        }
                        if (!compat) {
                            getAt(adjacentPoint).Remove(adjacentState);
                            propagationQueue.Enqueue(adjacentPoint);
                        }
                    }
                }
            }
        }


        public bool hasRule(char current, char adjacent, Direction dir) {
            Rule pattern = new Rule(current, adjacent, dir);
            foreach(Rule rule in Rules) {
                if (rule.Equals(pattern))
                    return true;
            }
            return false;
        }

        public void Collapse(Point point) {
            int totalWeight = 0;
            Random random = new Random();
            var cell = getAt(point);
            foreach(char state in cell) {
                totalWeight += Weights[state];
            }
            double rnd = random.NextDouble() * totalWeight;
            foreach (char state in cell) {
                rnd -= Weights[state];
                if (rnd < 0) {
                    cell.Clear();
                    cell.Add(state);
                    return;
                }
            }
        }

        public double calcEntropyForCell(Point point) {
            int totalWeights = 0;
            double totalWeightLogWeight = 0;
            foreach (char state in getAt(point)) {
                int weight = Weights[state];
                totalWeights += weight;
                totalWeightLogWeight += weight * Math.Log2(weight);
            }

            return Math.Log2(totalWeights) - totalWeightLogWeight / totalWeights;
        }
    }
}
