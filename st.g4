grammar st;

// Parser //

// Starting rule
file:
	(
		pouDeclaration
		| usingDirective* (
			namespaceDeclaration
			| globalVarDeclarations
			| dataTypeDeclaration
			| classDeclaration
			| interfaceDeclaration
		)
	)+;

// POU declarations
pouDeclaration:
	usingDirective* (
		functionDeclaration
		| functionBlockDeclaration
		| programDeclaration
	);

// Namespace declaration
namespaceDeclaration:
	NAMESPACE INTERNAL? namespaceAccess usingDirective* namespaceElements END_NAMESPACE;
namespaceElements: (
		dataTypeDeclaration
		| functionDeclaration
		| functionBlockDeclaration
		| classDeclaration
		| interfaceDeclaration
		| namespaceDeclaration
	)+;

namespaceAccess: namespaceName (DOT namespaceName)*;
namespaceName: IDENTIFIER;

usingDirective:
	USING namespaceAccess (COMMA namespaceAccess)* SEMICOLON;

// Program declaration
programDeclaration:
	PROGRAM programName (
		ioVarDeclarations
		| externalVarDeclarations
		| normalVarDeclarations
		| tempVarDeclarations
		| otherVarDeclarations
		| locatedVarDeclarations
	)* programBody END_PROGRAM;

programName: IDENTIFIER;
programNameAccess: (namespaceName DOT)* programName;
programBody: statementList;

// User defined data type declaraction
dataTypeDeclaration: TYPE (typeDeclaration SEMICOLON)+ END_TYPE;
typeDeclaration:
	simpleTypeDeclaration
	| subrangeTypeDeclaration
	| enumTypeDeclaration
	| arrayTypeDeclaration
	| structTypeDeclaration
	| stringTypeDeclaration
	| referenceTypeDeclaration;

// Simple type declaration 
simpleTypeDeclaration:
	simpleTypeName COLON simpleSpecificationInit;
simpleTypeName: IDENTIFIER;

simpleSpecificationInit:
	simpleSpecification (ASSIGN simpleInit)?;
simpleInit: expression;
simpleSpecification: elementaryTypeName | simpleTypeAccess;

elementaryTypeName:
	intTypeName
	| realTypeName
	| boolTypeName
	| multibitsTypeName
	| stringTypeName
	| dateTypeName
	| timeOfDayTypeName
	| dateAndTimeTypeName
	| durationTypeName;

simpleTypeAccess: (namespaceName DOT)* simpleTypeName;

stringTypeName: (STRING | WSTRING) (
		LEFT_BRACKET stringSize RIGHT_BRACKET
	)?
	| CHAR
	| WCHAR;
stringSize: UNSIGNED_INT;

// Subrange type declaration 
subrangeTypeDeclaration:
	subrangeTypeName COLON subrangeSpecificationInit;

subrangeTypeName: IDENTIFIER;
subrangeSpecificationInit:
	subrangeSpecification (ASSIGN subrangeInit)?;
subrangeSpecification:
	intTypeName LEFT_PAREN subrange RIGHT_PAREN
	| subrangeTypeAccess;
subrangeInit: signOperator? UNSIGNED_INT;

subrangeTypeAccess: (namespaceName DOT)* subrangeTypeName;
subrange: subrangeBegin RANGE subrangeEnd;
subrangeBegin: expression;
subrangeEnd: expression;

// Enumeration type declaration
enumTypeDeclaration:
	enumTypeName COLON (
		elementaryTypeName? enumNamedSpecificationInit
		| enumSpecificationInit
	);

enumTypeName: IDENTIFIER;
enumNamedSpecificationInit:
	LEFT_PAREN enumElementSpecificationInit (
		COMMA enumElementSpecificationInit
	)* RIGHT_PAREN (ASSIGN enumValue)?;

enumElementSpecificationInit:
	enumElementName (ASSIGN enumElementValue)?;
enumElementName: IDENTIFIER;
enumElementValue: intLiteral | expression;

