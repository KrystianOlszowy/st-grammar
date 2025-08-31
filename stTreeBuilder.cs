using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;

public class STTreeBuilder : stBaseVisitor<object>
{
    // CAŁY PLIK //
    public override STFile VisitFile(stParser.FileContext context)
    {
        var file = new STFile
        {
            Declarations = new List<STDeclaration>()
        };

        foreach (var decl in context.pouDeclaration())
        {
            if (Visit(decl) is STDeclaration declaration)
                file.Declarations.Add(declaration);
        }

        return file;
    }

    // DEKLARACJE //

    // PROGRAM
    public override STProgram VisitProgramDeclaration(stParser.ProgramDeclarationContext context)
    {
        var program = new STProgram
        {
            Name = context.programName().GetText(),
            Variables = new List<STVariable>(),
            Body = new List<STStatement>()
        };

        // deklaracje zmiennych lokalnych dla programu
        if (context.normalVarDeclarations() != null)
        {
            foreach (var normalVarDeclarations in context.normalVarDeclarations())
            {
                program.Variables.AddRange(VisitNormalVarDeclarations(normalVarDeclarations));
            }
        }

        if (context.programBody() != null)
        {
            program.Body = VisitProgramBody(context.programBody());
        }

        return program;
    }

    public override List<STStatement> VisitProgramBody(stParser.ProgramBodyContext context)
    {
        return VisitStatementList(context.statementList());
    }

    // ZMIENNE 
    public override List<STVariable> VisitNormalVarDeclarations(stParser.NormalVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();

        foreach (var declaration in context.varDeclarationInit())
        {
            variables.AddRange(VisitVarDeclarationInit(declaration));
        }

        return variables;
    }

    public override List<STVariable> VisitVarDeclarationInit(stParser.VarDeclarationInitContext context)
    {
        var variables = new List<STVariable>();

        foreach (string variableName in VisitVariableList(context.variableList()))
        {
            // Obsługa inicjalizacji zmiennej
            if (context.simpleSpecificationInit().simpleInit() != null)
            {

                variables.Add(new STVariable
                {
                    Name = variableName,
                    Type = context.simpleSpecificationInit().simpleSpecification().GetText(),
                    InitialValue = (STExpression)Visit(context.simpleSpecificationInit().simpleInit().expression())
                });
            }
            else // Deklaracja bez inicjalizacji
            {
                variables.Add(new STVariable
                {
                    Name = variableName,
                    Type = context.simpleSpecificationInit().simpleSpecification().GetText()
                });
            }
        }
        return variables;
    }

    // Lista zmiennych przy tworzeniu zmiennych np. VAR x, y, z : INT;
    public override List<string> VisitVariableList(stParser.VariableListContext context)
    {
        var names = new List<string>();
        foreach (var name in context.variableName())
        {
            names.Add(name.GetText());
        }
        return names;
    }

    // INSTRUKCJE //
    public override List<STStatement> VisitStatementList(stParser.StatementListContext context)
    {
        var statements = new List<STStatement>();

        foreach (var stmt in context.statement())
        {
            if (Visit(stmt) is STStatement statement)
                statements.Add(statement);
        }

        return statements;
    }

    // Przypisanie
    public override STAssignment VisitAssignStatement(stParser.AssignStatementContext context)
    {
        return new STAssignment
        {
            Target = (STVariableAccess)VisitVariable(context.variable()),
            Operator = context.assignOperator().GetText(),
            Value = (STExpression)Visit(context.expression())
        };
    }

    // Dostęp do zmiennej
    public override STVariableAccess VisitVariable(stParser.VariableContext context)
    {
        if (context.directVariable() != null)
            return new STVariableAccess
            {
                Address = context.directVariable().GetText()
            };

        if (context.symbolicVariable() != null)
        {
            return VisitSymbolicVariable(context.symbolicVariable());
        }

        return null;
    }

    // Dostęp do zmiennej symbolicznej
    public override STVariableAccess VisitSymbolicVariable(stParser.SymbolicVariableContext context)
    {
        var varAccess = new STVariableAccess
        {
            IsThis = context.THIS() != null
        };

        foreach (var ns in context.namespaceName())
            varAccess.NamespacePath.Add(ns.GetText());

        if (context.variableAccess().variableName() != null)
        {
            varAccess.Name = context.variableAccess().variableName().GetText();
        }
        else if (context.variableAccess().dereference().referenceName() != null)
        {
            varAccess.Name = context.variableAccess().dereference().referenceName().GetText();
            varAccess.Selectors.Add(new STDereferenceSelector());
        }

        foreach (var selector in context.variableElementSelect())
        {
            if (selector.subscriptList() != null)
            {
                var newSelector = new STIndexSelector();
                foreach (var exp in selector.subscriptList().expression())
                {
                    newSelector.Indexes.Add((STExpression)Visit(exp));
                }
                varAccess.Selectors.Add(newSelector);
            }
            else if (selector.variableAccess() != null)
            {
                if (selector.variableAccess().variableName() != null)
                {
                    varAccess.Selectors.Add(new STFieldSelector { FieldName = selector.variableAccess().variableName().GetText() });
                }
                else if (selector.variableAccess().dereference().referenceName() != null)
                {
                    varAccess.Selectors.Add(new STFieldSelector { FieldName = selector.variableAccess().dereference().referenceName().GetText() });
                    for (int i = 0; i < selector.variableAccess().dereference().CARET().Length; i++)
                    {
                        varAccess.Selectors.Add(new STDereferenceSelector());
                    }
                }
            }
        }
        return varAccess;
    }

