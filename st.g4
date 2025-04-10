grammar st;
// options

// Parser //
program: PROGRAM .*? END_PROGRAM;

// Lexer //

// program declaration
PROGRAM: 'PROGRAM';
END_PROGRAM: 'END_PROGRAM';

// variable declarations
VAR: 'VAR';
VAR_INPUT: 'VAR_INPUT';
VAR_OUTPUT: 'VAR_OUTPUT';
VAR_IN_OUT: 'VAR_IN_OUT';
VAR_EXTERNAL: 'VAR_EXTERNAL';
VAR_GLOBAL: 'VAR_GLOBAL';
VAR_ACCESS: 'VAR_ACCESS';
VAR_TEMP: 'VAR_TEMP';
VAR_CONFIG: 'VAR_CONFIG';
RETAIN: 'RETAIN';
NOT_RETAIN: 'NON_RETAIN';
CONSTANT: 'CONSTANT';
AT: 'AT';
END_VAR: 'END_VAR';

// data types
BOOL: 'BOOL';
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
TIME_OF_DAY: 'TIME_OF_DAY' | 'TOD';
DATE_AND_TIME: 'DATE_AND_TIME' | 'DT';
STRING: 'STRING';
BYTE: 'BYTE';
WORD: 'WORD';
DWORD: 'DWORD';
LWORD: 'LWORD';
WSTRING: 'WSTRING';

// generic data types
ANY: 'ANY';
ANY_DERIVED: 'ANY_DERIVED';
ANY_ELEMENTARY: 'ANY_ELEMENTARY';
ANY_MAGNITUDE: 'ANY_MAGNITUDE';
ANY_NUM: 'ANY_NUM';
ANY_REAL: 'ANY_REAL';
ANY_INT: 'ANY_INT';
ANY_BIT: 'ANY_BIT';
ANY_STRING: 'ANY_STRING';
ANY_DATE: 'ANY_DATE';

// user declared types
TYPE: 'TYPE';
END_TYPE: 'END_TYPE';
ARRAY: 'ARRAY';
OF: 'OF';
STRUCT: 'STRUCT';

// variables addresing and sizing for direct representation
VAR_LOCATION_PREFIX: 'I' | 'Q' | 'M';
VAR_SIZE_PREFIX: 'X' | 'B' | 'W' | 'D' | 'L';

// language constructions
FUNCTION: 'FUNCTION';
END_FUNCTION: 'END_FUNCTION';
FUNCTION_BLOCK: 'FUNCTION_BLOCK';
END_FUNCTION_BLOCK: 'END_FUNCTION_BLOCK';
RETURN: 'RETURN';
IF: 'IF';
ELSIF: 'ELSIF';
ELSE: 'ELSE';
THEN: 'THEN';
END_IF: 'END_IF';
CASE: 'CASE';
//OF: 'OF';
END_CASE: 'END_CASE';
FOR: 'FOR';
//TO: 'TO';
BY: 'BY';
DO: 'DO';
EXIT: 'EXIT';
END_FOR: 'END_FOR';
WHILE: 'WHILE';
END_WHILE: 'END_WHILE';
REPEAT: 'REPEAT';
UNTIL: 'UNTIL';
END_REPEAT: 'END_REPEAT';

// configuration elements
CONFIGURATION: 'CONFIGURATION';
END_CONFIGURATION: 'END_CONFIGURATION';
RESOURCE: 'RESOURCE';
ON: 'ON';
END_RESOURCE: 'END_RESOURCE';
TASK: 'TASK';
WITH: 'WITH';
// can be unnecessery
SINGLE: 'SINGLE';
INTERVAL: 'INTERVAL';
PRIORITY: 'PRIORITY';
//end of unnecesssery stuff

// SFC elements
STEP: 'STEP';
INITIAL_STEP: 'INITIAL_STEP';
END_STEP: 'END_STEP';
TRANSITION: 'TRANSITION';
FROM: 'FROM';
TO: 'TO';
END_TRANSITION: 'END_TRANSITION';
ACTION: 'ACTION';
END_ACTION: 'END_ACTION';

// special characters
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

// transfer operators
ASSIGN_OPERATOR: ':=';
ANONYMOUS_FN_OPERATOR: '=>';

// arithmetic operators
ADD_OPERATOR: '+';
SUBTRACT_OPERATOR: '-';
MULTIPLY_OPERATOR: '*';
DIVISION_OPERATOR: '/';
MODULO_OPERATOR: 'MOD';
EXPONENT_OPERATOR: '**';

// relational operators
EQUAL_OPERATOR: '=';
LESS_THAN_OPERATOR: '<';
LESS_THAN_EQUAL_OPERATOR: '<=';
GREATER_THAN_OPERATOR: '>';
GREATER_THAN_EQUAL_OPERATOR: '>=';
NOT_EQUAL_OPERATOR: '<>';

// logical/bitwise operators
AND_OPERATOR: '&' | 'AND';
OR_OPERATOR: 'OR';
XOR_OPERATOR: 'XOR';
NEGATION_OPERATOR: 'NOT';

// values


// comments and whitespaces
LINE_COMMENT: '//' .*? '\r'? '\n' -> skip;
COMMENT: '/*' .*? '*/' -> skip;
WHITESPACE: [ \t\r\n]+ -> skip;