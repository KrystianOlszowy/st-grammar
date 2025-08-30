
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

public class STAssignment : STStatement
{
    public STVariableAccess Target { get; set; }

    public string Operator { get; set; } = ":=";
    public STExpression Value { get; set; }
}


// Wywołanie funkcji jako instrukcji
public class STFunctionCallStatement : STStatement
{
    public STFunctionCall Call { get; set; }
}

// Wywołanie metody/FB
public class STInvocation : STStatement
{
    public STExpression Target { get; set; }
    public List<STExpression> Arguments { get; set; } = new();
}

public class STSuperCall : STStatement { }

// Return
public class STReturn : STStatement
{
    public STExpression Value { get; set; }
}

public class STIf : STStatement
{
    public STExpression Condition { get; set; }
    public List<STStatement> ThenBranch { get; set; } = new();
    public List<(STExpression Condition, List<STStatement> Body)> ElseIfBranches { get; set; } = new();
    public List<STStatement> ElseBranch { get; set; } = new();
}

public class STCase : STStatement
{
    public STExpression Expression { get; set; }
    public List<STCaseSelection> Selections { get; set; } = new();
    public List<STStatement> ElseBranch { get; set; } = new();
}

public class STCaseSelection
{
    public List<STExpression> Labels { get; set; } = new();
    public List<STStatement> Body { get; set; } = new();
}

public class STFor : STStatement
{
    public string Variable { get; set; }
    public STExpression From { get; set; }
    public STExpression To { get; set; }
    public STExpression By { get; set; }
    public List<STStatement> Body { get; set; } = new();
}

public class STWhile : STStatement
{
    public STExpression Condition { get; set; }
    public List<STStatement> Body { get; set; } = new();
}

public class STRepeat : STStatement
{
    public List<STStatement> Body { get; set; } = new();
    public STExpression Until { get; set; }
}

// WYRAŻENIA
public abstract class STExpression : STEntity { }

public class STQualifiedName : STExpression
{
    public List<string> Parts { get; set; } = new();

    public override string ToString() => string.Join(".", Parts);
}

public class STLiteral : STExpression
{
    public object Value { get; set; }
}

public class STFunctionCall : STExpression
{
    public STQualifiedName Target { get; set; }
    public List<STExpression> Arguments { get; set; } = new();
}

public class STDereference : STExpression
{
    public STExpression Target { get; set; }
}

public class STUnaryExpression : STExpression
{
    public string Operator { get; set; } // np. "-", "NOT"
    public STExpression Operand { get; set; }
}

public class STBinaryExpression : STExpression
{
    public string Operator { get; set; } // "+", "-", "*", "/", AND, OR, XOR, =
    public STExpression Left { get; set; }
    public STExpression Right { get; set; }
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