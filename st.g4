grammar st;

// namespace: (program | function | fb | global_var | class)*;

// Parser //
program:
	PROGRAM (literalValue | dataTypeDeclaration | directVariable) END_PROGRAM ';'?;

PROGRAM: 'PROGRAM';
END_PROGRAM: 'END_PROGRAM';

namespaceName: IDENTIFIER;

// User defined data type declaraction
dataTypeDeclaration: TYPE ( typeDeclaration ';')+ END_TYPE;
typeDeclaration:
	simpleTypeDeclaration
	| subrangeTypeDeclaration
	| enumTypeDeclaration
	| arrayTypeDeclaration
	| structTypeDeclaration
	| stringTypeDeclaration
	| refTypeDeclaration;

// simple type declaration
simpleTypeDeclaration: simpleTypeName ':' simpleSpecInit;
simpleSpecInit: simpleSpec ( ':=' constExpression)?;
simpleSpec: elementTypeName | simpleTypeAccess;
elementTypeName:
	numericTypeName
	| boolTypeName
	| derivedTypeAccess
	| multibitsTypeName
	| stringTypeName
	| dateTypeName
	| durationTypeName;
numericTypeName: intTypeName | realTypeName;

// subrange type declaration
subrangeTypeDeclaration: subrangeTypeName ':' subrangeSpecInit;
subrangeSpecInit: subrangeSpec ( ':=' (SIGNED_INT | UNSIGNED_INT))?;
subrangeSpec: intTypeName '(' subrange ')' | subrangeTypeAccess;
subrange: constExpression '..' constExpression;

// enumeration type declaration
enumTypeDeclaration:
	enumTypeName ':' (
		( elementTypeName? namedSpecInit)
		| enumSpecInit
	);
namedSpecInit:
	'(' enumValueSpec (',' enumValueSpec)* ')' (':=' enumValue)?;
enumSpecInit: (
		( '(' IDENTIFIER ( ',' IDENTIFIER)* ')')
		| enumTypeAccess
	) (':=' enumValue)?;
enumValueSpec:
	IDENTIFIER (':=' ( intLiteral | constExpression))?;
enumValue: ( enumTypeName '#')? IDENTIFIER;

// array type declaration
arrayTypeDeclaration: arrayTypeName ':' arraySpecInit;
arraySpecInit: arraySpec ( ':=' arrayInit)?;
arraySpec:
	arrayTypeAccess
	| ARRAY '[' subrange (',' subrange)* ']' OF dataTypeAccess;
dataTypeAccess: elementTypeName | derivedTypeAccess;
arrayInit: '[' arrayElementInit ( ',' arrayElementInit)* ']';
arrayElementInit:
	arrayElementInitValue
	| UNSIGNED_INT '(' arrayElementInitValue? ')';
arrayElementInitValue:
	constExpression
	| enumValue
	| structInit
	| arrayInit;

// struct type declaration
structTypeDeclaration: structTypeName ':' structSpec;
structSpec: structDeclaration | structSpecInit;
structSpecInit: structTypeAccess ( ':=' structInit)?;
structDeclaration:
	STRUCT OVERLAP? (structElementDeclaration ';')+ END_STRUCT;
structElementDeclaration:
	structElementName (locatedAt multibitPartAccess?)? ':' (
		simpleSpecInit
		| subrangeSpecInit
		| enumSpecInit
		| arraySpecInit
		| structSpecInit
	);
locatedAt: AT directVariable;
multibitPartAccess:
	'.' (
		UNSIGNED_INT
		| '%' ( 'X' | 'B' | 'W' | 'D' | 'L')? UNSIGNED_INT
	);
structElementName: IDENTIFIER;
structInit: '(' structElementInit ( ',' structElementInit)* ')';
structElementInit:
	structElementName ':=' (
		constExpression
		| enumValue
		| arrayInit
		| structInit
		| refValue
	);

// string type declaration
stringTypeDeclaration:
	stringTypeName ':' stringTypeName (':=' charString)?;

// reference type declaration
refTypeDeclaration: refTypeName ':' refSpecInit;
refSpecInit: refSpec ( ':=' refValue)?;
refSpec: REF_TO+ dataTypeAccess;
refTypeName: IDENTIFIER;
refTypeAccess: ( namespaceName '.')* refTypeName;
ref_Name: IDENTIFIER;
refValue: refAddress | NULL;
refAddress:
	REF '(' (
/*Symbolic_Variable*/
		| /*FB_Instance_Name*/
		| /*Class_Instance_Name*/
	) ')';
refAssign: ref_Name ':=' ( ref_Name | refDereference | refValue);
refDereference: ref_Name '^'+;


