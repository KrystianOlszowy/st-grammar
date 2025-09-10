using System;
using System.Collections.Generic;
using Antlr4.Runtime.Tree;

public class STTreeBuilder : stBaseVisitor<object>
{
    // Cały plik //
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

        foreach (var typeDecl in context.dataTypeDeclaration())
        {
            if (Visit(typeDecl) is List<STTypeDeclaration> typeDeclaration)
                file.Declarations.AddRange(typeDeclaration);
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
        
        // deklaracje zmiennych wejścia/wyjścia
        if (context.ioVarDeclarations() != null)
        {
            foreach (var ioVarDeclarations in context.ioVarDeclarations())
            {
                program.Variables.AddRange(VisitIoVarDeclarations(ioVarDeclarations));
            }
        }

        // deklaracje zmiennych zewnętrznych
        if (context.externalVarDeclarations() != null)
        {
            foreach (var externalVarDeclarations in context.externalVarDeclarations())
            {
                program.Variables.AddRange(VisitExternalVarDeclarations(externalVarDeclarations));
            }
        }

        // deklaracje zmiennych tymczasowych
        if (context.tempVarDeclarations() != null)
        {
            foreach (var tempVarDeclarations in context.tempVarDeclarations())
            {
                program.Variables.AddRange(VisitTempVarDeclarations(tempVarDeclarations));
            }
        }

        // zmienne adersowane
        if (context.locatedVarDeclarations() != null)
        {
            foreach (var locatedVarDeclarations in context.locatedVarDeclarations())
            {
                program.Variables.AddRange(VisitLocatedVarDeclarations(locatedVarDeclarations));
            }
        }

        // pozostałe typy zmiennych
        if (context.otherVarDeclarations() != null)
        {
            foreach (var otherVarDeclarations in context.otherVarDeclarations())
            {
                program.Variables.AddRange(VisitOtherVarDeclarations(otherVarDeclarations));
            }
        }

        if (context.programBody() != null)
        {
            program.Body = VisitProgramBody(context.programBody());
        }

        return program;
    }

    // Deklaracje zmiennych wejścia/wyjścia
    public override List<STVariable> VisitIoVarDeclarations(stParser.IoVarDeclarationsContext context)
    {
        if (context.inputVarDeclarations() != null)
            return VisitInputVarDeclarations(context.inputVarDeclarations());

        if (context.outputVarDeclarations() != null)
            return VisitOutputVarDeclarations(context.outputVarDeclarations());

        if (context.inOutVarDeclarations() != null)
            return VisitInOutVarDeclarations(context.inOutVarDeclarations());

        return null;
    }

    // Deklaracje zmiennych wejścia
    public override List<STVariable> VisitInputVarDeclarations(stParser.InputVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();
        foreach (var inputVarDecl in context.inputVarDeclaration())
        {
            if (VisitInputVarDeclaration(inputVarDecl) is List<STVariable> inputVarDecls)
            {
                foreach (var var in inputVarDecls)
                {
                    var.IsInput = true;

                    if (context.RETAIN() != null)
                        var.IsRetain = true;

                    variables.Add(var);
                }
            }
        }
        return variables;
    }

    public override List<STVariable> VisitInputVarDeclaration(stParser.InputVarDeclarationContext context)
    {
        if (context.varDeclarationInit() != null)
        {
            return VisitVarDeclarationInit(context.varDeclarationInit());
        }
        else if (context.edgeDeclaration() != null)
        {
            return VisitEdgeDeclaration(context.edgeDeclaration());
        }
        else if (context.arrayConformDeclaration() != null)
        {
            return VisitArrayConformDeclaration(context.arrayConformDeclaration());
        }
        else
        {
            return null;
        }
    }

    // Deklaracje zmiennych wyjścia
    public override List<STVariable> VisitOutputVarDeclarations(stParser.OutputVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();
        foreach (var outputVarDecl in context.outputVarDeclaration())
        {
            if (VisitOutputVarDeclaration(outputVarDecl) is List<STVariable> outputVarDecls)
            {
                foreach (var var in outputVarDecls)
                {
                    var.IsOutput = true;
                    variables.Add(var);
                }
            }
        }

        if (context.RETAIN() != null)
            foreach (var var in variables)
                var.IsRetain = true;

        return variables;
    }

