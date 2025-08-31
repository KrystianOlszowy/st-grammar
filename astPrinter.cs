public class ASTPrinter
{
    public void Print(STEntity node, int indent = 0)
    {
        if (node == null)
            return;

        string indentStr = new string(' ', indent * 2);

        Console.WriteLine($"{indentStr}{FormatNode(node)}");

        switch (node)
        {
            case STFile file:
                foreach (var decl in file.Declarations)
                    Print(decl, indent + 1);
                break;

            case STNamespace ns:
                foreach (var member in ns.Members)
                    Print(member, indent + 1);
                break;

            case STPou pou:
                foreach (var v in pou.Variables)
                    Print(v, indent + 1);
                foreach (var stmt in pou.Body)
                    Print(stmt, indent + 1);
                break;

            case STVariable varDecl:
                if (varDecl.InitialValue != null)
                    Print(varDecl.InitialValue, indent + 1);
                break;

            case STAssignment assign:
                Print(assign.Target, indent + 1);
                Print(assign.Value, indent + 1);
                break;

            case STFunctionCallStatement callStmt:
                Print(callStmt.FunctionCall, indent + 1);
                break;

            case STIf ifStmt:
                Print(ifStmt.Condition, indent + 1);
                foreach (var stmt in ifStmt.ThenBranch)
                    Print(stmt, indent + 1);

                foreach (var (cond, body) in ifStmt.ElseIfBranches)
                {
                    Console.WriteLine($"{indentStr}  ElseIfBranch");
                    Print(cond, indent + 2);
                    foreach (var stmt in body)
                        Print(stmt, indent + 2);
                }

                if (ifStmt.ElseBranch != null)
                {
                    Console.WriteLine($"{indentStr} ElseBranch");
                    foreach (var stmt in ifStmt.ElseBranch)
                        Print(stmt, indent + 2);
                }
                break;

            case STCase caseStmt:
                Print(caseStmt.Selector, indent + 1);
                foreach (var sel in caseStmt.Selections)
                {
                    Print(sel, indent + 1);
                }
                foreach (var stmt in caseStmt.ElseBranch)
                    Print(stmt, indent + 1);
                break;

            case STCaseSelection sel:
                foreach (var lbl in sel.Labels)
                    Print(lbl, indent + 1);
                foreach (var stmt in sel.Body)
                    Print(stmt, indent + 1);
                break;

            case STCaseExpressionLabel exprLabel:
                Print(exprLabel.Expression, indent + 1);
                break;

            case STCaseRangeLabel rangeLabel:
                Print(rangeLabel.From, indent + 1);
                Print(rangeLabel.To, indent + 1);
                break;

            case STFor forStmt:
                Print(forStmt.From, indent + 1);
                Print(forStmt.To, indent + 1);
                if (forStmt.By != null)
                    Print(forStmt.By, indent + 1);
                foreach (var stmt in forStmt.Body)
                    Print(stmt, indent + 1);
                break;

            case STWhile whileStmt:
                Print(whileStmt.Condition, indent + 1);
                foreach (var stmt in whileStmt.Body)
                    Print(stmt, indent + 1);
                break;

            case STRepeat repeatStmt:
                foreach (var stmt in repeatStmt.Body)
                    Print(stmt, indent + 1);
                Print(repeatStmt.Until, indent + 1);
                break;

            case STUnaryExpression unary:
                Print(unary.Operand, indent + 1);
                break;

            case STBinaryExpression binary:
                Print(binary.Left, indent + 1);
                Print(binary.Right, indent + 1);
                break;

            case STVariableAccess varAccess:
                foreach (var sel in varAccess.Selectors)
                    Print(sel, indent + 1);
                break;

            case STFunctionCall call:
                foreach (var param in call.Parameters)
                    Print(param, indent + 1);
                break;

            case STPouParameter param:
                Print(param.Value, indent + 1);
                break;

            case STDereference deref:
                Print(deref.Operand, indent + 1);
                break;

            case STIndexSelector idxSel:
                foreach (var idx in idxSel.Indexes)
                    Print(idx, indent + 1);
                break;
            default:
                break;
        }
    }

    private static string FormatNode(STEntity node)
    {
        return node switch
        {
            
            STVariable v => $"{node.GetType().Name} (Name={v.Name}, Type={v.Type})",
            STDeclaration d => $"{node.GetType().Name} (Name={d.Name})",
            STAssignment a => $"{node.GetType().Name} (Operator={a.Operator})",
            STBinaryExpression b => $"{node.GetType().Name} (Operator={b.Operator})",
            STUnaryExpression u => $"{node.GetType().Name} (Operator={u.Operator})",
            STLiteral l => $"{node.GetType().Name} (Value={l.Value})",
            STVariableAccess v => $"{node.GetType().Name} (Name={v.Name})",
            STFunctionCall f => $"{node.GetType().Name} (Name={f.Name})",
            STPouParameter p => $"{node.GetType().Name} (Name={p.Name}, IsOutput={p.IsOutput}, IsNegated={p.IsNegated})",
            STEnumValue e => $"{node.GetType().Name} ({e.TypeName}.{e.ElementName})",
            STFieldSelector f => $"{node.GetType().Name} (FieldName={f.FieldName})",
            STFor f => $"{node.GetType().Name} (Iterator={f.Iterator})",
            STCaseRangeLabel r => $"{node.GetType().Name} (Range)",
            _ => node.GetType().Name
        };
    }
}
