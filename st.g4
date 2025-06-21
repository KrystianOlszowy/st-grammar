grammar st;

// TO DO:

// Parser //

// Starting rule
file:
	/* configurationDeclaration
 |
	 */
	(
		pouDeclaration
		| usingDirective* (
			namespaceDeclaration
			| globalVarDeclarations
			| dataTypeDeclaration
			| classDeclaration
			| interfaceDeclaration
/*| accessDeclaration */
		)
	)+;

// configuration and resource declaration
/* 
 configurationName: IDENTIFIER;
 resourceTypeName: IDENTIFIER;
 configurationDeclaration:
 CONFIGURATION configurationName globalVarDeclarations? (
 singleResourceDeclaration
 |
 resourceDeclaration+
 ) accessDeclarations? configInit? END_CONFIGURATION;
 resourceDeclaration:
 RESOURCE resourceName ON resourceTypeName globalVarDeclarations? singleResourceDeclaration
 END_RESOURCE;
 singleResourceDeclaration: (taskConfig ';')* (programConfig ';')+;
 resourceName:
 IDENTIFIER;
 accessDeclarations: VAR_ACCESS ( accessDeclaration ';')* END_VAR;
 accessDeclaration:
 accessName ':' accessPath ':' dataTypeAccess accessDirection?;
 accessPath: (resourceName '.')?
 directVariable
 | (resourceName '.')? (programName '.')? (
 ( fbInstanceName | classInstanceName)
 '.'
 )* symbolicVariable;
 globalVarAccess: (resourceName '.')? globalVarName (
 '.'
 structElementName
 )?;
 accessName: IDENTIFIER;
 programOutputAccess: programName '.'
 symbolicVariable;
 accessDirection: READ_WRITE | READ_ONLY;
 taskConfig: TASK taskName taskInit;
 taskName: IDENTIFIER;
 taskInit:
 '(' (SINGLE ':=' dataSource ',')? (
 INTERVAL ':=' dataSource
 ','
 )? PRIORITY ':=' UNSIGNED_INT ')';
 dataSource:
 literalValue
 | globalVarAccess
 |
 programOutputAccess
 | directVariable;
 programConfig:
 PROGRAM (RETAIN | NON_RETAIN)? programName
 (WITH taskName)? ':' programNameAccess (
 '(' programConfigurationElements ')'
 )?;
 programConfigurationElements:
 programConfigurationElement (',' programConfigurationElement)*;
 programConfigurationElement: fbTask | programCnxn;
 fbTask: fbInstanceName WITH taskName;
 programCnxn:
 symbolicVariable ':=' programDataSource
 | symbolicVariable '=>' dataSink;
 programDataSource:
 literalValue
 | enumValue
 | globalVarAccess
 | directVariable;
 dataSink:
 globalVarAccess | directVariable;
 configInit: VAR_CONFIG ( configInstInit ';')* END_VAR;
 configInstInit:
 resourceName '.' programName '.' (
 ( fbInstanceName | classInstanceName) '.'
 )*
 (
 variableName locatedAt? ':' locVarSpecInit
 | (
 ( fbInstanceName ':' fbTypeAccess)
 | (
 classInstanceName ':' classTypeAccess)
 ) ':=' structInit
 );
 */

// POU declarations
pouDeclaration:
	usingDirective* (
		functionDeclaration
		| fbDeclaration
		| programDeclaration
	);

// Namespace declaration
namespaceDeclaration:
	NAMESPACE INTERNAL? namespaceAccess usingDirective* namespaceElements END_NAMESPACE;
namespaceElements: (
		dataTypeDeclaration
		| functionDeclaration
		| fbDeclaration
		| classDeclaration
		| interfaceDeclaration
		| namespaceDeclaration
	)+;

namespaceAccess: namespaceName ( DOT namespaceName)*;
namespaceName: IDENTIFIER;

usingDirective:
	USING namespaceAccess (COMMA namespaceAccess)* SEMICOLON;