    public override STFunctionCallStatement VisitFunctionCallStatement(stParser.FunctionCallStatementContext context)
    {
        return new STFunctionCallStatement
        {
            FunctionCall = VisitFunctionCall(context.functionCall())
        };
    }

    // Wyrażenie warunkowe IF-ELSIF-ELSE
    public override STIf VisitIfStatement(stParser.IfStatementContext context)
    {
        var ifStmt = new STIf
        {
            Condition = (STExpression)Visit(context.ifCondition()),
            ThenBranch = VisitStatementList(context.ifStatementList().statementList())
        };

        //ELSIF
        for (int i = 0; i < context.ELSIF().Length; i++)
        {

            ifStmt.ElseIfBranches.Add(((STExpression)Visit(context.elsifCondition(i).expression()),
                                        VisitStatementList(context.elsifStatementList(i).statementList())));
        }

        // ELSE
        if (context.ELSE() != null)
        {
            ifStmt.ElseBranch.AddRange(VisitStatementList(context.elseStatementList().statementList()));
        }

        return ifStmt;
    }

    // Wyrazenie warunkowe CASE
    public override STCase VisitCaseStatement(stParser.CaseStatementContext context)
    {
        var caseStmt = new STCase
        {
            Selector = (STExpression)Visit(context.expression())
        };

        foreach (var selCtx in context.caseSelection())
        {
            var selection = (STCaseSelection)Visit(selCtx);
            caseStmt.Selections.Add(selection);
        }

        if (context.statementList() != null)
        {
            caseStmt.ElseBranch.AddRange(VisitStatementList(context.statementList()));
        }

        return caseStmt;
    }

    // etykiety dla CASE
    public override STCaseSelection VisitCaseSelection(stParser.CaseSelectionContext context)
    {
        var selection = new STCaseSelection();

        // Etykiety
        foreach (var labelCtx in context.caseList().caseListElement())
        {
            var label = (STCaseLabel)Visit(labelCtx);
            selection.Labels.Add(label);
        }

        // Instrukcje
        selection.Body.AddRange(VisitStatementList(context.statementList()));

        return selection;
    }

    public override STCaseLabel VisitCaseListElement(stParser.CaseListElementContext context)
    {
        if (context.subrange() != null)
        {
            return VisitSubrange(context.subrange());
        }
        else
        {
            return new STCaseExpressionLabel
            {
                Expression = (STExpression)Visit(context.expression())
            };
        }
    }

    public override STCaseRangeLabel VisitSubrange(stParser.SubrangeContext context)
    {
        return new STCaseRangeLabel
        {
            From = (STExpression)Visit(context.subrangeBegin().expression()),
            To = (STExpression)Visit(context.subrangeEnd().expression())
        };
    }

    // Pętla WHILE
    public override STWhile VisitWhileStatement(stParser.WhileStatementContext context)
    {
        return new STWhile
        {
            Condition = (STExpression)Visit(context.expression()),
            Body = VisitStatementList(context.statementList())
        };
    }

    // Pętla FOR
    public override STFor VisitForStatement(stParser.ForStatementContext context)
    {
        return new STFor
        {
            Iterator = context.controlVariable().GetText(),
            From = (STExpression)Visit(context.forRange().expression(0)),
            To = (STExpression)Visit(context.forRange().expression(1)),
            By = context.forRange().expression().Length > 2
                ? (STExpression)Visit(context.forRange().expression(2))
                : null,
            Body = VisitStatementList(context.statementList())
        };
    }

    // Pętla REPEAT
    public override STRepeat VisitRepeatStatement(stParser.RepeatStatementContext context)
    {
        return new STRepeat
        {
            Body = VisitStatementList(context.statementList()),
            Until = (STExpression)Visit(context.expression())
        };
    }

    // EXIT
    public override STExit VisitExitStatement(stParser.ExitStatementContext context)
    {
        return new STExit();
    }

    // CONTINUE
    public override STContinue VisitContinueStatement(stParser.ContinueStatementContext context)
    {
        return new STContinue();
    }
    
    // SUPER()
    public override STSuperCall VisitSuperCallStatement(stParser.SuperCallStatementContext context)
    {
        return new STSuperCall();
    }

