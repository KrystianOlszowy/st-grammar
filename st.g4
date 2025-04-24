grammar st;
// options //

// Parser //
program: PROGRAM literalValue END_PROGRAM ';'?;

PROGRAM: 'PROGRAM';
END_PROGRAM: 'END_PROGRAM';

// Literals
literalValue:
	numericLiteral
	| charLiteral
	| timeLiteral
	| bitStringLiteral
	| boolLiteral;

numericLiteral: intLiteral | realLiteral;

//integer literals
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

// real literals
realLiteral: (realTypeName '#')? SIMPLE_REAL (
		'E' (SIGNED_INT | UNSIGNED_INT)
	)?;
realTypeName: REAL | LREAL;

// bit string literals
bitStringLiteral: (multibitsTypeName '#')? (
		UNSIGNED_INT
		| BINARY_INT
		| OCTAL_INT
		| HEX_INT
	);
multibitsTypeName: BYTE | WORD | DWORD | LWORD;

// bool literals
boolLiteral: (boolTypeName '#')? (BOOLEAN | '0' | '1');
boolTypeName: BOOL;

// character string literals
charLiteral: ( charTypeName '#')? charString;
charTypeName: STRING | CHAR | WSTRING | WCHAR;
charString: SINGLE_BYTE_STRING | DOUBLE_BYTE_STRING;

// time litearals
timeLiteral:
	durationLiteral
	| timeOfDayLiteral
	| dateLiteral
	| dateAndTimeLiteral;

durationLiteral: (timeTypeName | 'T' | 'LT') '#' ('+' | '-')? DURATION;
timeTypeName: TIME | LTIME;

timeOfDayLiteral: timeOfDayTypeName '#' CLOCK_TIME;
timeOfDayTypeName: TIME_OF_DAY | LTIME_OF_DAY;

dateLiteral: (dateTypeName | 'D' | 'LD') '#' DATE_VALUE;
dateTypeName: DATE | LDATE;

dateAndTimeLiteral:
	(dateAndTimeTypeName | 'DT' | 'LDT') '#' DATE_TIME_VALUE;
dateAndTimeTypeName: DATE_AND_TIME | LDATE_AND_TIME;

///////////
// Lexer // /////////

// strings
SINGLE_BYTE_STRING: '\'' SINGLE_BYTE_CHAR* '\'';
DOUBLE_BYTE_STRING: '"' DOUBLE_BYTE_CHAR* '"';

// date and time
DURATION: (DURATION_SEGMENT)+;
DATE_TIME_VALUE: DATE_VALUE '-' CLOCK_TIME;
DATE_VALUE: DIGIT+ '-' DIGIT+ '-' DIGIT+;
CLOCK_TIME: DIGIT+ ':' DIGIT+ ':' DIGIT+ ('.' DIGIT+)?;

// simple data types //
SIGNED_INT: ( '+' | '-') UNSIGNED_INT;
UNSIGNED_INT: DIGIT ( '_'? DIGIT)*;
BINARY_INT: '2#' ( '_'? BIT)+;
OCTAL_INT: '8#' ( '_'? OCTAL_DIGIT)+;
HEX_INT: '16#' ( '_'? HEX_DIGIT)+;
SIMPLE_REAL: (SIGNED_INT | UNSIGNED_INT) DOT UNSIGNED_INT;
BOOLEAN: 'FALSE' | 'TRUE';

// numeric data type names //

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

fragment DURATION_SEGMENT:
	DIGIT+ ('.' DIGIT+)? DURATION_UNIT
	| DIGIT+ DURATION_UNIT '_'?;

fragment DURATION_UNIT:
	'D'
	| 'H'
	| 'M'
	| 'S'
	| 'MS'
	| 'US'
	| 'NS';

// identifiers
IDENTIFIER:
	NON_DIGIT (NON_DIGIT | DIGIT)*; //lack of underscore checking

// pragmas
PRAGMA: '{' .*? '}' -> channel(HIDDEN);

// comments
LINE_COMMENT: '//' ~[\r\n]* -> channel(HIDDEN);
SLASH_COMMENT:
	'/*' (SLASH_COMMENT | .)*? '*/' -> channel(HIDDEN); //recursive lexer rule
BRACE_COMMENT:
	'(*' (BRACE_COMMENT | .)*? '*)' -> channel(HIDDEN); //recursive lexer rule

// whitespaces
WHITESPACE: [ \t\r\n]+ -> channel(HIDDEN);