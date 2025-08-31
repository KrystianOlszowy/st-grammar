
public abstract class STEntity { }

// CAŁY PLIK
public class STFile : STEntity
{
    public List<STDeclaration> Declarations { get; set; } = new();
}

// DEKLARACJE
public abstract class STDeclaration : STEntity
{
    public string Name { get; set; }
}


public class STNamespace : STDeclaration
{
    public List<STDeclaration> Members { get; set; } = new();
}


public abstract class STPou : STDeclaration
{
    public List<STVariable> Variables { get; set; } = new();
    public List<STStatement> Body { get; set; } = new();
}

public class STVariable : STDeclaration
{
    public string Type { get; set; }

    public STExpression InitialValue { get; set; }
}

public class STProgram : STPou { }
public class STFunction : STPou { }
public class STFunctionBlock : STPou { }


// INSTRUKCJE
public abstract class STStatement : STEntity { }

// Instrukcja przypisania
public class STAssignment : STStatement
{
    public STVariableAccess Target { get; set; }

    public string Operator { get; set; } = ":=";
    public STExpression Value { get; set; }
}

// Wywołanie funkcji jako samodzielnej instrukcji
public class STFunctionCallStatement : STStatement
{
    public STFunctionCall FunctionCall { get; set; }
}

public class STSuperCall : STStatement { }

// Return
public class STReturn : STStatement { }

// IF
public class STIf : STStatement
{
    public STExpression Condition { get; set; }
    public List<STStatement> ThenBranch { get; set; } = new();
    public List<(STExpression Condition, List<STStatement> Body)> ElseIfBranches { get; set; } = new();
    public List<STStatement> ElseBranch { get; set; } = new();
}

// CASE
public class STCase : STStatement
{
    public STExpression Selector { get; set; }
    public List<STCaseSelection> Selections { get; set; } = new();
    public List<STStatement> ElseBranch { get; set; } = new();
}

public class STCaseSelection : STEntity
{
    public List<STCaseLabel> Labels { get; set; } = new();
    public List<STStatement> Body { get; set; } = new();
}

// etykiety dla CASE
public abstract class STCaseLabel : STEntity { }

public class STCaseExpressionLabel : STCaseLabel
{
    public STExpression Expression { get; set; }
}

public class STCaseRangeLabel : STCaseLabel
{
    public STExpression From { get; set; }
    public STExpression To { get; set; }
}

public class STFor : STStatement
{
    public string Iterator { get; set; }
    public STExpression From { get; set; }
    public STExpression To { get; set; }
    public STExpression By { get; set; }
    public List<STStatement> Body { get; set; } = new();
}
// WHILE
public class STWhile : STStatement
{
    public STExpression Condition { get; set; }
    public List<STStatement> Body { get; set; } = new();
}

// REPEAT
public class STRepeat : STStatement
{
    public List<STStatement> Body { get; set; } = new();
    public STExpression Until { get; set; }
}

// CONTINUE
public class STContinue : STStatement { }

// EXIT
public class STExit : STStatement { }

// Wyrażenia //
public abstract class STExpression : STEntity { }

// Wyrażenie z operatorem jednoargumentowym
public class STUnaryExpression : STExpression
{
    public string Operator { get; set; } // np. "-", "NOT"
    public STExpression Operand { get; set; }
}

// Wyrażenie z operatorami 2 argumentowymi
public class STBinaryExpression : STExpression
{
    public string Operator { get; set; } // "+", "-", "*", "/", AND, OR, XOR, =
    public STExpression Left { get; set; }
    public STExpression Right { get; set; }
}

// Literały
public class STLiteral : STExpression
{
    public object Value { get; set; }
}

// Dostęp do zmiennej
public class STVariableAccess : STExpression
{
    public string Name { get; set; }
    public string Address { get; set; }
    public bool IsThis { get; set; } = false;
    public List<string> NamespacePath { get; set; } = new();
    public List<STVariableSelector> Selectors { get; set; } = new();
}

// Wybór elementu tablicy lub pola struktury z zmiennej
public abstract class STVariableSelector : STEntity { }
public class STDereferenceSelector : STVariableSelector { }

public class STFieldSelector : STVariableSelector
{
    public string FieldName { get; set; }
}

public class STIndexSelector : STVariableSelector
{
    public List<STExpression> Indexes { get; set; }
}

// Wartość enumeracyjna
public class STEnumValue : STExpression
{
    public string TypeName { get; set; }
    public string ElementName { get; set; }
}

// Wywołanie funkcji
public class STFunctionCall : STExpression
{
    public string Name { get; set; }
    public List<string> NamespacePath { get; set; } = new();
    public List<STPouParameter> Parameters { get; set; } = new();
}

public class STPouParameter : STExpression
{
    public string Name { get; set; }
    public bool IsOutput { get; set; }
    public bool IsNegated { get; set; }
    public STExpression Value { get; set; }
}

// Dereferencja jako wyrażenie
public class STDereference : STExpression
{
    public STExpression Operand { get; set; }
}
