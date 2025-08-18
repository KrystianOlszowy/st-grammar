using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;

public class STTreeBuilder : stBaseVisitor<STEntity>
{
    // -----------------------------
    // POU (Program, Function, FB)
    // -----------------------------
    public override STEntity VisitProgramDeclaration(stParser.ProgramDeclarationContext context)
    {
        var program = new STProgram
        {
            Name = context.programName().GetText()
        };

        // zmienne
        foreach (var normalDecl in context.normalVarDeclarations())
        {
            var vars = (List<STVariable>)Visit(normalDecl);
            program.Variables.AddRange(vars);
        }

        // ciało
        if (context.programBody() != null)
        {
            foreach (var statement in context.programBody().statementList().statement())
                program.Body.Add((STStatement)Visit(statement));
        }

        return program;
    }

    // -----------------------------
    // Deklaracje zmiennych
    // -----------------------------
    public override STEntity VisitNormalVarDeclarations(stParser.NormalVarDeclarationsContext context)
    {
        var result = new List<STVariable>();

        foreach (var decl in context.varDeclarationInit())
        {
            var varDecls = (List<STVariable>)Visit(decl);
            result.AddRange(varDecls);
        }

        return result;
    }

    public override STEntity VisitVarDeclarationInit(stParser.VarDeclarationInitContext context)
    {
        var vars = new List<STVariable>();

        var names = context.variableList()?.variableName()
                                 .Select(v => v.GetText())
                                 .ToList();

        // np. "x : INT := 5"
        if (context.simpleSpecificationInit() != null)
        {
            var typeName = context.simpleSpecificationInit().simpleTypeName().GetText();
            STExpression initExpr = null;

            if (context.simpleSpecificationInit().expression() != null)
                initExpr = (STExpression)Visit(context.simpleSpecificationInit().expression());

            foreach (var n in names)
            {
                vars.Add(new STVariable {
                    Name = n,
                    Type = typeName,
                    InitialValue = initExpr
                });
            }
        }

        return vars;
    }

    // -----------------------------
    // Instrukcje
    // -----------------------------
    public override STEntity VisitAssignmentStatement(stParser.AssignmentStatementContext context)
    {
        return new STAssignment
        {
            Target = (STExpression)Visit(context.variable()),
            Value = (STExpression)Visit(context.expression())
        };
    }

    // -----------------------------
    // Wyrażenia
    // -----------------------------
    public override STEntity VisitIdentifier(stParser.IdentifierContext context)
    {
        return new STIdentifier { Name = context.GetText() };
    }

    public override STEntity VisitIntegerLiteral(stParser.IntegerLiteralContext context)
    {
        return new STLiteral { Value = context.GetText() };
    }

    public override STEntity VisitBinaryExpression(stParser.BinaryExpressionContext context)
    {
        return new STBinaryOperation
        {
            Operator = context.op.Text,
            Left = (STExpression)Visit(context.left),
            Right = (STExpression)Visit(context.right)
        };
    }

    public override STEntity VisitArrayAccess(stParser.ArrayAccessContext context)
    {
        return new STArrayAccess
        {
            Array = (STExpression)Visit(context.variable()),
            Index = (STExpression)Visit(context.expression())
        };
    }
}