enumValue: (enumTypeName HASH)? enumElementName;

enumSpecificationInit: (
		LEFT_PAREN enumElementName (COMMA enumElementName)* RIGHT_PAREN
		| enumTypeAccess
	) (ASSIGN enumValue)?;
enumTypeAccess: ( namespaceName DOT)* enumTypeName;

// Array type declaration
arrayTypeDeclaration:
	arrayTypeName COLON arraySpecificationInit;
arrayTypeName: IDENTIFIER;
arraySpecificationInit: arraySpecification (ASSIGN arrayInit)?;
arraySpecification:
	arrayTypeAccess
	| ARRAY LEFT_BRACKET subrange (COMMA subrange)* RIGHT_BRACKET OF dataTypeAccess;
arrayTypeAccess: (namespaceName DOT)* arrayTypeName;

arrayInit:
	LEFT_BRACKET arrayElementInit (COMMA arrayElementInit)* RIGHT_BRACKET;
arrayElementInit:
	arrayElementInitValue
	| arrayElementMultiplier LEFT_PAREN arrayElementInitValue? RIGHT_PAREN;
arrayElementMultiplier: UNSIGNED_INT;
arrayElementInitValue:
	expression
	| enumValue
	| structInit
	| arrayInit;

// variable length Array IO declaration
arrayConformDeclaration: variableList COLON arrayConformand;
arrayConformand:
	ARRAY LEFT_BRACKET ASTERISK (COMMA ASTERISK)* RIGHT_BRACKET OF dataTypeAccess;

// array type variable declaration
arrayVarDeclarationInit:
	variableList COLON arraySpecificationInit;

// Struct type declaration 
structTypeDeclaration:
	structTypeName COLON structTypeSpecification;
structTypeName: IDENTIFIER;

structTypeSpecification:
	structDeclaration
	| structSpecificationInit;

structDeclaration:
	STRUCT OVERLAP? (structElementDeclaration SEMICOLON)+ END_STRUCT;

structElementDeclaration:
	structElementName (locatedAt multibitPartAccess?)? COLON (
		simpleSpecificationInit
		| subrangeSpecificationInit
		| enumSpecificationInit
		| arraySpecificationInit
		| structSpecificationInit
	);

structElementName: IDENTIFIER;
locatedAt: AT (relativeAddress | partlySpecifiedAddress);
relativeAddress: RELATIVE_ADDRESS;
partlySpecifiedAddress: DIRECT_VARIABLE;
multibitPartAccess: DOT (UNSIGNED_INT | RELATIVE_ADDRESS);

structSpecificationInit:
	structSpecification (ASSIGN structInit)?;
structSpecification: structTypeAccess;
structTypeAccess: (namespaceName DOT)* structTypeName;
structInit:
	LEFT_PAREN structElementInit (COMMA structElementInit)* RIGHT_PAREN;
structElementInit:
	structElementName ASSIGN (
		expression
		| enumValue
		| arrayInit
		| structInit
		| referenceValue
	);
// struct type variable declaration
structVarDeclarationInit:
	variableList COLON structSpecificationInit;

// String type declaration 
stringTypeDeclaration:
	stringDerivedTypeName COLON stringTypeName (
		ASSIGN charString
	)?;
stringDerivedTypeName: IDENTIFIER;
stringTypeAccess: (namespaceName DOT)* stringDerivedTypeName;

// Reference type declaration
referenceTypeDeclaration:
	referenceTypeName COLON referenceSpecificationInit;

referenceTypeName: IDENTIFIER;
referenceSpecificationInit:
	referenceSpecification (ASSIGN referenceValue)?;
referenceSpecification: REF_TO+ dataTypeAccess;

referenceValue: referenceAddress | NULL;
referenceAddress:
	REF LEFT_PAREN (
		symbolicVariable
		| functionBlockInstanceName
		| classInstanceName
	) RIGHT_PAREN;

