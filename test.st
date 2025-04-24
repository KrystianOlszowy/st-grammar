program 

////////////////////////////////
/* Numeric and logic literals */
////////////////////////////////

//INT#16#7FFF
//17.65e-34
//BOOL#0
//BOOL#FALSE
//-0 
//FALSE
//0.4560
//0.0

///////////////////////////////
/* Character string literals */
///////////////////////////////

//WCHAR#"" 
//WSTRING#"Anatol $" i pies" 
//WSTRING#'Anatol $' i pies'
//"!#%& []';[]/%^&%!@#!@$L" 
//" ' " 
//"$"" 
//"$R$L$00C4"

////////////////////////////
/* Date and time literals */
////////////////////////////

//TIME#5d
//TIME#2h30m
//DATE#2025-04-24
//TIME_OF_DAY#12:30:45.123
//DATE_AND_TIME#2025-04-24-12:30:45.123

/////////////////////////////////////////////////////////
// Polecenia do testowania tworzenia drzew składniowych:
/////////////////////////////////////////////////////////
// >>dotnet-script parse.csx
// >>dot -Tpng tree.dot -o tree.png
end_program;
