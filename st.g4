grammar st;
// options
options { caseInsensitive=true; }

program : 'id' | dog;
dog : 'dog';

// Lexer //


// Comments and whitespaces
LINE_COMMENT : '//' .*? '\r'? '\n' -> skip ;
COMMENT      : '/*' .*? '*/'       -> skip ;
WHITESPACE   : [ \t\r\n]+ -> skip ;

