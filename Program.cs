using WFC;

string filename = "bebra.txt";

Dictionary<char, string> maps = new Dictionary<char, string>();
maps.Add('S', "\x1b[94m");
maps.Add('L', "\x1b[32m");
maps.Add('C', "\x1b[93m");

Dictionary<char, int> weights;
HashSet<Rule> rules;
Console.WriteLine("Wave Function Collapse Even Simpler Tiled Model");
Console.WriteLine("Input:");
Utils.printTiles(File.ReadAllLines(filename), maps);

Console.WriteLine("Parsing....");
WFCParser.ParseFile(filename, out weights, out rules);

Console.WriteLine("Rendering....");
for (int number = 0; number < 10; number++) {
    Console.WriteLine($"Sample #{number + 1}");
    Model model = new Model(weights, rules, 50, 10);

    while(!model.isFullyCollapsed()) {
        Point point = model.findMinEntropyCell();

        model.wavefunction.Collapse(point);
        model.wavefunction.Propagate(point);
    }


    Utils.printTiles(model.Render(), maps);
}
