#load "stLexer.cs"
#load "stListener.cs"
#load "stParser.cs"
#load "stVisitor.cs"

#r "nuget: Antlr4.Runtime.Standard, 4.13.0"

using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.IO;

public class CaseInsensitiveInputStream : AntlrInputStream
{
    public CaseInsensitiveInputStream(string input) : base(input.ToUpperInvariant()) { }
}

var filePath = "test.st"; 

if (!File.Exists(filePath))
{
    Console.WriteLine($" Plik '{filePath}' nie istnieje.");
    return;
}

var inputCode = File.ReadAllText(filePath);

// Parsowanie
var inputStream = new CaseInsensitiveInputStream(inputCode);
var lexer = new stLexer(inputStream);
var tokens = new CommonTokenStream(lexer);
var parser = new stParser(tokens);

// Wypisanie drzewa gramatyki w konsoli i do pliku .dot
var tree = parser.program();


var dot = ToDot(tree, parser);
File.WriteAllText("tree.dot", dot);
Console.WriteLine("Zapisano drzewo składni do tree.dot");

Console.WriteLine("Drzewo składniowe pliku test.st: \n" + tree.ToStringTree(parser));

//funkcja budująca plik .dot do wizualizacji drzewa przy pomocy Graphviz
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