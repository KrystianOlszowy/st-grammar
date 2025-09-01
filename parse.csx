#load "stLexer.cs"
#load "stParser.cs"
#load "stBaseVisitor.cs"
#load "stVisitor.cs"

// Klasy reprezentujące elementy drzewa składniowego oraz ich odwiedzanie
#load "stEntities.cs"
#load "stTreeBuilder.cs"
//#load "astPrinter.cs"

#r "nuget: Antlr4.Runtime.Standard, 4.13.0"

using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

public class CaseInsensitiveInputStream : AntlrInputStream
{
    public CaseInsensitiveInputStream(string input) : base(input) { }

    public override int LA(int i)
    {
        int c = base.LA(i);
        if (c <= 0)
            return c;
        return Char.ToUpperInvariant((char)c);
    }
}

var filePath = "test.st"; 

if (!File.Exists(filePath))
{
    Console.WriteLine($" Plik '{filePath}' nie istnieje.");
    return;
}


//var inputCode = File.ReadAllText(filePath);

// Prosty kod do testów budowy drzewa
var inputCode = @"
PROGRAM Main
    VAR 
        x : ARRAY[1..2, 3..4] OF INT := [[1, 2], [3, 4]];
    END_VAR

END_PROGRAM
";

// Parsowanie
var inputStream = new CaseInsensitiveInputStream(inputCode);
var lexer = new stLexer(inputStream);
var tokens = new CommonTokenStream(lexer);
var parser = new stParser(tokens);

// Wypisanie drzewa gramatyki w konsoli i do pliku .dot
var tree = parser.file();


//Budowa drzewa składniowego
var treeBuilder = new STTreeBuilder();
var file = (STFile)treeBuilder.Visit(tree);

//var printer = new ASTPrinter();
//printer.Print(file);

var dot = ToDot(tree, parser);
File.WriteAllText("tree.dot", dot);
Console.WriteLine("Zapisano drzewo składni do tree.dot");

Console.WriteLine("Drzewo składniowe pliku test.st: \n" + tree.ToStringTree(parser));

// Funkcja budująca plik .dot do wizualizacji drzewa przy pomocy Graphviz
string ToDot(IParseTree tree, Parser parser)
{
    int id = 0;
    var sb = new System.Text.StringBuilder();
    sb.AppendLine("digraph ParseTree {");
    sb.AppendLine("node [shape=box, style=filled, color=\".7 .3 1.0\"];");

    void Walk(IParseTree node, int parentId)
    {
        int currentId = id++;
        string label = node is TerminalNodeImpl t
            ? t.Symbol.Text.Replace("\"", "\\\"")
            : parser.RuleNames[((RuleContext)node).RuleIndex];
        sb.AppendLine($"node{currentId} [label=\"{label}\"];");
        if (parentId != -1)
            sb.AppendLine($"node{parentId} -> node{currentId};");

        for (int i = 0; i < node.ChildCount; i++)
        {
            Walk(node.GetChild(i), currentId);
        }
    }

    Walk(tree, -1);

    sb.AppendLine("}");
    return sb.ToString();
}