referenceTypeAccess: (namespaceName DOT)* referenceTypeName;
referenceName: IDENTIFIER;
dereference: referenceName CARET+;

// Funtion Block type declaration
functionBlockDeclaration:
	FUNCTION_BLOCK (FINAL | ABSTRACT)? functionBlockName usingDirective* (
		EXTENDS (functionBlockTypeAccess | classTypeAccess)
	)? (IMPLEMENTS interfaceTypeList)? (
		ioVarDeclarations
		| externalVarDeclarations
		| normalVarDeclarations
		| tempVarDeclarations
		| otherVarDeclarations
	)* (methodDeclaration)* functionBlockBody END_FUNCTION_BLOCK;

functionBlockName: IDENTIFIER;
functionBlockInstanceName: (namespaceName DOT)* functionBlockName CARET*;

functionBlockTypeAccess: (namespaceName DOT)* functionBlockTypeName;
functionBlockTypeName: IDENTIFIER;
functionBlockBody: statementList;

functionBlockVarDeclarationInit:
	variableList COLON functionBlockVarSpecificationInit;
functionBlockVarSpecificationInit:
	functionBlockTypeAccess (ASSIGN structInit)?;

// Method declaration
methodDeclaration:
	METHOD accessSpecification? (FINAL | ABSTRACT)? OVERRIDE? methodName (
		COLON dataTypeAccess
	)? (
		ioVarDeclarations
		| externalVarDeclarations
		| normalVarDeclarations
		| tempVarDeclarations
	)* functionBody END_METHOD;
methodName: IDENTIFIER;

// Type accessing
dataTypeAccess: elementaryTypeName | derivedTypeAccess;

derivedTypeAccess:
	simpleTypeAccess
	| subrangeTypeAccess
	| enumTypeAccess
	| arrayTypeAccess
	| structTypeAccess
	| stringTypeAccess
	| classTypeAccess
	| referenceTypeAccess
	| interfaceTypeAccess;

// Variable access
variable: directVariable | symbolicVariable;
symbolicVariable: (THIS DOT | (namespaceName DOT)+)? (
		variableAccess (variableElementSelect)*
	);
variableAccess: variableName | dereference;
variableName: IDENTIFIER;

variableElementSelect: subscriptList | COMMA variableAccess;
subscriptList:
	LEFT_BRACKET expression (COMMA expression)* RIGHT_BRACKET;

// Input variable declarations [!!!]
ioVarDeclarations:
	inputVarDeclarations
	| outputVarDeclarations
	| inOutVarDeclarations;

inputVarDeclarations:
	VAR_INPUT (RETAIN | NON_RETAIN)? (
		inputVarDeclaration SEMICOLON
	)* END_VAR;
inputVarDeclaration:
	varDeclarationInit
	| edgeDeclaration
	| arrayConformDeclaration;

edgeDeclaration: variableList COLON BOOL (R_EDGE | F_EDGE);

varDeclarationInit:
	variableList COLON (
		simpleSpecificationInit
		| stringVarDeclarationInit
		| referenceSpecificationInit
	)
	| arrayVarDeclarationInit
	| structVarDeclarationInit
	| functionBlockVarDeclarationInit
	| interfaceVarDeclarationInit;

variableList: variableName (COMMA variableName)*;

// Output variables declaration
outputVarDeclarations:
	VAR_OUTPUT (RETAIN | NON_RETAIN)? (
		outputVarDeclaration SEMICOLON
	)* END_VAR;
outputVarDeclaration:
	varDeclarationInit
	| arrayConformDeclaration;

// InOut variables declaration
inOutVarDeclarations:
	VAR_IN_OUT (inOutVarDeclaration SEMICOLON)* END_VAR;
inOutVarDeclaration:
	varDeclarationInit
	| arrayConformDeclaration;

// Normal variables declaration
normalVarDeclarations:
	VAR CONSTANT? accessSpecification? (
		varDeclarationInit SEMICOLON
	)* END_VAR;

