// ELEMENTY JĘZYKA ST
public abstract class STEntity { }

// Cały plik z kodem ST
public class STFile : STEntity {
    public List<STPou> Pous { get; set; } = new();
}

// Jednostki programowe
public abstract class STPou : STEntity
{
    public string Name { get; set; }
    public List<STVariable> Variables { get; set; } = new();
    public List<STStatement> Body { get; set; } = new();
}

public class STProgram : STPou { }
public class STFunction : STPou { }
public class STFunctionBlock : STPou { }

// Deklaracje zmiennych
public class STVariable : STEntity
{
    public string Name { get; set; }
    public string Type { get; set; }
    public STExpression InitialValue { get; set; }
}

// INSTRUKCJE
public abstract class STStatement : STEntity { }

// Przypisania wartości do zmiennych
public class STAssignment : STStatement
{
    public STExpression Target { get; set; }
    public STExpression Value { get; set; }
}

// Instrukcja warunkowa IF
public class STIf : STStatement
{
    public STExpression Condition { get; set; }
    public List<STStatement> ThenBranch { get; set; } = new();
    public List<STStatement> ElseBranch { get; set; } = new();
}

// Instrukcja pętli FOR
public class STFor : STStatement
{
    public string Iterator { get; set; }
    public STExpression From { get; set; }
    public STExpression To { get; set; }
    public List<STStatement> Body { get; set; } = new();
}

// WYRAŻENIA
public abstract class STExpression : STEntity { }

// Identyfikator obiektu, nazwa zmiennych, funkcji itp.
public class STIdentifier : STExpression
{
    public string Name { get; set; }
}

// Wyrażenie stałe, np. liczby, teksty, daty
public class STLiteral : STExpression
{
    public string Value { get; set; }
}

// Wyrażenie 2-argumentowe, np. operacje arytmetyczne
public class STBinaryOperation : STExpression
{
    public string Operator { get; set; }
    public STExpression Left { get; set; }
    public STExpression Right { get; set; }
}

// Wyrażenie dostępu do elementu tablicy
public class STArrayAccess : STExpression
{
    public STExpression Array { get; set; }
    public STExpression Index { get; set; }
}