    public override List<STVariable> VisitOutputVarDeclaration(stParser.OutputVarDeclarationContext context)
    {
        if (context.varDeclarationInit() != null)
        {
            return VisitVarDeclarationInit(context.varDeclarationInit());
        }
        else if (context.arrayConformDeclaration() != null)
        {
            return VisitArrayConformDeclaration(context.arrayConformDeclaration());
        }
        else
        {
            return null;
        }
    }

    // Deklaracje zmiennych wejścia/wyjścia
    public override List<STVariable> VisitInOutVarDeclarations(stParser.InOutVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();
        foreach (var inOutVarDecl in context.inOutVarDeclaration())
        {
            if (VisitInOutVarDeclaration(inOutVarDecl) is List<STVariable> inOutVarDecls)
            {
                foreach (var var in inOutVarDecls)
                {
                    var.IsInputOutput = true;
                    variables.Add(var);
                }
            }
        }
        return variables;
    }

    public override List<STVariable> VisitInOutVarDeclaration(stParser.InOutVarDeclarationContext context)
    {
        if (context.varDeclarationInit() != null)
        {
            return VisitVarDeclarationInit(context.varDeclarationInit());
        }
        else if (context.arrayConformDeclaration() != null)
        {
            return VisitArrayConformDeclaration(context.arrayConformDeclaration());
        }
        else
        {
            return null;
        }
    }

    // Deklaracje zmiennych tymczasowych
    public override List<STVariable> VisitTempVarDeclarations(stParser.TempVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();
        foreach (var decl in context.varDeclarationInit())
        {
            if (VisitVarDeclarationInit(decl) is List<STVariable> tempVarDecls)
            {
                foreach (var var in tempVarDecls)
                {
                    var.IsTemporary = true;
                    variables.Add(var);
                }
            }
        }
        return variables;
    }

    // Deklaracje zmiennych zewnętrznych
    public override List<STVariable> VisitExternalVarDeclarations(stParser.ExternalVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();
        foreach (var externalDecl in context.externalDeclaration())
        {
            if (VisitExternalDeclaration(externalDecl) is STVariable var)
            {
                var.IsExternal = true;
                if (context.CONSTANT() != null)
                {
                    var.IsConstant = true;
                }
                variables.Add(var);
            }
        }
        return variables;
    }

    public override STVariable VisitExternalDeclaration(stParser.ExternalDeclarationContext context)
    {
        var externalVar = new STVariable { Name = context.globalVarName().GetText() };
        if (context.simpleSpecification() != null)
        {
            externalVar.Type = VisitSimpleSpecification(context.simpleSpecification());
        }
        else if (context.arraySpecification() != null)
        {
            externalVar.Type = VisitArraySpecification(context.arraySpecification());
        }

        return externalVar;
    }

    // Deklaracje 
    public override List<STVariable> VisitLocatedVarDeclarations(stParser.LocatedVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();

        foreach (var varDecl in context.locatedVarDeclaration())
        {
            var locatedVar = new STVariable();

            if (varDecl.locatedVarSpecificationInit() != null)
            {
                locatedVar = VisitLocatedVarSpecificationInit(varDecl.locatedVarSpecificationInit());
            }

            if (varDecl.variableName() != null)
            {
                locatedVar.Name = varDecl.variableName().GetText();
            }

            if (context.CONSTANT() != null)
                locatedVar.IsConstant = true;

            if (context.RETAIN() != null)
                locatedVar.IsRetain = true;

            locatedVar.Address = VisitLocatedAt(varDecl.locatedAt());

            variables.Add(locatedVar);
        }

        return variables;
    }

