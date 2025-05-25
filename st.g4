grammar st;

// namespace: (program | function | fb | global_var | class)*;

// Parser //
program:
	PROGRAM (literalValue | directVariable) END_PROGRAM ';'?;

PROGRAM: 'PROGRAM';
END_PROGRAM: 'END_PROGRAM';

// Literals
literalValue:
	numericLiteral
	| boolLiteral
	| charLiteral
	| timeLiteral
	| multibitsLiteral;

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
boolLiteralValue: BOOLEAN | '0' | '1';
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

// Lexer //
DIRECT_VARIABLE:
	PERCENT ('I' | 'Q' | 'M') ('X' | 'B' | 'W' | 'D' | 'L') UNSIGNED_INT (
		DOT UNSIGNED_INT
	)*;
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

// pragmas //miejsce jest waażne, w konkretnych miejscach są 
PRAGMA: '{' .*? '}' -> channel(HIDDEN);

// comments
LINE_COMMENT: '//' ~[\r\n]* -> channel(HIDDEN);
SLASH_COMMENT:
	'/*' (SLASH_COMMENT | .)*? '*/' -> channel(HIDDEN);
BRACE_COMMENT:
	'(*' (BRACE_COMMENT | .)*? '*)' -> channel(HIDDEN);

// whitespaces
WHITESPACE: [ \t\r\n]+ -> channel(HIDDEN);