// Program declaration
programDeclaration:
	PROGRAM programName (
		ioVarDeclarations
		| functionVarDeclarations
		| tempVarDeclarations
		| otherVarDeclarations
		| locVarDeclarations
		/*| programAccessDeclarations*/
	)* statementList END_PROGRAM;

programName: IDENTIFIER;
programNameAccess: ( namespaceName DOT)* programName;

/*programAccessDeclarations:
 VAR_ACCESS (programAccessDeclaration SEMICOLON)* END_VAR;
 programAccessDeclaration:
 accessName
 COLON symbolicVariable multibitPartAccess? COLON dataTypeAccess accessDirection?;
 */

// User defined data type declaraction
dataTypeDeclaration: TYPE ( typeDeclaration SEMICOLON)+ END_TYPE;
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
	simpleSpecification (ASSIGN expression)?;
simpleSpecification: elementaryTypeName | simpleTypeAccess;

elementaryTypeName:
	intTypeName
	| realTypeName
	| boolTypeName
	| multibitsTypeName
	| stringTypeName
	| dateTypeName
	| dateAndTimeTypeName
	| durationTypeName;

simpleTypeAccess: ( namespaceName DOT)* simpleTypeName;

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
	subrangeSpecification (ASSIGN subrangeValue)?;

subrangeValue: SIGNED_INT | UNSIGNED_INT;
subrangeSpecification:
	intTypeName LEFT_PAREN subrange RIGHT_PAREN
	| subrangeTypeAccess;
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
	LEFT_PAREN enumValueSpecification (
		COMMA enumValueSpecification
	)* RIGHT_PAREN (ASSIGN enumValue)?;

enumValueSpecification:
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
arrayTypeAccess: ( namespaceName DOT)* arrayTypeName;

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

// Struct type declaration 
structTypeDeclaration: structTypeName COLON structSpecification;
structTypeName: IDENTIFIER;

structSpecification:
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

structSpecificationInit: structTypeAccess (ASSIGN structInit)?;
structTypeAccess: ( namespaceName DOT)* structTypeName;
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
		| fbInstanceName
		| classInstanceName
	) RIGHT_PAREN;

referenceTypeAccess: (namespaceName DOT)* referenceTypeName;

referenceAssign:
	referenceName ASSIGN (
		referenceName
		| dereference
		| referenceValue
	);
referenceName: IDENTIFIER;
dereference: referenceName CARET+;

// function blocks [!!!]
fbTypeName: IDENTIFIER;
fbTypeAccess: ( namespaceName '.')* fbTypeName;
fbDeclaration:
	FUNCTION_BLOCK (FINAL | ABSTRACT)? fbName usingDirective* (
		EXTENDS (fbTypeAccess | classTypeAccess)
	)? (IMPLEMENTS interfaceTypeList)? (
		fbIOVarDeclarations
		| functionVarDeclarations
		| tempVarDeclarations
		| otherVarDeclarations
	)* (methodDeclaration)* fbBody END_FUNCTION_BLOCK;
fbIOVarDeclarations:
	fbInputDeclarations
	| fbOutputDeclarations
	| inOutDeclarations;
fbInputDeclarations:
	VAR_INPUT (RETAIN | NON_RETAIN)? (fbInputDeclaration ';')* END_VAR;
fbInputDeclaration:
	varDeclarationInit
	| arrayConformDeclaration;
fbOutputDeclarations:
	VAR_OUTPUT (RETAIN | NON_RETAIN)? (fbOutputDeclaration ';')* END_VAR;
fbOutputDeclaration:
	varDeclarationInit
	| arrayConformDeclaration;
otherVarDeclarations:
	retainVarDeclarations
	| nonRetainVarDeclarations
	| locPartlyVarDeclaration;
nonRetainVarDeclarations:
	VAR NON_RETAIN accessSpecification? (varDeclarationInit ';')* END_VAR;