    public override STVariable VisitLocatedVarSpecificationInit(stParser.LocatedVarSpecificationInitContext context)
    {
        var locatedVar = new STVariable();
        if (context.simpleSpecificationInit() != null)
        {
            locatedVar = VisitSimpleSpecificationInit(context.simpleSpecificationInit());
        }
        else if (context.arraySpecificationInit() != null)
        {
            locatedVar = VisitArraySpecificationInit(context.arraySpecificationInit());
        }
        return locatedVar;
    }

    public override STVariable VisitArraySpecificationInit(stParser.ArraySpecificationInitContext context)
    {
        var variable = new STVariable
        {
            Type = VisitArraySpecification(context.arraySpecification())
        };

        if (context.arrayInit() != null)
            variable.InitialValue = (STExpression)Visit(context.arrayInit());


        return variable;
    }

    public override string VisitLocatedAt(stParser.LocatedAtContext context)
    {
        return context.relativeAddress() != null ? context.relativeAddress().GetText() : context.partlySpecifiedAddress().GetText();
    }


    // Deklaracje pozostałych zmiennych
    public override List<STVariable> VisitOtherVarDeclarations(stParser.OtherVarDeclarationsContext context)
    {
        if (context.retainVarDeclarations() != null)
        {
            return VisitRetainVarDeclarations(context.retainVarDeclarations());
        }
        else if (context.locatedPartlyVarDeclaration() != null)
        {
            return VisitLocatedPartlyVarDeclaration(context.locatedPartlyVarDeclaration());
        }
        else
        {
            return null;
        }
    }

    public override List<STVariable> VisitRetainVarDeclarations(stParser.RetainVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();
        foreach (var retainVarDecl in context.varDeclarationInit())
        {
            if (VisitVarDeclarationInit(retainVarDecl) is List<STVariable> retainVarDecls)
            {
                foreach (var var in retainVarDecls)
                {
                    var.IsRetain = true;
                    variables.Add(var);
                }
            }
        }
        return variables;
    }

    public override List<STVariable> VisitLocatedPartlyVarDeclaration(stParser.LocatedPartlyVarDeclarationContext context)
    {
        var variables = new List<STVariable>();
        foreach (var locatedPartlyVarDecl in context.locatedPartlyVar())
        {
            var var = VisitLocatedPartlyVar(locatedPartlyVarDecl);

            if (context.RETAIN != null)
            {
                var.IsRetain = true;
            }
            variables.Add(var);
        }
        return variables;
    }

    public override STVariable VisitLocatedPartlyVar(stParser.LocatedPartlyVarContext context)
    {
        var locatedPartlyVar = new STVariable { Name = context.variableName().GetText() };

        if (context.varSpecification().simpleSpecification() != null)
        {
            locatedPartlyVar.Type = VisitSimpleSpecification(context.varSpecification().simpleSpecification());
        }
        else if (context.varSpecification().arraySpecification() != null)
        {
            locatedPartlyVar.Type = VisitArraySpecification(context.varSpecification().arraySpecification());
        }
        else if (context.varSpecification().stringSpecification() != null)
        {
            locatedPartlyVar.Type = VisitStringSpecification(context.varSpecification().stringSpecification());
        }

        locatedPartlyVar.RelativeAddress = context.RELATIVE_ADDRESS().GetText();

        return locatedPartlyVar;
    }

    public override STStringType VisitStringSpecification(stParser.StringSpecificationContext context)
    {
        var stringType = new STStringType();
        stringType.Length = new STLiteral() { Value = context.stringSize().GetText() };
        return stringType;
    }

    public override List<STVariable> VisitEdgeDeclaration(stParser.EdgeDeclarationContext context)
    {
        var edgeVarDecls = new List<STVariable>();

        foreach (var variableName in VisitVariableList(context.variableList()))
        {
            var edgeVarDecl = new STVariable { Name = variableName };

            if (context.R_EDGE() != null)
                edgeVarDecl.Type = new STNamedType { Name = "BOOL R_EDGE" };
            else if (context.F_EDGE() != null)
                edgeVarDecl.Type = new STNamedType { Name = "BOOL F_EDGE" };

            edgeVarDecls.Add(edgeVarDecl);
        }
        return edgeVarDecls;
    }

