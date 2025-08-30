using System;
using System.Collections.Generic;

public class STPrinter
{
    public void Print(STEntity entity, int indent = 0)
    {
        string pad = new string(' ', indent * 2);

        switch (entity)
        {
            case STFile file:
                Console.WriteLine($"{pad}File");
                foreach (var decl in file.Declarations)
                    Print(decl, indent + 1);
                break;

            case STNamespace ns:
                Console.WriteLine($"{pad}Namespace {ns.Name}");
                foreach (var member in ns.Members)
                    Print(member, indent + 1);
                break;

            case STProgram prog:
                Console.WriteLine($"{pad}Program {prog.Name}");
                PrintPou(prog, indent);
                break;

            case STFunction func:
                Console.WriteLine($"{pad}Function {func.Name}");
                PrintPou(func, indent);
                break;

            case STFunctionBlock fb:
                Console.WriteLine($"{pad}FunctionBlock {fb.Name}");
                PrintPou(fb, indent);
                break;

            case STVariable variable:
                Console.WriteLine($"{pad}Var {variable.Name} : {variable.Type}");
                if (variable.InitialValue != null)
                {
                    Console.WriteLine($"{pad}  Init:");
                    Print(variable.InitialValue, indent + 2);
                }
                break;

            case STAssignment assign:
                Console.WriteLine($"{pad}Assign ({assign.Operator})");
                Console.WriteLine($"{pad}  Target:");
                Print(assign.Target, indent + 2);
                Console.WriteLine($"{pad}  Value:");
                Print(assign.Value, indent + 2);
                break;

            case STFunctionCallStatement stmtCall:
                Console.WriteLine($"{pad}FunctionCallStmt");
                Print(stmtCall.Call, indent + 1);
                break;

            case STInvocation inv:
                Console.WriteLine($"{pad}Invocation");
                Print(inv.Target, indent + 1);
                foreach (var arg in inv.Arguments)
                    Print(arg, indent + 2);
                break;

            case STReturn ret:
                Console.WriteLine($"{pad}Return");
                Print(ret.Value, indent + 1);
                break;

            case STIf ifStmt:
                Console.WriteLine($"{pad}If");
                Console.WriteLine($"{pad}  Condition:");
                Print(ifStmt.Condition, indent + 2);
                Console.WriteLine($"{pad}  Then:");
                foreach (var stmt in ifStmt.ThenBranch)
                    Print(stmt, indent + 2);
                foreach (var (cond, body) in ifStmt.ElseIfBranches)
                {
                    Console.WriteLine($"{pad}  ElseIf:");
                    Print(cond, indent + 2);
                    foreach (var stmt in body)
                        Print(stmt, indent + 2);
                }
                if (ifStmt.ElseBranch.Count > 0)
                {
                    Console.WriteLine($"{pad}  Else:");
                    foreach (var stmt in ifStmt.ElseBranch)
                        Print(stmt, indent + 2);
                }
                break;

            case STLiteral lit:
                Console.WriteLine($"{pad}Literal {lit.Value}");
                break;

            case STQualifiedName qn:
                Console.WriteLine($"{pad}QualifiedName {qn}");
                break;

            case STFunctionCall call:
                Console.WriteLine($"{pad}FunctionCall {call.Target}");
                foreach (var arg in call.Arguments)
                    Print(arg, indent + 1);
                break;

            case STUnaryExpression un:
                Console.WriteLine($"{pad}Unary {un.Operator}");
                Print(un.Operand, indent + 1);
                break;

            case STBinaryExpression bin:
                Console.WriteLine($"{pad}Binary {bin.Operator}");
                Print(bin.Left, indent + 1);
                Print(bin.Right, indent + 1);
                break;

            case STVariableAccess acc:
                Console.WriteLine($"{pad}VarAccess {acc.Name}");
                if (acc.NamespacePath.Count > 0)
                    Console.WriteLine($"{pad}  Namespace: {string.Join(".", acc.NamespacePath)}");
                foreach (var sel in acc.Selectors)
                    Print(sel, indent + 1);
                break;

            case STFieldSelector fs:
                Console.WriteLine($"{pad}FieldSelector .{fs.FieldName}");
                break;

            case STIndexSelector idx:
                Console.WriteLine($"{pad}IndexSelector");
                foreach (var i in idx.Indexes)
                    Print(i, indent + 1);
                break;

            case STDereferenceSelector _:
                Console.WriteLine($"{pad}DereferenceSelector ^");
                break;

            default:
                Console.WriteLine($"{pad}{entity.GetType().Name}");
                break;
        }
    }

    private void PrintPou(STPou pou, int indent)
    {
        string pad = new string(' ', (indent + 1) * 2);

        if (pou.Variables.Count > 0)
        {
            Console.WriteLine($"{pad}Variables:");
            foreach (var v in pou.Variables)
                Print(v, indent + 2);
        }

        if (pou.Body.Count > 0)
        {
            Console.WriteLine($"{pad}Body:");
            foreach (var stmt in pou.Body)
                Print(stmt, indent + 2);
        }
    }
}