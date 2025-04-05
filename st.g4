grammar st;
// options

// Parser //
program: PROGRAM .*? END_PROGRAM ;

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
OF : 'OF';
STRUCT: 'STRUCT';


// comments and whitespaces
LINE_COMMENT: '//' .*? '\r'? '\n' -> skip;
COMMENT: '/*' .*? '*/' -> skip;
WHITESPACE: [ \t\r\n]+ -> skip;