    public override List<STVariable> VisitArrayConformDeclaration(stParser.ArrayConformDeclarationContext context)
    {
        var arrayConformVarDecls = new List<STVariable>();

        foreach (var variableName in VisitVariableList(context.variableList()))
        {
            var arrayConformVarDecl = new STVariable { Name = variableName };
            var arrayType = new STArrayType() { ElementType = VisitDataTypeAccess(context.arrayConformand().dataTypeAccess()) };

            foreach (var dimension in context.arrayConformand().ASTERISK())
            {
                arrayType.Dimensions.Add(new STSubrange());
            }
            arrayConformVarDecls.Add(arrayConformVarDecl);
        }
        return arrayConformVarDecls;
    }

    public override List<STStatement> VisitProgramBody(stParser.ProgramBodyContext context)
    {
        if (context.statementList() != null)
            return VisitStatementList(context.statementList());
        else
            return new List<STStatement>();
    }

    // Deklaracje typów użytkownika
    public override List<STTypeDeclaration> VisitDataTypeDeclaration(stParser.DataTypeDeclarationContext context)
    {
        var decls = new List<STTypeDeclaration>();

        foreach (var typeDecl in context.typeDeclaration())
        {
            if (Visit(typeDecl) is STTypeDeclaration td)
                decls.Add(td);
        }

        return decls;
    }

    // Deklaracje struktur
    public override STStructTypeDeclaration VisitStructTypeDeclaration(stParser.StructTypeDeclarationContext context)
    {

        if (context.structTypeSpecification().structDeclaration() != null)
        {
            var decl = VisitStructDeclaration(context.structTypeSpecification().structDeclaration());
            decl.Name = context.structTypeName().GetText();
            return decl;
        }

        if (context.structTypeSpecification().structSpecificationInit() != null)
        {
            var decl = new STStructTypeDeclaration
            {
                DerivedStruct = VisitStructSpecificationInit(context.structTypeSpecification().structSpecificationInit()),
                Name = context.structTypeName().GetText(),
            };
            return decl;
        }

        return null;
    }

    public override STStructTypeDeclaration VisitStructDeclaration(stParser.StructDeclarationContext context)
    {
        var declSpec = new STStructTypeDeclaration
        {
            Overlap = context.OVERLAP() != null
        };

        foreach (var elemDeclaration in context.structElementDeclaration())
            declSpec.Fields.Add(VisitStructElementDeclaration(elemDeclaration));

        return declSpec;
    }

    public override STStructField VisitStructElementDeclaration(stParser.StructElementDeclarationContext context)
    {
        var field = new STStructField
        {
            Name = context.structElementName().GetText()
        };

        // Addresowanie w strukturze
        if (context.locatedAt() != null)
        {
            if (context.locatedAt().relativeAddress() != null)
                field.Address = context.locatedAt().relativeAddress().GetText();
            else
                field.Address = context.locatedAt().partlySpecifiedAddress().GetText();

            if (context.multibitPartAccess() != null)
                field.Address += context.multibitPartAccess().GetText();
        }

        // Proste elementy struktury
        if (context.simpleSpecificationInit() != null)
        {
            field.FieldType = new STNamedType
            {
                Name = context.simpleSpecificationInit().simpleSpecification().GetText()
            };

            if (context.simpleSpecificationInit().simpleInit() != null)
                field.InitialValue = (STExpression)Visit(context.simpleSpecificationInit().simpleInit().expression());
        }
        else if (context.arraySpecificationInit() != null)
        {
            field.FieldType = VisitArraySpecification(context.arraySpecificationInit().arraySpecification());
            if (context.arraySpecificationInit().arrayInit() != null)
                field.InitialValue = (STExpression)Visit(context.arraySpecificationInit().arrayInit());
        }
        // Element struktury jest inną strukturą
        else if (context.structSpecificationInit() != null)
        {
            var spec = VisitStructSpecificationInit(context.structSpecificationInit());
            field.FieldType = spec.StructTypeName;
            field.InitialValue = spec.InitialValue;
        }
        // analogicznie: subrangeSpecificationInit, enumSpecificationInit

        return field;
    }