    // RETURN <wyrażenie>
    public override STReturn VisitReturnStatement(stParser.ReturnStatementContext context)
    {
        return new STReturn();
    }


    // WYRAŻENIA //
    // Wyrażenia podstawowe
    public override STExpression VisitPrimaryExpression(stParser.PrimaryExpressionContext context)
    {
        if (context.literalValue() != null)
            return new STLiteral { Value = context.literalValue().GetText() };

        if (context.variableValue() != null)
            return VisitVariable(context.variableValue().variable());

        if (context.enumValue() != null)
            return VisitEnumValue(context.enumValue());

        if (context.referenceValue() != null)
            return new STLiteral { Value = Visit(context.referenceValue()) };

        return null;
    }

    // wywołanie funkcji
    public override STFunctionCall VisitFunctionCall(stParser.FunctionCallContext context)
    {
        var call = new STFunctionCall
        {
            Name = context.functionAccess().functionName().GetText()
        };

        foreach (var ns in context.functionAccess().namespaceName())
            call.NamespacePath.Add(ns.GetText());

        if (context.parameterAssign() != null)
        {
            foreach (var param in context.parameterAssign())
                call.Parameters.Add(VisitParameterAssign(param));
        }

        return call;
    }

    // Parametry wywołania POU
    public override STPouParameter VisitParameterAssign(stParser.ParameterAssignContext context)
    {
        var param = new STPouParameter();

        if (context.variableName() != null)
            param.Name = context.variableName().GetText();

        if (context.ASSIGN_OUT() != null)
        {
            param.IsOutput = true;
            param.Value = (STExpression)Visit(context.variable());
        }
        else
        {
            param.IsOutput = false;
            param.Value = (STExpression)Visit(context.expression());

            if (context.NOT() != null)
                param.IsNegated = true;
        }
        return param;
    }

    // Literał enumeracyjny
    public override STEnumValue VisitEnumValue(stParser.EnumValueContext context)
    {
        var enumValue = new STEnumValue();

        if (context.enumTypeName() != null)
            enumValue.TypeName = context.enumTypeName().GetText();

        if (context.enumElementName() != null)
            enumValue.ElementName = context.enumElementName().GetText();

        return enumValue;
    }

    public override STDereference VisitDerefExpression(stParser.DerefExpressionContext context)
    {
        return new STDereference
        {
            Operand = (STExpression)Visit(context.expression())
        };
    }

    // Wyrażenie z operatorem jednoargumentowym
    public override STUnaryExpression VisitUnaryExpression(stParser.UnaryExpressionContext context)
    {
        return new STUnaryExpression
        {
            Operator = context.unaryOperator().GetText(),
            Operand = (STExpression)Visit(context.expression())
        };
    }

    // Wyrażenia z operatorami 2 argumentowymi
    public override STBinaryExpression VisitExponentExpression(stParser.ExponentExpressionContext context)
    {
        return new STBinaryExpression
        {
            Operator = context.exponentOperator().GetText(),
            Left = (STExpression)Visit(context.expression(0)),
            Right = (STExpression)Visit(context.expression(1))
        };
    }

    public override STBinaryExpression VisitMultDivModExpression(stParser.MultDivModExpressionContext context)
    {
        return new STBinaryExpression
        {
            Operator = context.multDivModOperator().GetText(),
            Left = (STExpression)Visit(context.expression(0)),
            Right = (STExpression)Visit(context.expression(1))
        };
    }
    public override STBinaryExpression VisitAddSubExpression(stParser.AddSubExpressionContext context)
    {
        return new STBinaryExpression
        {
            Operator = context.addSubOperator().GetText(),
            Left = (STExpression)Visit(context.expression(0)),
            Right = (STExpression)Visit(context.expression(1))
        };
    }

    public override STBinaryExpression VisitComparisonExpression(stParser.ComparisonExpressionContext context)
    {
        return new STBinaryExpression
        {
            Operator = context.comparisonOperator().GetText(),
            Left = (STExpression)Visit(context.expression(0)),
            Right = (STExpression)Visit(context.expression(1))
        };
    }

    public override STBinaryExpression VisitAndExpression(stParser.AndExpressionContext context)
    {
        return new STBinaryExpression
        {
            Operator = context.andOperator().GetText(),
            Left = (STExpression)Visit(context.expression(0)),
            Right = (STExpression)Visit(context.expression(1))
        };
    }

    public override STBinaryExpression VisitXorExpression(stParser.XorExpressionContext context)
    {
        return new STBinaryExpression
        {
            Operator = context.xorOperator().GetText(),
            Left = (STExpression)Visit(context.expression(0)),
            Right = (STExpression)Visit(context.expression(1))
        };
    }

    public override STBinaryExpression VisitOrExpression(stParser.OrExpressionContext context)
    {
        return new STBinaryExpression
        {
            Operator = context.orOperator().GetText(),
            Left = (STExpression)Visit(context.expression(0)),
            Right = (STExpression)Visit(context.expression(1))
        };
    }
}