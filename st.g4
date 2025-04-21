grammar st;
// options //

// Parser //
program: PROGRAM literalValue END_PROGRAM ';'?;

PROGRAM: 'PROGRAM';
END_PROGRAM: 'END_PROGRAM';

// Literals
literalValue:
	numericLiteral
	/*| charLiteral*/
	/*| timeLiteral*/
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

realLiteral: (realTypeName '#')? SIMPLE_REAL (
		'E' (SIGNED_INT | UNSIGNED_INT)
	)?;

realTypeName: REAL | LREAL;

bitStringLiteral: (multibitsTypeName '#')? (
		UNSIGNED_INT
		| BINARY_INT
		| OCTAL_INT
		| HEX_INT
	);
multibitsTypeName: BYTE | WORD | DWORD | LWORD;

boolLiteral: (boolTypeName '#')? (BOOLEAN | '0' | '1');
boolTypeName: BOOL;

/* 
 charLiteral : ( 'STRING#' )? charString; 
 charString : singleByteCharacterString |
 doubleByteCharacterString; 
 singleByteCharacterString : '\'' singleByteCharValue + '\''; 
 doubleByteCharacterString : '"' doubleByteCharValue + '"'; 
 singleByteCharValue : commonCharValue
 | '$\'' | '"' | '$' HEX_DIGIT HEX_DIGIT; 
 doubleByteCharValue : commonCharValue | '\'' | '$"' |
 '$' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT; 
 commonCharValue : ' ' | '!' | '#' | '%' | '&' |
 '('..'/' | '0'..'9' | ':'..'@' | 'A'..'Z' | '['..'`' | 'a'..'z' | '{'..'~' 
 | '$$' | '$L' | '$N'
 |
 '$P' | '$R' | '$T';
 */

// Lexer //

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

// special characters
DOT: '.';

// essential fragments
fragment NON_DIGIT: [a-zA-Z_];
fragment DIGIT: [0-9];
fragment BIT: [0-1];
fragment OCTAL_DIGIT: [0-7];
fragment HEX_DIGIT: [0-9a-fA-F];

// identifiers
IDENTIFIER:
	NON_DIGIT (NON_DIGIT | DIGIT)*; //lack of underscore checking

// pragma
PRAGMA: '{' .*? '}' -> channel(HIDDEN);

// comments
LINE_COMMENT: '//' ~[\r\n]* -> channel(HIDDEN);
SLASH_COMMENT:
	'/*' (SLASH_COMMENT | .)*? '*/' -> channel(HIDDEN); //recursive lexer rule
BRACE_COMMENT:
	'(*' (BRACE_COMMENT | .)*? '*)' -> channel(HIDDEN); //recursive lexer rule

// whitespaces
WHITESPACE: [ \t\r\n]+ -> channel(HIDDEN);