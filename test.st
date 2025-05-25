(*$DIR var*)
{ }
prograM

////////////////////////////////
/* Numeric and logic literals */
////////////////////////////////

// INT ////////////////////////
//-17
//INT#16#7FFF
//0
//-0

// REAL ///////////////////////
//REAL#-17.65e-34
//0.0
//0.4560

// BOOL ///////////////////////
//BOOL#0
//BOOL#FALSE
//BOOL#1
//FALSE

// MULTIBITS //////////////////
//WORD#2#1111_0000
//DWORD#16#FF07
//LWORD#2654

/////////////////////////////////
/* Character string literals *///
/////////////////////////////////

//WCHAR#"" 
//WSTRING#"Anatol $" i pies" 
//STRING#'Anatol $' i pies'
//"!#%& []';[]/%^&%!@#!@$L" 
//" ' "
//" " 
//"$"" 
//"$R$L$00C4"

////////////////////////////////
/* Date and time literals */////
////////////////////////////////

// DURATION/TIME ///////////////
//TIME#50d_22h_30m_22s_300.3ms
//LT#2h30m

// TIME OF DAY /////////////////
//TIME_OF_DAY#15:36:55.36_400
//LTOD#15:36:55.36 
//LTIME_OF_DAY#15:36:55.36

// DATE //////////////////////////
//DATE#2025-04-24
//LD#2010-09-22

// DATE AND TIME ///////////////
//DATE_AND_TIME#1984-06-25-15:36:55.360227400 
//LDT#1984-06-25-15:36:55.360_227_400

// DIRECT VARIABLES //////////////
//%QX7.5 
%MW0.7.9

/////////////////////////////////////////////////////////
// Polecenia do testowania tworzenia drzew składniowych:
/////////////////////////////////////////////////////////
// >>dotnet-script parse.csx
// >>dot -Tpng tree.dot -o tree.png
end_program;