    public override STStructVariable VisitStructSpecificationInit(stParser.StructSpecificationInitContext context)
    {
        var structVariable = new STStructVariable
        {
            StructTypeName = VisitDerivedTypeAccess(context.structSpecification().derivedTypeAccess())
        };

        if (context.structInit() != null)
            structVariable.InitialValue = VisitStructInit(context.structInit());

        return structVariable;
    }

    public override STStructInit VisitStructInit(stParser.StructInitContext context)
    {
        var init = new STStructInit();

        foreach (var elem in context.structElementInit())
        {
            var name = elem.structElementName().GetText();

            if (elem.expression() != null)
                init.Fields[name] = (STExpression)Visit(elem.expression());
            else if (elem.enumValue() != null)
                init.Fields[name] = VisitEnumValue(elem.enumValue());
            else if (elem.arrayInit() != null)
                init.Fields[name] = (STExpression)Visit(elem.arrayInit());
            else if (elem.structInit() != null)
                init.Fields[name] = (STExpression)VisitStructInit(elem.structInit());
            else if (elem.referenceValue() != null)
                init.Fields[name] = new STLiteral { Value = elem.referenceValue().GetText() };
        }
        return init;
    }

    // ZMIENNE 
    public override List<STVariable> VisitNormalVarDeclarations(stParser.NormalVarDeclarationsContext context)
    {
        var variables = new List<STVariable>();

        foreach (var declaration in context.varDeclarationInit())
        {
            variables.AddRange(VisitVarDeclarationInit(declaration));

        }
        foreach(var variable in variables)
        {
            if (context.accessSpecification() != null)
            {
                variable.AccessType = context.accessSpecification().GetText();
            }
            if (context.CONSTANT() != null)
                variable.IsConstant = true;
        }
        return variables;
    }

    public override List<STVariable> VisitVarDeclarationInit(stParser.VarDeclarationInitContext context)
    {
        var variables = new List<STVariable>();

        if (context.variableList() != null)
        {
            foreach (string variableName in VisitVariableList(context.variableList()))
            {
                // Obsługa inicjalizacji zmiennej
                if (context.simpleSpecificationInit().simpleInit() != null)
                {

                    variables.Add(new STVariable
                    {
                        Name = variableName,
                        Type = VisitSimpleSpecification(context.simpleSpecificationInit().simpleSpecification()),
                        InitialValue = (STExpression)Visit(context.simpleSpecificationInit().simpleInit().expression())
                    });
                }
                else // Deklaracja bez inicjalizacji
                {
                    variables.Add(new STVariable
                    {
                        Name = variableName,
                        Type = VisitSimpleSpecification(context.simpleSpecificationInit().simpleSpecification())
                    });
                }
            }
        }
        else if (context.arrayVarDeclarationInit() != null)
        {
            variables.AddRange(VisitArrayVarDeclarationInit(context.arrayVarDeclarationInit()));
        }

        return variables;
    }

    public override STNamedType VisitSimpleSpecification(stParser.SimpleSpecificationContext context)
    {
        if (context.derivedTypeAccess() != null)
            return VisitDerivedTypeAccess(context.derivedTypeAccess());
        else
            return new STNamedType { Name = context.elementaryTypeName().GetText() };
    }

    public override STVariable VisitSimpleSpecificationInit(stParser.SimpleSpecificationInitContext context)
    {
        return new STVariable
        {
            Type = VisitSimpleSpecification(context.simpleSpecification()),
            InitialValue = (STExpression)Visit(context.simpleInit().expression())
        };
    }

