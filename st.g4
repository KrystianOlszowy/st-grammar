grammar st;

/// Parser rules ///

project: variableBlock+ program EOF;

variableBlock:
	variableBlockKeyword NAME variableDeclaration* END_VAR;
variableBlockKeyword:
	VAR_INPUT
	| VAR_OUTPUT
	| VAR_IN_OUT
	| VAR_GLOBAL
	| VAR_EXTERNAL
	| VAR_ACCESS;
variableDeclaration:
	NAME (AT MEMORY_ADDRESS)? COLON dataType (initialValue)? SEMICOLON;
initialValue: ASSIGNMENT_OPERATOR value;

program:
	PROGRAM NAME variableDeclaration* statement END_PROGRAM SEMICOLON?;
statement: assignment | blockCall SEMICOLON;
assignment:
	ASSIGNMENT_OPERATOR (expression | bracketedExpression);
blockCall:
	NAME BRACKET_OPEN (assignment COLON)* BRACKET_CLOSE SEMICOLON;

bracketedExpression: BRACKET_OPEN expression BRACKET_CLOSE;
expression:
    value operator value
	| value operator (expression | bracketedExpression)
	| (expression | bracketedExpression) operator value
	| (expression | bracketedExpression) operator (expression | bracketedExpression);
negation: NOT_OPERATOR value;
operator: logicalBinaryOperator;
logicalBinaryOperator:
	AND_OPERATOR
	| OR_OPERATOR
	| XOR_OPERATOR;

value: NUMBER | TIME_VALUE | variableValue;
variableValue: NAME (DOT NAME)*;
dataType:
	BOOL
	| BYTE
	| WORD
	| DWORD
	| LWORD
	| SINT
	| INT
	| DINT
	| LINT
	| USINT
	| UINT UDINT
	| ULINT
	| REAL
	| LREAL
	| TIME
	| DATE
	| TIME_OF_DAY
	| DATE_AND_TIME
	| STRING;

/// Lexer rules  ///

// Keywords
PROGRAM: 'PROGRAM';
END_PROGRAM: 'END_PROGRAM';
VAR: 'VAR';
VAR_INPUT: 'VAR_INPUT';
VAR_OUTPUT: 'VAR_OUTPUT';
VAR_IN_OUT: 'VAR_IN_OUT';
VAR_GLOBAL: 'VAR_GLOBAL';
VAR_EXTERNAL: 'VAR_EXTERNAL';
VAR_ACCESS: 'VAR_ACCESS;';
END_VAR: 'END_VAR';
RETAIN: 'RETAIN';
CONSTANT: 'CONSTANT';
AT: 'AT';

// Data types
BOOL: 'BOOL';
BYTE: 'BYTE';
WORD: 'WORD';
DWORD: 'DWORD';
LWORD: 'LWORD';
SINT: 'SINT';
INT: 'INT';
DINT: 'DINT';
LINT: 'LINT';
USINT: 'USINT';
UINT: 'UINT';
UDINT: 'UDINT';
ULINT: 'ULINT';
REAL: 'REAL';
LREAL: 'LREAL';
TIME: 'TIME';
DATE: 'DATE';
TIME_OF_DAY: 'TIME_OF_DAY';
DATE_AND_TIME: 'DATE_AND_TIME';
STRING: 'STRING';

// Generic data types
ANY: 'ANY';
ANY_BIT: 'ANY_BIT';
ANY_NUM: 'ANY_NUM';
ANY_DATE: 'ANY_DATE';
ANY_INT: 'ANY_INT';
ANY_REAL: 'ANY_REAL';

// Operators
ASSIGNMENT_OPERATOR: ':=';

// Logical operators
AND_OPERATOR: 'AND' | '&';
OR_OPERATOR: 'OR';
NOT_OPERATOR: 'NOT';
XOR_OPERATOR: 'XOR';

// Special characters
COLON: ':';
SEMICOLON: ';';
DOT: '.';
COMMA: ',';
BRACKET_OPEN: '(';
BRACKET_CLOSE: ')';
SQUARE_BRACKET_OPEN: '[';
SQUARE_BRACKET_CLOSE: ']';
CURLY_BRACKET_OPEN: '{';
CURLY_BRACKET_CLOSE: '}';
HASH: '#';
CARET: '^';
PERCENT: '%';

// Values
MEMORY_ADDRESS: '%' [0-9]{4};
NUMBER: [1-9][0-9]* ('.' [0-9]+)?;
TIME_VALUE: [tT]'#' [1-9][0-9]* 's';
NAME: [a-zA-Z][a-zA-Z0-9_]*;

// Comments
SINGLE_LINE_COMMENT: '//' .*? -> skip;
DELIMITED_COMMENT_SLASH: '/*' .*? '*/' -> skip;
DELIMITED_COMMENT_BRACKET: '(*' .*? '*)' -> skip;

// Whitespaces
WHITESPACE: [ \t\n] -> skip;