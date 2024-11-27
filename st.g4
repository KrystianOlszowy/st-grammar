grammar st;
/*
 * Parser Rules
 */
file        : PROGRAM WHITESPACE PROGRAM_NAME WHITESPACE var_init program END_PROGRAM;
var_init    : ;
program     : ;

/*
 * Lexer Rules
 */
PROGRAM             : 'PROGRAM';
END_PROGRAM         : 'END_PROGRAM';
PROGRAM_NAME        : [ a-z | A-Z | _ ];
WHITESPACE          : (' '|'\t')+ -> skip ;