    // Deklaracja zmiennych tablicowych
    public override List<STVariable> VisitArrayVarDeclarationInit(stParser.ArrayVarDeclarationInitContext context)
    {
        var variables = new List<STVariable>();

        foreach (string variableName in VisitVariableList(context.variableList()))
        {
            if (context.arraySpecificationInit().arrayInit() != null)
            {
                variables.Add(new STArrayVariable
                {
                    Name = variableName,
                    Type = VisitArraySpecification(context.arraySpecificationInit().arraySpecification()),
                    InitialValue = VisitArrayInit(context.arraySpecificationInit().arrayInit())
                });
            }
            else // Deklaracja bez inicjalizacji
            {
                variables.Add(new STVariable
                {
                    Name = variableName,
                    Type = VisitArraySpecification(context.arraySpecificationInit().arraySpecification())
                });
            }
        }

        return variables;
    }

public override STArrayInitializer VisitArrayInit(stParser.ArrayInitContext context)
{
    var init = new STArrayInitializer();

    foreach (var elemCtx in context.arrayElementInit())
    {
        init.Elements.Add(VisitArrayElementInit(elemCtx));
    }

    return init;
}

public override STArrayElementInit VisitArrayElementInit(stParser.ArrayElementInitContext context)
{
    if (context.arrayElementMultiplier() != null) // np. 3(0)
    {
        return new STArrayElementRepeated
        {
            Multiplier = int.Parse(context.arrayElementMultiplier().GetText()),
            Value = context.arrayElementInitValue() != null 
                ? (STExpression)Visit(context.arrayElementInitValue()) 
                : null
        };
    }
    else
    {
        return new STArrayElementValue
        {
            Value = (STExpression)Visit(context.arrayElementInitValue())
        };
    }
}

    // Określenie typu tablicy
    public override STType VisitArraySpecification(stParser.ArraySpecificationContext context)
    {
        if (context.subrange() != null)
        {
            var arrayType = new STArrayType
            {
                ElementType = VisitDataTypeAccess(context.dataTypeAccess())
            };

            foreach (var subrange in context.subrange())
            {
                arrayType.Dimensions.Add(VisitSubrange(subrange));
            }
            return arrayType;
        }
        else
        {
            return VisitDerivedTypeAccess(context.derivedTypeAccess());
        }
    }

    public override STType VisitDataTypeAccess(stParser.DataTypeAccessContext context)
    {

        if (context.elementaryTypeName() != null)
            return new STNamedType { Name = context.elementaryTypeName().GetText() };
        else
            return VisitDerivedTypeAccess(context.derivedTypeAccess());
    }

    // Dostęp do zmiennej tworzonej przez użytkownika lub biblioteki
    public override STNamedType VisitDerivedTypeAccess(stParser.DerivedTypeAccessContext context)
    {
        var namedType = new STNamedType { Name = context.derivedTypeName().GetText() };

        foreach (var ns in context.namespaceName())
            namedType.NamespacePath.Add(ns.GetText());

        return namedType;
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

    // Wyrażenie warunkowe IF
    public override STIf VisitIfStatement(stParser.IfStatementContext context)
    {
        var ifStmt = new STIf
        {
            Condition = (STExpression)Visit(context.ifCondition()),
            ThenBranch = VisitStatementList(context.ifStatementList().statementList())
        };

        for (int i = 0; i < context.ELSIF().Length; i++)
        {
            ifStmt.ElseIfBranches.Add(((STExpression)Visit(context.elsifCondition(i).expression()),
                                        VisitStatementList(context.elsifStatementList(i).statementList())));
        }

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
            var label = (STExpression)Visit(labelCtx);
            selection.Labels.Add(label);
        }

        // Instrukcje
        selection.Body.AddRange(VisitStatementList(context.statementList()));

        return selection;
    }

    public override STExpression VisitCaseListElement(stParser.CaseListElementContext context)
    {
        if (context.subrange() != null)
        {
            return VisitSubrange(context.subrange());
        }
        else
        {
            return (STExpression)Visit(context.expression());
        }
    }

    public override STSubrange VisitSubrange(stParser.SubrangeContext context)
    {
        return new STSubrange
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

    // Wyrażenia zawarte w nawiasach
    public override STExpression VisitBracketedExpression(stParser.BracketedExpressionContext constext)
    {
        return (STExpression)Visit(constext.expression());
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