// Type accessing
derivedTypeAccess:
	singleElementTypeAccess
	| arrayTypeAccess
	| structTypeAccess
	| stringTypeAccess
/*| classTypeAccess */
	| refTypeAccess
/*| interfaceTypeAccess */;

stringTypeAccess: ( namespaceName '.')* stringTypeName;
stringTypeName: (STRING | WSTRING) ('[' UNSIGNED_INT ']')?
	| CHAR
	| WCHAR;

singleElementTypeAccess:
	simpleTypeAccess
	| subrangeTypeAccess
	| enumTypeAccess;

simpleTypeAccess: ( namespaceName '.')* simpleTypeName;
simpleTypeName: IDENTIFIER;

subrangeTypeAccess: ( namespaceName '.')* subrangeTypeName;
subrangeTypeName: IDENTIFIER;

enumTypeAccess: ( namespaceName '.')* enumTypeName;
enumTypeName: IDENTIFIER;

arrayTypeAccess: ( namespaceName '.')* arrayTypeName;
arrayTypeName: IDENTIFIER;

structTypeAccess: ( namespaceName '.')* structTypeName;
structTypeName: IDENTIFIER;

constExpression: expression;
expression: literalValue;

// Literals
literalValue:
	numericLiteral
	| charLiteral
	| timeLiteral
	| multibitsLiteral
	| boolLiteral;

numericLiteral: intLiteral | realLiteral;

// integer literals
intLiteral: (intTypeName '#')? intLiteralValue;
intLiteralValue:
	SIGNED_INT
	| UNSIGNED_INT
	| BINARY_INT
	| OCTAL_INT
	| HEX_INT;
intTypeName: unsignedIntTypeName | signedIntTypeName;
unsignedIntTypeName: USINT | UINT | UDINT | ULINT;
signedIntTypeName: SINT | INT | DINT | LINT;

// multibits literals
multibitsLiteral: (multibitsTypeName '#')? multibitsLiteralValue;
multibitsLiteralValue:
	UNSIGNED_INT
	| BINARY_INT
	| OCTAL_INT
	| HEX_INT;
multibitsTypeName: BYTE | WORD | DWORD | LWORD;

// real literals
realLiteral: (realTypeName '#')? realLiteralValue;
realLiteralValue: GENERAL_REAL;
realTypeName: REAL | LREAL;

// bool literals
boolLiteral: (boolTypeName '#')? boolLiteralValue;
boolLiteralValue: BOOLEAN | UNSIGNED_INT;
boolTypeName: BOOL;

// character literals
charLiteral: ( charTypeName '#')? charString;
charString: SINGLE_BYTE_STRING | DOUBLE_BYTE_STRING;
charTypeName: STRING | CHAR | WSTRING | WCHAR;

// time literals
timeLiteral:
	durationLiteral
	| timeOfDayLiteral
	| dateLiteral
	| dateAndTimeLiteral;

durationLiteral: (durationTypeName) '#' durationLiteralValue;
durationLiteralValue: DURATION;
durationTypeName: TIME | LTIME | 'T' | 'LT';

timeOfDayLiteral: timeOfDayTypeName '#' timeOfDayLiteralValue;
timeOfDayLiteralValue: CLOCK_TIME;
timeOfDayTypeName: TIME_OF_DAY | LTIME_OF_DAY | 'TOD' | 'LTOD';

dateLiteral: (dateTypeName) '#' dateLiteralValue;
dateLiteralValue: DATE_VALUE;
dateTypeName: DATE | LDATE | 'D' | 'LD';

dateAndTimeLiteral: (dateAndTimeTypeName) '#' dateAndTimeLiteralValue;
dateAndTimeLiteralValue: DATE_TIME_VALUE;
dateAndTimeTypeName:
	DATE_AND_TIME
	| LDATE_AND_TIME
	| 'DT'
	| 'LDT';

// direct variables
directVariable: DIRECT_VARIABLE;

// LEXER /////////////////////////////////////////////////////////////////////////////////////////////
// tu może być problem przy mnożeniu
DIRECT_VARIABLE:
	PERCENT ('I' | 'Q' | 'M')? ('X' | 'B' | 'W' | 'D' | 'L' | '*')? (UNSIGNED_INT (
		DOT UNSIGNED_INT)*)?;
// strings
SINGLE_BYTE_STRING: '\'' SINGLE_BYTE_CHAR* '\'';
DOUBLE_BYTE_STRING: '"' DOUBLE_BYTE_CHAR* '"';