// Other normal variables delaration
otherVarDeclarations:
	retainVarDeclarations
	| nonRetainVarDeclarations
	| locatedPartlyVarDeclaration;

nonRetainVarDeclarations:
	VAR NON_RETAIN accessSpecification? (
		varDeclarationInit SEMICOLON
	)* END_VAR;
retainVarDeclarations:
	VAR RETAIN accessSpecification? (
		varDeclarationInit SEMICOLON
	)* END_VAR;

locatedPartlyVarDeclaration:
	VAR (RETAIN | NON_RETAIN)? locatedPartlyVar* END_VAR;
locatedPartlyVar:
	variableName AT RELATIVE_ADDRESS COLON varSpecification SEMICOLON;
varSpecification:
	simpleSpecification
	| arraySpecification
	| structTypeAccess
	| stringSpecification;

// Fully located variable declaration
locatedVarDeclarations:
	VAR (CONSTANT | RETAIN | NON_RETAIN)? (
		locatedVarDeclaration SEMICOLON
	)* END_VAR;
locatedVarDeclaration:
	variableName? locatedAt COLON locatedVarSpecificationInit;

// Temporary variables declaration
tempVarDeclarations:
	VAR_TEMP ((varDeclarationInit) SEMICOLON)* END_VAR;

// External variables declaration
externalVarDeclarations:
	VAR_EXTERNAL CONSTANT? (externalDeclaration SEMICOLON)* END_VAR;
externalDeclaration:
	globalVarName COLON (
		simpleSpecification
		| arraySpecification
		| structTypeAccess
		| functionBlockTypeAccess
		| referenceTypeAccess
	);

// Global variables
globalVarName: IDENTIFIER;
globalVarDeclarations:
	VAR_GLOBAL (CONSTANT | RETAIN)? (
		globalVarDeclaration SEMICOLON
	)* END_VAR;
globalVarDeclaration:
	globalVarSpecification COLON (
		locatedVarSpecificationInit
		| functionBlockTypeAccess
	);

globalVarSpecification:
	globalVarName (COMMA globalVarName)*
	| globalVarName locatedAt;

locatedVarSpecificationInit:
	simpleSpecificationInit
	| arraySpecificationInit
	| structSpecificationInit
	| stringSpecificationInit;

stringVarDeclarationInit:
	variableList COLON stringSpecificationInit;
stringSpecificationInit: stringSpecification stringInit?;
stringSpecification: (STRING | WSTRING) (
		LEFT_BRACKET stringSize RIGHT_BRACKET
	)?;
stringInit: SINGLE_BYTE_STRING | DOUBLE_BYTE_STRING;

// Funtions
functionDeclaration:
	FUNCTION functionName (COLON dataTypeAccess)? usingDirective* (
		ioVarDeclarations
		| externalVarDeclarations
		| normalVarDeclarations
		| tempVarDeclarations
	)* functionBody END_FUNCTION;

functionBody: statementList;

// Class declaration
classDeclaration:
	CLASS (FINAL | ABSTRACT)? classTypeName usingDirective* (
		EXTENDS classTypeAccess
	)? (IMPLEMENTS interfaceTypeList)? (
		externalVarDeclarations
		| normalVarDeclarations
		| otherVarDeclarations
	)* (methodDeclaration)* END_CLASS;
classTypeName: IDENTIFIER;
classTypeAccess: (namespaceName DOT)* classTypeName;

className: IDENTIFIER;
classInstanceName: (namespaceName DOT)* className CARET*;

accessSpecification: PUBLIC | PROTECTED | PRIVATE | INTERNAL;

// Interface declaration
interfaceDeclaration:
	INTERFACE interfaceName usingDirective* (
		EXTENDS interfaceTypeList
	)? methodPrototype* END_INTERFACE;

interfaceName: IDENTIFIER;
interfaceTypeList:
	interfaceTypeAccess (COLON interfaceTypeAccess)*;
interfaceTypeAccess: (namespaceName DOT)* interfaceName;

