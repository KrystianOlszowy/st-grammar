public static class ASTPrinter
{
    public static void PrintAST(STEntity node, int indent = 0)
    {
        if (node == null)
        {
            Console.WriteLine(new string(' ', indent * 2) + "(null)");
            return;
        }

        var indentStr = new string(' ', indent * 2);
        var type = node.GetType();
        var props = type.GetProperties();

        var nonEmptyProps = props
            .Where(p =>
            {
                var val = p.GetValue(node);

                if (val == null) return false;
                if (val is string s && string.IsNullOrEmpty(s)) return false;
                if (val is System.Collections.ICollection c && c.Count == 0) return false;
                if (val is bool b && b == false) return false;

                return true;
            })
            .ToList();

        if (nonEmptyProps.Count == 0)
        {
            Console.WriteLine($"{indentStr}{type.Name}");
        }
        else
        {
            Console.WriteLine($"{indentStr}{type.Name}:");
            foreach (var prop in nonEmptyProps)
            {
                var val = prop.GetValue(node);

                if (val is STEntity child)
                {
                    Console.WriteLine($"{indentStr}  {prop.Name}:");
                    PrintAST(child, indent + 2);
                }
                else if (val is IEnumerable<STEntity> list)
                {
                    Console.WriteLine($"{indentStr}  {prop.Name} [{list.Cast<object>().Count()}]");
                    foreach (var item in list)
                        PrintAST(item, indent + 2);
                }
                else
                {
                    Console.WriteLine($"{indentStr}  {prop.Name} = {val}");
                }
            }
        }
    }

}