// date and time
DURATION: ('+' | '-')? (DIGIT+ DURATION_UNIT '_'?)+ DIGIT+ (
		DOT DIGIT+
	)? DURATION_UNIT;
DATE_TIME_VALUE: DATE_VALUE '-' CLOCK_TIME;
DATE_VALUE:
	DIGIT DIGIT DIGIT DIGIT '-' DIGIT DIGIT '-' DIGIT DIGIT;
CLOCK_TIME:
	DIGIT DIGIT ':' DIGIT DIGIT ':' UNSIGNED_INT DOT UNSIGNED_INT;

// simple data types //
GENERAL_REAL: ('+' | '-')? UNSIGNED_INT DOT UNSIGNED_INT (
		'E' (SIGNED_INT | UNSIGNED_INT)
	)?;
SIGNED_INT: ( '+' | '-') UNSIGNED_INT;
UNSIGNED_INT: DIGIT ( '_'? DIGIT)*;
BINARY_INT: '2#' ( '_'? BIT)+;
OCTAL_INT: '8#' ( '_'? OCTAL_DIGIT)+;
HEX_INT: '16#' ( '_'? HEX_DIGIT)+;
BOOLEAN: FALSE | TRUE;

// integer names
USINT: 'USINT';
UINT: 'UINT';
UDINT: 'UDINT';
ULINT: 'ULINT';
SINT: 'SINT';
INT: 'INT';
DINT: 'DINT';
LINT: 'LINT';

// real data type names
REAL: 'REAL';
LREAL: 'LREAL';

// multibit data type names
BYTE: 'BYTE';
WORD: 'WORD';
DWORD: 'DWORD';
LWORD: 'LWORD';

// bool type name
BOOL: 'BOOL';
FALSE: 'FALSE';
TRUE: 'TRUE';

// characters type names
STRING: 'STRING';
WSTRING: 'WSTRING';
CHAR: 'CHAR';
WCHAR: 'WCHAR';

// data type names
TIME: 'TIME';
LTIME: 'LTIME';
TIME_OF_DAY: 'TIME_OF_DAY';
LTIME_OF_DAY: 'LTIME_OF_DAY';
DATE: 'DATE';
LDATE: 'LDATE';
DATE_AND_TIME: 'DATE_AND_TIME';
LDATE_AND_TIME: 'LDATE_AND_TIME';

// user defined data types
TYPE: 'TYPE';
END_TYPE: 'END_TYPE';

// arrays
ARRAY: 'ARRAY';
OF: 'OF';

// structures
STRUCT: 'STRUCT';
OVERLAP: 'OVERLAP';
END_STRUCT: 'END_STRUCT';

//dirext variables
AT: 'AT';

// references
REF_TO: 'REF_TO';
REF: 'REF';
NULL: 'NULL';

// special characters
DOT: '.';
PERCENT: '%';

// essential fragments
fragment NON_DIGIT: [a-zA-Z_];
fragment DIGIT: [0-9];
fragment BIT: [0-1];
fragment OCTAL_DIGIT: [0-7];
fragment HEX_DIGIT: [0-9a-fA-F];

fragment SINGLE_BYTE_CHAR:
	COMMON_CHAR
	| '$\''
	| '"'
	| '$' HEX_DIGIT HEX_DIGIT;

fragment DOUBLE_BYTE_CHAR:
	COMMON_CHAR
	| '\''
	| '$"'
	| '$' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT;

fragment COMMON_CHAR:
	[ !#%&]
	| '(' ..'/'
	| DIGIT
	| NON_DIGIT
	| [:-@]
	| '[' ..'`'
	| '{' ..'~'
	| TWO_CHAR_COMMON;

fragment TWO_CHAR_COMMON:
	'$$'
	| '$L'
	| '$N'
	| '$P'
	| '$R'
	| '$T';

fragment DURATION_UNIT:
	'D'
	| 'H'
	| 'M'
	| 'S'
	| 'MS'
	| 'US'
	| 'NS';

// identifiers
IDENTIFIER: NON_DIGIT (NON_DIGIT | DIGIT)*;

//miejsce jest waażne, w konkretnych miejscach są 

// pragmas 
PRAGMA: '{' .*? '}' -> channel(HIDDEN);

// comments
LINE_COMMENT: '//' ~[\r\n]* -> channel(HIDDEN);
SLASH_COMMENT:
	'/*' (SLASH_COMMENT | .)*? '*/' -> channel(HIDDEN);
BRACE_COMMENT:
	'(*' (BRACE_COMMENT | .)*? '*)' -> channel(HIDDEN);

// whitespaces
WHITESPACE: [ \t\r\n]+ -> channel(HIDDEN);