methodPrototype:
	METHOD methodName (COLON dataTypeAccess)? ioVarDeclarations* END_METHOD;

interfaceVarDeclarationInit:
	variableList COLON interfaceSpecificationInit;
interfaceSpecificationInit:
	interfaceTypeAccess (ASSIGN interfaceValue)?;
interfaceValue:
	symbolicVariable
	| functionBlockInstanceName
	| classInstanceName
	| NULL;

// Statements
statementList: (statement? SEMICOLON)*;
statement:
	assignStatement
	| subprogControlStatement
	| selectionStatement
	| loopStatement;

assignStatement: variable assignOperator expression;

assignOperator: ASSIGN | ATTEMPT_ASSIGN;

subprogControlStatement:
	functionCall
	| invocationStatement
	| superCallStatement
	| returnStatement;

functionCall:
	functionAccess LEFT_PAREN (
		parameterAssign (COMMA parameterAssign)*
	)? RIGHT_PAREN;

functionAccess: (namespaceName DOT)* functionName;
functionName: IDENTIFIER;

invocationStatement: (
		functionBlockInstanceName
		| (THIS DOT)? (
			((functionBlockInstanceName | classInstanceName) DOT)+
		) methodName
	) LEFT_PAREN (parameterAssign (COMMA parameterAssign)*)? RIGHT_PAREN;

parameterAssign: ((variableName ASSIGN)? expression)
	| (NOT? variableName ASSIGN_OUT variable);

superCallStatement: SUPER LEFT_PAREN RIGHT_PAREN;
returnStatement: RETURN;

// Selection statements
selectionStatement: ifStatement | caseStatement;

// If
ifStatement:
	IF expression THEN statementList (
		ELSIF expression THEN statementList
	)* (ELSE statementList)? END_IF;

// Case
caseStatement:
	CASE expression OF caseSelection+ (ELSE statementList)? END_CASE;
caseSelection: caseList COLON statementList;
caseList: caseListElement (COMMA caseListElement)*;
caseListElement: subrange | expression;

// Iteration statements
loopStatement:
	forStatement
	| whileStatement
	| repeatStatement
	| exitStatement
	| continueStatement;

// For
forStatement:
	FOR controlVariable ASSIGN forRange DO statementList END_FOR;
controlVariable: IDENTIFIER;
forRange: expression TO expression (BY expression)?;

// While
whileStatement: WHILE expression DO statementList END_WHILE;

// Repeat
repeatStatement:
	REPEAT statementList UNTIL expression END_REPEAT;

// Expressions 
expression:
	(literalValue | variableValue | enumValue | referenceValue)	# primaryExpression
	| LEFT_PAREN expression RIGHT_PAREN							# bracketedExpression
	| functionCall												# funcCallExpression
	| expression derefOperator+									# derefExpression
	| unaryOperator expression									# unaryExpression
	| <assoc = right> expression exponentOperator expression	# exponentExpression
	| expression multDivModOperator expression					# multDivModExpression
	| expression addSubOperator expression						# addSubExpression
	| expression comparisonOperator expression					# comparisonExpression
	| expression andOperator expression							# andExpression
	| expression xorOperator expression							# xorExpression
	| expression orOperator expression							# orExpression;

derefOperator: CARET;
unaryOperator: signOperator | NOT;
signOperator: PLUS | MINUS;
exponentOperator: POWER;
multDivModOperator: ASTERISK | SLASH | MOD;
addSubOperator: PLUS | MINUS;
comparisonOperator:
	LESS
	| GREATER
	| LESS_EQUAL
	| GREATER_EQUAL
	| EQUAL
	| NOT_EQUAL;
andOperator: AMPERSAND | AND;
xorOperator: XOR;
orOperator: OR;

// Exit
exitStatement: EXIT;

// Continue
continueStatement: CONTINUE;

variableValue: variable multibitPartAccess?;

// Literals 
literalValue:
	intLiteral
	| realLiteral
	| charLiteral
	| timeLiteral
	| multibitsLiteral
	| boolLiteral;

