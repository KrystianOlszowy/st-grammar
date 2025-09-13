// Abstrakcyjne klasy kategoryzujące elementy drzewa składniowego //
public abstract class STEntity { }

public abstract class STType : STEntity { }

public abstract class STDeclaration : STEntity
{
    public string Name { get; set; }
}

public abstract class STPou : STDeclaration
{
    public List<STVariable> Variables { get; set; } = new();
    public List<STStatement> Body { get; set; } = new();
}

// Cały plik //
public class STFile : STEntity
{
    public List<STDeclaration> Declarations { get; set; } = new();
}

// DEKLARACJE //
public class STNamespace : STDeclaration
{
    public List<STDeclaration> Members { get; set; } = new();
}

// Deklracje POU
public class STProgram : STPou { }
public class STFunction : STPou { }
public class STFunctionBlock : STPou { }

// Deklaracje typów użytkownika
public abstract class STTypeDeclaration : STDeclaration { }

// Deklaracje struktur
public class STStructTypeDeclaration : STTypeDeclaration
{
    public STStructVariable DerivedStruct { get; set; } = null;

    public List<STStructField> Fields { get; set; } = new();

    public bool Overlap { get; set; }
}

public class STStructField : STEntity
{
    public string Name { get; set; }
    public STType FieldType { get; set; }
    public STExpression InitialValue { get; set; }
    public string Address { get; set; }
}

// Deklaracje zmiennych
public class STVariable : STDeclaration
{
    public STType Type { get; set; }

    public STExpression InitialValue { get; set; }

    public bool IsConstant { get; set; }
    public bool IsInput { get; set; }
    public bool IsOutput { get; set; }

    public bool IsInputOutput { get; set; }

    public bool IsRetain { get; set; }

    public string AccessType { get; set; }

    public bool IsTemporary { get; set; }

    public bool IsExternal { get; set; }

    public string Address { get; set; }
    public string RelativeAddress { get; set; }
}

public class STStructVariable : STVariable
{
    public STNamedType StructTypeName { get; set; }
    public new STStructInit InitialValue { get; set; }
}


// Deklaracje tablic
public class STArrayVariable : STVariable
{
    public STArrayType ArrayType { get; set; }
    public STArrayInitializer Initializer { get; set; }
}

// TYPY DANYCH I ELEMENTY DEKLARACJI //
public class STArrayType : STType
{
    public List<STSubrange> Dimensions { get; set; } = new();
    public STType ElementType { get; set; }
}


public class STNamedType : STType
{
    public string Name { get; set; }

    public List<string> NamespacePath { get; set; } = new();
}

public class STStringType : STType
{
    public STNamedType StringType { get; set; }
    public STExpression Length { get; set; }    
}

public class STSubrange : STExpression
{
    public STExpression From { get; set; }
    public STExpression To { get; set; }
}

public class STArrayInitializer : STExpression
{
    public List<STArrayElementInit> Elements { get; set; } = new();
}

public abstract class STArrayElementInit : STEntity { }

public class STArrayElementValue : STArrayElementInit
{
    public STExpression Value { get; set; }
}

public class STArrayElementRepeated : STArrayElementInit
{
    public int Multiplier { get; set; }
    public STExpression Value { get; set; }
}

public class STStructInit : STExpression
{
    public Dictionary<string, STExpression> Fields { get; set; } = new();
}

// INSTRUKCJE //
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
    public List<STExpression> Labels { get; set; } = new();
    public List<STStatement> Body { get; set; } = new();
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

// WYRAŻENIA //
public abstract class STExpression : STEntity { }

// Wyrażenie z operatorem jednoargumentowym
public class STUnaryExpression : STExpression
{
    public string Operator { get; set; }
    public STExpression Operand { get; set; }
}

// Wyrażenie z operatorami 2 argumentowymi
public class STBinaryExpression : STExpression
{
    public string Operator { get; set; }
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
