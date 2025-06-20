(*$DIR var*)
{ }

////////////////////////////////
/* /* Numeric and logic literals */ */
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
/*TYPE data_i_godzina : 
  STRUCT
    dh : LDATE_AND_TIME := LDT#1984-06-25-15:36:55.360_227_400;
  END_STRUCT
END_TYPE
*/

// DIRECT VARIABLES /////////////////////////////////////////
//%QX7.5 
//%MW0.7.9
//%M*

// USER DEFINED DATA TYPE 

// ENUMARATION /////////////////////////////////////////////
/*TYPE 
  ANALOG_SIGNAL_RANGE: 
 (BIPOLAR_10V, 
  UNIPOLAR_10V, 
  UNIPOLAR_1_5V, 
  UNIPOLAR_0_5V, 
  UNIPOLAR_4_20_MA, 
  UNIPOLAR_0_20_MA) 
  := UNIPOLAR_1_5V; 
END_TYPE 
*/

// Data type with named values ////////////////////////////////
// DOESNT WORK NOW !!!!
/*TYPE 
 Colors: DWORD 
     (Red  := 16#00FF0000, 
      Green:= 16#0000FF00, 
      Blue := 16#000000FF, 
      White:= Red OR Green OR Blue, 
      Black:= Red AND Green AND Blue) 
      := Green; 
END_TYPE */

// SUBRANGE DATA TYPE ///////////////////////////////////////
/*TYPE 
  ANALOG_DATA: INT(-4095 .. 4095):= 0; 
END_TYPE
*/

// ARRAY DATA TYPE ////////////////////////////////////////////
/*TYPE ANALOG_16_INPUT_DATA: 
  ARRAY [1..16] OF ANALOG_DATA 
 := [8(-4095), 8(4095)]; 
END_TYPE*/ 

// SIMPLE STRUCT //////////////////////////////////////////////////
/*TYPE ANALOG_CHANNEL_CONFIGURATION: 
  STRUCT 
    RANGE:     ANALOG_SIGNAL_RANGE; 
    MIN_SCALE: ANALOG_DATA:= -4095; 
    MAX_SCALE: ANALOG_DATA:=  4095; 
  END_STRUCT;
END_TYPE*/

// STRUCT WITH RELATIVE ADDRESSING ///////////////////////
/*TYPE 
  Com1_data: STRUCT  
   head  AT %B0:  INT; 
   length  AT %B2:  USINT:= 26;  
   flag1  AT %X3.0:  BOOL; 
   end  AT %B25:  BYTE; 
  END_STRUCT; 
END_TYPE
*/

// STRUCT WITH PARTLY SPECIFIED ADDRESSES //////////////
/*TYPE 
  HW_COMP: STRUCT 
   IN AT %I*: BOOL; 
   OUT_VAR AT %Q*: WORD:= 200; 
   ITNL_VAR : REAL:= 123.0; // not located 
   END_STRUCT; 
END_TYPE 
*/

// DIRECTLY RERIVED DATA TYPE ///////////////////////////
/*TYPE 
  CNT:  UINT; 
  FREQ: REAL:= 50.0; 
  ANALOG_CHANNEL_CONFIG: 
   ANALOG_CHANNEL_CONFIGURATION 
  := (MIN_SCALE:= 0, MAX_SCALE:= 4000); 
END_TYPE */

// DATA TYPE INITIALIZATION WITH CONST EXPRESSION ///////////////
/*TYPE 
  crazyExpr: BOOL:= ((A + B * 2) > (C - D / 4)) AND (NOT (X OR Y)) OR (E MOD 3 = 0) AND (SHL(F, 1) < 256); 
END_TYPE
*/

// REFERENCE DATA TYPES //////////////////////////////////////
/*TYPE 
 myArrType1:  ARRAY[0..999] OF INT; 
 myArrType2:  myArrType1; 
 myRefType1:  REF_TO myArrType1; 
 myRefType2:  REF_TO myArrType2; 
END_TYPE
*/

/*VAR_GLOBAL 
  DataArray: ARRAY [0..100] OF INT; 
END_VAR 
*/

FUNCTION SPECIAL_FUN   
  VAR_INPUT 
    FirstIndex: INT; 
    LastIndex:  INT;  
  END_VAR 
  VAR_OUTPUT 
    Sum: INT;  
  END_VAR 
  VAR_EXTERNAL DataArray:  
      ARRAY [0..100] OF INT;  
  END_VAR 

  VAR 
    b : INT ; 
    Sum : INT:= 0 + 1; 
  END_VAR 

  FOR  i := FirstIndex TO LastIndex DO 
    Sum:= Sum + DataArray[i + 1]; 
  END_FOR 

END_FUNCTION


/////////////////////////////////////////////////////////
// Polecenia do testowania tworzenia drzew składniowych:
/////////////////////////////////////////////////////////
// >>dotnet-script parse.csx
// >>dot -Tpng tree.dot -o tree.png