// Integer literals
intLiteral: (intTypeName HASH)? intLiteralValue;
intLiteralValue:
	signOperator? UNSIGNED_INT
	| BINARY_INT
	| OCTAL_INT
	| HEX_INT;
intTypeName:
	USINT
	| UINT
	| UDINT
	| ULINT
	| SINT
	| INT
	| DINT
	| LINT;

// Multibit literals
multibitsLiteral: (multibitsTypeName HASH)? multibitsLiteralValue;
multibitsLiteralValue:
	UNSIGNED_INT
	| BINARY_INT
	| OCTAL_INT
	| HEX_INT;
multibitsTypeName: BYTE | WORD | DWORD | LWORD;

// Real literals
realLiteral: (realTypeName HASH)? realLiteralValue;
realLiteralValue: signOperator? UNSIGNED_REAL_VALUE;
realTypeName: REAL | LREAL;

// Boolean literals
boolLiteral: (boolTypeName HASH)? boolLiteralValue;
boolLiteralValue: BOOLEAN | UNSIGNED_INT;
boolTypeName: BOOL;

// Characters and Strings literals
charLiteral: (charStringTypeName HASH)? charString;
charString: SINGLE_BYTE_STRING | DOUBLE_BYTE_STRING;
charStringTypeName: CHAR | STRING | WCHAR | WSTRING;

// Time literals 
timeLiteral:
	durationLiteral
	| timeOfDayLiteral
	| dateLiteral
	| dateAndTimeLiteral;

// Duration literals
durationLiteral: (durationTypeName) HASH durationLiteralValue;
durationLiteralValue: signOperator? UNSIGNED_DURATION;
durationTypeName: TIME | LTIME;

// Time of Day literals
timeOfDayLiteral: timeOfDayTypeName HASH timeOfDayLiteralValue;
timeOfDayLiteralValue: CLOCK_TIME;
timeOfDayTypeName: TIME_OF_DAY | LTIME_OF_DAY;

// Date literals
dateLiteral: (dateTypeName) HASH dateLiteralValue;
dateLiteralValue: DATE_VALUE;
dateTypeName: DATE | LDATE;

// Date and Time literals
dateAndTimeLiteral: (dateAndTimeTypeName) HASH dateAndTimeLiteralValue;
dateAndTimeLiteralValue: DATE_TIME_VALUE;
dateAndTimeTypeName: DATE_AND_TIME | LDATE_AND_TIME;

// Direct variables access 
directVariable: DIRECT_VARIABLE;

// Lexer // 

// Adresy zmiennych
RELATIVE_ADDRESS:
	'%' ('X' | 'B' | 'W' | 'D' | 'L')? UNSIGNED_INT;
DIRECT_VARIABLE:
	'%' ('I' | 'Q' | 'M') ('X' | 'B' | 'W' | 'D' | 'L' | '*')? (
		UNSIGNED_INT ( '.' UNSIGNED_INT)*
	)?;

// Literały  ciągów znakowych
SINGLE_BYTE_STRING: '\'' SINGLE_BYTE_CHAR* '\'';
DOUBLE_BYTE_STRING: '"' DOUBLE_BYTE_CHAR* '"';

// Literały czasu i daty
UNSIGNED_DURATION: (DIGIT+ DURATION_UNIT '_'?)+ DIGIT+ (
		'.' DIGIT+
	)? DURATION_UNIT;
DATE_TIME_VALUE: DATE_VALUE '-' CLOCK_TIME;
DATE_VALUE:
	DIGIT DIGIT DIGIT DIGIT '-' DIGIT DIGIT '-' DIGIT DIGIT;
CLOCK_TIME:
	DIGIT DIGIT? ':' DIGIT DIGIT (
		':' UNSIGNED_INT ('.' UNSIGNED_INT)?
	)?;

