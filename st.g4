grammar st;
// options
options { caseInsensitive=true; }

program : 'id';

// Lexer //


// Comments and whitespaces
LINE_COMMENT : '//' .*? '\r'? '\n' -> skip ;
COMMENT      : '/*' .*? '*/'       -> skip ;
WHITESPACE   : [ \t\r\n]+ -> skip ;