fbBody: statementList;
methodDeclaration:
	METHOD accessSpecification (FINAL | ABSTRACT)? OVERRIDE? methodName (
		':' dataTypeAccess
	)? (
		ioVarDeclarations
		| functionVarDeclarations
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

// Variable declarations [!!!]
variable: directVariable | symbolicVariable;
symbolicVariable: (( THIS '.') | ( namespaceName '.')+)? (
		varAccess
		| multiElementVar
	);
varAccess: varName | dereference;
varName: IDENTIFIER;
multiElementVar: varAccess (subscriptList | structVariable)+;
subscriptList: '[' subscript ( ',' subscript)* ']';
subscript: expression;
structVariable: '.' structElementSelect;
structElementSelect: varAccess;

// input variable declarations [!!!]
inputDeclarations:
	VAR_INPUT (RETAIN | NON_RETAIN)? (inputDeclaration ';')* END_VAR;
inputDeclaration: varDeclarationInit | arrayConformDeclaration;

varDeclarationInit:
	variableList ':' (
		simpleSpecificationInit
		| strVarDeclaration
		| referenceSpecificationInit
	)
	| arrayVarDeclarationInit
	| structVarDeclarationInit
	| fbDeclarationInit
	| interfaceSpecificationInit;

referenceVarDeclaration:
	variableList ':' referenceSpecification;

interfaceVarDeclaration: variableList ':' interfaceTypeAccess;
variableList: variableName ( ',' variableName)*;
variableName: IDENTIFIER;

arrayVarDeclarationInit:
	variableList ':' arraySpecificationInit;
arrayConformand: ARRAY '[' '*' ( ',' '*')* ']' OF dataTypeAccess;
arrayConformDeclaration: variableList ':' arrayConformand;

structVarDeclarationInit:
	variableList ':' structSpecificationInit;

fbDeclarationNoInit: fbName ( ',' fbName)* ':' fbTypeAccess;
fbDeclarationInit: fbDeclarationNoInit ( ':=' structInit)?;
fbName: IDENTIFIER;
fbInstanceName: ( namespaceName '.')* fbName '^'*;

// output declarations [!!!]
outputDeclarations:
	VAR_OUTPUT (RETAIN | NON_RETAIN)? (outputDeclaration ';')* END_VAR;
outputDeclaration: varDeclarationInit | arrayConformDeclaration;
inOutDeclarations:
	VAR_IN_OUT (inOutVarDeclaration ';')* END_VAR;
inOutVarDeclaration:
	varDeclaration
	| arrayConformDeclaration
	| fbDeclarationNoInit;

// normal variable declaration [!!!]
varDeclaration:
	variableList ':' (
		simpleSpecification
		| strVarDeclaration
		| arrayVarDeclaration
		| structVarDeclaration
	);

arrayVarDeclaration: variableList ':' arraySpecification;

structVarDeclaration: variableList ':' structTypeAccess;

varDeclarations:
	VAR CONSTANT? accessSpecification? (varDeclarationInit ';')* END_VAR;

retainVarDeclarations:
	VAR RETAIN accessSpecification? (varDeclarationInit ';')* END_VAR;

locVarDeclarations:
	VAR (CONSTANT | RETAIN | NON_RETAIN)? (locVarDeclaration ';')* END_VAR;
locVarDeclaration:
	variableName? locatedAt ':' locVarSpecificationInit;

tempVarDeclarations:
	VAR_TEMP (
		(
			varDeclaration
			| referenceVarDeclaration
			| interfaceVarDeclaration
		) ';'
	)* END_VAR;

externalVarDeclarations:
	VAR_EXTERNAL CONSTANT? (externalDeclaration ';')* END_VAR;
externalDeclaration:
	globalVarName ':' (
		simpleSpecification
		| arraySpecification
		| structTypeAccess
		| fbTypeAccess
		| referenceTypeAccess
	);

// global variables [!!!]
globalVarName: IDENTIFIER;
globalVarDeclarations:
	VAR_GLOBAL (CONSTANT | RETAIN)? (
		globalVarDeclaration SEMICOLON
	)* END_VAR;
globalVarDeclaration:
	globalVarSpecification COLON (
		locVarSpecificationInit
		| fbTypeAccess
	);
globalVarSpecification:
	globalVarName (COMMA globalVarName)*
	| globalVarName locatedAt;
locVarSpecificationInit:
	simpleSpecificationInit
	| arraySpecificationInit
	| structSpecificationInit
	| sByteStrSpecification
	| dByteStrSpecification;

strVarDeclaration: sByteStrVarDecl | dByteStrVarDeclaration;
sByteStrVarDecl: variableList ':' sByteStrSpecification;
sByteStrSpecification:
	STRING ('[' UNSIGNED_INT ']')? (':=' SINGLE_BYTE_STRING)?;
dByteStrVarDeclaration: variableList ':' dByteStrSpecification;
dByteStrSpecification:
	WSTRING ('[' UNSIGNED_INT ']')? (':=' DOUBLE_BYTE_STRING)?;
locPartlyVarDeclaration:
	VAR (RETAIN | NON_RETAIN)? locPartlyVar* END_VAR;
locPartlyVar:
	variableName AT RELATIVE_ADDRESS ':' varSpecification ';';
varSpecification:
	simpleSpecification
	| arraySpecification
	| structTypeAccess
	| ( STRING | WSTRING) ( '[' UNSIGNED_INT ']')?;

// Funtions
functionDeclaration:
	FUNCTION functionName (COLON dataTypeAccess)? usingDirective* (
		ioVarDeclarations
		| functionVarDeclarations
		| tempVarDeclarations
	)* functionBody END_FUNCTION;

ioVarDeclarations:
	inputDeclarations
	| outputDeclarations
	| inOutDeclarations;

functionVarDeclarations:
	externalVarDeclarations
	| varDeclarations;

functionBody: statementList;

// Class declaration
classDeclaration:
	CLASS (FINAL | ABSTRACT)? classTypeName usingDirective* (
		EXTENDS classTypeAccess
	)? (IMPLEMENTS interfaceTypeList)? (
		functionVarDeclarations
		| otherVarDeclarations
	)* (methodDeclaration)* END_CLASS;
classTypeName: IDENTIFIER;
classTypeAccess: ( namespaceName DOT)* classTypeName;

className: IDENTIFIER;
classInstanceName: ( namespaceName DOT)* className CARET*;

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
interfaceSpecificationInit:
	variableList (ASSIGN interfaceValue)?;
interfaceValue:
	symbolicVariable
	| fbInstanceName
	| classInstanceName
	| NULL;

// Statements
statementList: (statement? SEMICOLON)*;
statement:
	assignStatement
	| subprogControlStatement
	| selectionStatement
	| loopStatement;

assignStatement: (variable ASSIGN expression)
	| referenceAssign
	| assignmentAttempt;

assignmentAttempt: (referenceName | dereference) ATTEMPT_ASSIGN (
		referenceName
		| dereference
		| referenceValue
	);

subprogControlStatement:
	functionCall
	| invocation
	| SUPER LEFT_PAREN RIGHT_PAREN
	| RETURN;

functionCall:
	functionAccess LEFT_BRACKET (
		parameterAssign (COMMA parameterAssign)*
	)? RIGHT_PAREN;

functionAccess: (namespaceName DOT)* functionName;
functionName: IDENTIFIER;

invocation: (
		fbInstanceName
		| (THIS DOT)? (
			((fbInstanceName | classInstanceName) DOT)+
		) methodName
	) LEFT_PAREN (parameterAssign (COMMA parameterAssign)*)? RIGHT_PAREN;

parameterAssign: ((variableName ASSIGN)? expression)
	| referenceAssign
	| (NOT? variableName ASSIGN_OUT variable);

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
	(literalValue | variableAccess | enumValue | referenceValue)	# primaryExpression
	| LEFT_PAREN expression RIGHT_PAREN								# bracketedExpression
	| functionCall													# funcCallExpression
	| dereference													# derefExpression
	| ( PLUS | MINUS | NOT) expression								# unaryExpression
	| <assoc = right> expression POWER expression					# exponentExpression
	| expression (ASTERISK | SLASH | MOD) expression				# multDivModExpression
	| expression (PLUS | MINUS) expression							# addSubExpression
	| expression (
		LESS
		| GREATER
		| LESS_EQUAL
		| GREATER_EQUAL
		| EQUAL
		| NOT_EQUAL
	) expression								# comparisonExpression
	| expression (AMPERSAND | AND) expression	# andExpression
	| expression XOR expression					# xorExpression
	| expression OR expression					# orExpression;

// Exit
exitStatement: EXIT;

// Continue
continueStatement: CONTINUE;

variableAccess: variable multibitPartAccess?;

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
	SIGNED_INT
	| UNSIGNED_INT
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
realLiteralValue: REAL_VALUE;
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
durationLiteralValue: DURATION;
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

// Direct access literals
RELATIVE_ADDRESS:
	'%' ('X' | 'B' | 'W' | 'D' | 'L')? UNSIGNED_INT;
DIRECT_VARIABLE:
	'%' ('I' | 'Q' | 'M') ('X' | 'B' | 'W' | 'D' | 'L' | '*')? (
		UNSIGNED_INT ( '.' UNSIGNED_INT)*
	)?;

// Character string literals
SINGLE_BYTE_STRING: '\'' SINGLE_BYTE_CHAR* '\'';
DOUBLE_BYTE_STRING: '"' DOUBLE_BYTE_CHAR* '"';

// Date and time literal values
DURATION: ('+' | '-')? (DIGIT+ DURATION_UNIT '_'?)+ DIGIT+ (
		'.' DIGIT+
	)? DURATION_UNIT;
DATE_TIME_VALUE: DATE_VALUE '-' CLOCK_TIME;
DATE_VALUE:
	DIGIT DIGIT DIGIT DIGIT '-' DIGIT DIGIT '-' DIGIT DIGIT;
CLOCK_TIME:
	DIGIT DIGIT ':' DIGIT DIGIT ':' UNSIGNED_INT '.' UNSIGNED_INT;

// Numeric and boolean literal values
REAL_VALUE: ('+' | '-')? UNSIGNED_INT '.' UNSIGNED_INT (
		'E' (SIGNED_INT | UNSIGNED_INT)
	)?;
SIGNED_INT: ( '+' | '-') UNSIGNED_INT;
UNSIGNED_INT: DIGIT ( '_'? DIGIT)*;
BINARY_INT: '2#' ( '_'? BIT)+;
OCTAL_INT: '8#' ( '_'? OCTAL_DIGIT)+;
HEX_INT: '16#' ( '_'? HEX_DIGIT)+;
BOOLEAN: FALSE | TRUE;

// Integer data type names
USINT: 'USINT';
UINT: 'UINT';
UDINT: 'UDINT';
ULINT: 'ULINT';
SINT: 'SINT';
INT: 'INT';
DINT: 'DINT';
LINT: 'LINT';

// Real data type names
REAL: 'REAL';
LREAL: 'LREAL';

// Multibit data type names
BYTE: 'BYTE';
WORD: 'WORD';
DWORD: 'DWORD';
LWORD: 'LWORD';

// Bool type names
BOOL: 'BOOL';
FALSE: 'FALSE';
TRUE: 'TRUE';

// Character and string type names
STRING: 'STRING';
WSTRING: 'WSTRING';
CHAR: 'CHAR';
WCHAR: 'WCHAR';

// Date type names
TIME: 'TIME' | 'T';
LTIME: 'LTIME' | 'LT';
TIME_OF_DAY: 'TIME_OF_DAY' | 'TOD';
LTIME_OF_DAY: 'LTIME_OF_DAY' | 'LTOD';
DATE: 'DATE' | 'D';
LDATE: 'LDATE' | 'LD';
DATE_AND_TIME: 'DATE_AND_TIME' | 'DT';
LDATE_AND_TIME: 'LDATE_AND_TIME' | 'LDT';

// User defined data types keywords
TYPE: 'TYPE';
END_TYPE: 'END_TYPE';

// Array declaration keywords
ARRAY: 'ARRAY';
OF: 'OF';

// Structure declaration keywords
STRUCT: 'STRUCT';
OVERLAP: 'OVERLAP';
END_STRUCT: 'END_STRUCT';

// Direct variable declaration keywords
AT: 'AT';

// References kewords
REF_TO: 'REF_TO';
REF: 'REF';
NULL: 'NULL';

// Main variable declarations keywords
THIS: 'THIS';
VAR_INPUT: 'VAR_INPUT';
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

// Function declaration keywords
FUNCTION: 'FUNCTION';
END_FUNCTION: 'END_FUNCTION';

// Function block and class declaration keywords
FUNCTION_BLOCK: 'FUNCTION_BLOCK';
FINAL: 'FINAL';
ABSTRACT: 'ABSTRACT';
EXTENDS: 'EXTENDS';
IMPLEMENTS: 'IMPLEMENTS';
END_FUNCTION_BLOCK: 'END_FUNCTION_BLOCK';
METHOD: 'METHOD';
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

// Program declaration keywords
PROGRAM: 'PROGRAM';
END_PROGRAM: 'END_PROGRAM';
VAR_ACCESS: 'VAR_ACCESS';

// Configuration and resource declaration keywords
CONFIGURATION: 'CONFIGURATION';
END_CONFIGURATION: 'END_CONFIGURATION';
RESOURCE: 'RESOURCE';
ON: 'ON';
END_RESOURCE: 'END_RESOURCE';
READ_WRITE: 'READ_WRITE';
READ_ONLY: 'READ_ONLY';
TASK: 'TASK';
SINGLE: 'SINGLE';
INTERVAL: 'INTERVAL';
PRIORITY: 'PRIORITY';
WITH: 'WITH';
VAR_CONFIG: 'VAR_CONFIG';

// Namespace declaration keywords
NAMESPACE: 'NAMESPACE';
END_NAMESPACE: 'END_NAMESPACE';
USING: 'USING';

// Selection statements keywords
IF: 'IF';
THEN: 'THEN';
ELSIF: 'ELSIF';
ELSE: 'ELSE';
END_IF: 'END_IF';
CASE: 'CASE';
END_CASE: 'END_CASE';

// Loop statements keywords
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

// Arithmetic operators
PLUS: '+';
MINUS: '-';
ASTERISK: '*';
POWER: '**';
SLASH: '/';
MOD: 'MOD';

// Relational operators
EQUAL: '=';
NOT_EQUAL: '<>';
LESS: '<';
LESS_EQUAL: '<=';
GREATER: '>';
GREATER_EQUAL: '>=';

// Logical/bitwise operators
AND: 'AND';
OR: 'OR';
NOT: 'NOT';
XOR: 'XOR';
AMPERSAND: '&';

// Assignment operators
ASSIGN: ':=';
ASSIGN_OUT: '=>';
ATTEMPT_ASSIGN: '?=';

// Delimiter characters
LEFT_PAREN: '(';
RIGHT_PAREN: ')';
LEFT_BRACKET: '[';
RIGHT_BRACKET: ']';
COMMA: ',';
COLON: ':';
SEMICOLON: ';';
DOT: '.';
RANGE: '..';

// Special characters
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

// General identifier rule
IDENTIFIER: NON_DIGIT (NON_DIGIT | DIGIT)*;

// Pragmas, placement is important 
PRAGMA: '{' .*? '}' -> channel(HIDDEN);

// Comments 
LINE_COMMENT: '//' ~[\r\n]* -> channel(HIDDEN);
SLASH_COMMENT:
	'/*' (SLASH_COMMENT | .)*? '*/' -> channel(HIDDEN);
BRACE_COMMENT:
	'(*' (BRACE_COMMENT | .)*? '*)' -> channel(HIDDEN);

// Whitespaces
WHITESPACE: [ \t\r\n]+ -> channel(HIDDEN);