// Literały liczbowe
UNSIGNED_REAL_VALUE:
	UNSIGNED_INT '.' UNSIGNED_INT ('E' ('+' | '-')? UNSIGNED_INT)?;
UNSIGNED_INT: DIGIT ( '_'? DIGIT)*;
BINARY_INT: '2#' ( '_'? BIT)+;
OCTAL_INT: '8#' ( '_'? OCTAL_DIGIT)+;
HEX_INT: '16#' ( '_'? HEX_DIGIT)+;
BOOLEAN: FALSE | TRUE;

// Słowa kluczowe typów całkowitych
USINT: 'USINT';
UINT: 'UINT';
UDINT: 'UDINT';
ULINT: 'ULINT';
SINT: 'SINT';
INT: 'INT';
DINT: 'DINT';
LINT: 'LINT';

// Słowa kluczowe typów rzeczywistych
REAL: 'REAL';
LREAL: 'LREAL';

// Słowa kluczowe typów bitowych
BYTE: 'BYTE';
WORD: 'WORD';
DWORD: 'DWORD';
LWORD: 'LWORD';

// Bool type names
BOOL: 'BOOL';
FALSE: 'FALSE';
TRUE: 'TRUE';

// Słowa kluczowe typów znakowych
STRING: 'STRING';
WSTRING: 'WSTRING';
CHAR: 'CHAR';
WCHAR: 'WCHAR';

// Słowa kluczowe typów czasu i daty
TIME: 'TIME' | 'T';
LTIME: 'LTIME' | 'LT';
TIME_OF_DAY: 'TIME_OF_DAY' | 'TOD';
LTIME_OF_DAY: 'LTIME_OF_DAY' | 'LTOD';
DATE: 'DATE' | 'D';
LDATE: 'LDATE' | 'LD';
DATE_AND_TIME: 'DATE_AND_TIME' | 'DT';
LDATE_AND_TIME: 'LDATE_AND_TIME' | 'LDT';

// Słowa kluczowe typów definiowanych przez użytkownika
TYPE: 'TYPE';
END_TYPE: 'END_TYPE';

// Słowa kluczowe tablic
ARRAY: 'ARRAY';
OF: 'OF';

// Słowa kluczowe struktur
STRUCT: 'STRUCT';
OVERLAP: 'OVERLAP';
END_STRUCT: 'END_STRUCT';

// Słowa kluczowe referencji
REF_TO: 'REF_TO';
REF: 'REF';
NULL: 'NULL';

// Słowa kluczowe deklaracji zmiennych
VAR_INPUT: 'VAR_INPUT';
R_EDGE: 'R_EDGE';
F_EDGE: 'F_EDGE';
VAR_OUTPUT: 'VAR_OUTPUT';
VAR_IN_OUT: 'VAR_IN_OUT';
RETAIN: 'RETAIN';
NON_RETAIN: 'NON_RETAIN';
END_VAR: 'END_VAR';
VAR: 'VAR';
CONSTANT: 'CONSTANT';
VAR_TEMP: 'VAR_TEMP';
VAR_EXTERNAL: 'VAR_EXTERNAL';
VAR_GLOBAL: 'VAR_GLOBAL';
AT: 'AT';

// Słowa kluczowe funkcji
FUNCTION: 'FUNCTION';
END_FUNCTION: 'END_FUNCTION';

// Słowa kluczowe obiektów z pamięcią
FUNCTION_BLOCK: 'FUNCTION_BLOCK';
FINAL: 'FINAL';
ABSTRACT: 'ABSTRACT';
EXTENDS: 'EXTENDS';
IMPLEMENTS: 'IMPLEMENTS';
END_FUNCTION_BLOCK: 'END_FUNCTION_BLOCK';
METHOD: 'METHOD';
THIS: 'THIS';
OVERRIDE: 'OVERRIDE';
END_METHOD: 'END_METHOD';
CLASS: 'CLASS';
END_CLASS: 'END_CLASS';
INTERFACE: 'INTERFACE';
END_INTERFACE: 'END_INTERFACE';
PUBLIC: 'PUBLIC';
PROTECTED: 'PROTECTED';
PRIVATE: 'PRIVATE';
INTERNAL: 'INTERAL';
SUPER: 'SUPER';
RETURN: 'RETURN';

// Słowa kluczowe programów
PROGRAM: 'PROGRAM';
END_PROGRAM: 'END_PROGRAM';
VAR_ACCESS: 'VAR_ACCESS';

// Słowa kluczowe przestrzeni nazw
NAMESPACE: 'NAMESPACE';
END_NAMESPACE: 'END_NAMESPACE';
USING: 'USING';

// Słowa kluczowe innstrukcji warunkowych
IF: 'IF';
THEN: 'THEN';
ELSIF: 'ELSIF';
ELSE: 'ELSE';
END_IF: 'END_IF';
CASE: 'CASE';
END_CASE: 'END_CASE';

// Słowa kluczowe instrukcji pętli
EXIT: 'EXIT';
CONTINUE: 'CONTINUE';
FOR: 'FOR';
TO: 'TO';
BY: 'BY';
DO: 'DO';
END_FOR: 'END_FOR';
WHILE: 'WHILE';
END_WHILE: 'END_WHILE';
REPEAT: 'REPEAT';
UNTIL: 'UNTIL';
END_REPEAT: 'END_REPEAT';

// Operatory arytmetyczne
PLUS: '+';
MINUS: '-';
ASTERISK: '*';
POWER: '**';
SLASH: '/';
MOD: 'MOD';

// Operatory relacyjne
EQUAL: '=';
NOT_EQUAL: '<>';
LESS: '<';
LESS_EQUAL: '<=';
GREATER: '>';
GREATER_EQUAL: '>=';

// Operaotry logiczne
AND: 'AND';
OR: 'OR';
NOT: 'NOT';
XOR: 'XOR';
AMPERSAND: '&';

// Operatory przypisania
ASSIGN: ':=';
ASSIGN_OUT: '=>';
ATTEMPT_ASSIGN: '?=';

// Separatory
LEFT_PAREN: '(';
RIGHT_PAREN: ')';
LEFT_BRACKET: '[';
RIGHT_BRACKET: ']';
COMMA: ',';
COLON: ':';
SEMICOLON: ';';
DOT: '.';
RANGE: '..';

// Znaki specjalne
HASH: '#';
CARET: '^';
PERCENT: '%';
UNDERSCORE: '_';

// Numeric fragments
fragment NON_DIGIT: [a-zA-Z_];
fragment DIGIT: [0-9];
fragment BIT: [0-1];
fragment OCTAL_DIGIT: [0-7];
fragment HEX_DIGIT: [0-9a-fA-F];

// Character string fragments
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

// Identyfikatory
IDENTIFIER: NON_DIGIT (NON_DIGIT | DIGIT)*;

// Dyrektywy CPDev
CPDEV_AUTO: '(*$AUTO*)' -> channel(HIDDEN);
CPDEV_READ: '(*$READ*)' -> channel(HIDDEN);
CPDEV_WRITE: '(*$WRITE*)' -> channel(HIDDEN);

// Dyrektywy CPDev z parametrami
CPDEV_COMMENT: '(*$COMMENT' .*? '*)' -> channel(HIDDEN);
CPDEV_VMASM: '(*$VMASM' .*? '*)' -> channel(HIDDEN);

// Dyrektywy języka
PRAGMA: '{' .*? '}' -> channel(HIDDEN);

// Komentarze
LINE_COMMENT: '//' ~[\r\n]* -> channel(HIDDEN);
SLASH_COMMENT:
	'/*' (SLASH_COMMENT | .)*? '*/' -> channel(HIDDEN);
BRACE_COMMENT:
	'(*' (BRACE_COMMENT | .)*? '*)' -> channel(HIDDEN);

// Białe znaki
WHITESPACE: [ \t\r\n]+ -> channel(HIDDEN);