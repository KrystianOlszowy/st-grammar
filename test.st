(*$DIR var*)
{ }

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
TYPE 
 Colors: DWORD 
     (Red  := 16#00FF0000, 
      Green:= 16#0000FF00, 
      Blue := 16#000000FF, 
      White:= Red OR Green OR Blue, 
      Black:= Red AND Green AND Blue) 
      := Green; 
END_TYPE 

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

/* 
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
*/

/* 
Class CCounter 
  VAR 
    m_iCurrentValue: INT;  (* Default = 0 *) 
    m_bCountUp: BOOL:=TRUE; 
  END_VAR 
  VAR PUBLIC 
    m_iUpperLimit: INT:=+10000; 
    m_iLowerLimit: INT:=-10000; 
  END_VAR 
 
METHOD Count (* Only body *) 
    IF (m_bCountUp AND m_iCurrentValue<m_iUpperLimit) THEN 
        m_iCurrentValue:= m_iCurrentValue+1; 
    END_IF; 
    IF (NOT m_bCountUp AND m_iCurrentValue>m_iLowerLimit) THEN 
        m_iCurrentValue:= m_iCurrentValue-1; 
    END_IF; 
END_METHOD 
 
METHOD SetDirection 
  VAR_INPUT 
     bCountUp: BOOL; 
  END_VAR 
  m_bCountUp:=bCountUp; 
END_METHOD 
 
END_CLASS */

/* 
INTERFACE ROOM 
   METHOD DAYTIME   END_METHOD  // Called during day-time 
   METHOD NIGHTTIME END_METHOD  // Called during night-time 
END_INTERFACE 
 
FUNCTION_BLOCK ROOM_CTRL   // 
  VAR_INPUT 
    RM: ROOM;    // Interface ROOM as type of input variable 
  END_VAR 
  VAR_EXTERNAL 
    Actual_TOD: TOD;   // Global time definition 
  END_VAR 
  IF (RM = NULL)    // Important: test valid reference! 
  THEN RETURN; 
  END_IF; 
  IF Actual_TOD >= TOD#20:15 OR Actual_TOD <= TOD#6:00 
  THEN RM.NIGHTTIME();   // call method of RM 
  ELSE RM.DAYTIME();  
  END_IF; 
END_FUNCTION_BLOCK 
*/

/* 
CLASS LIGHTROOM IMPLEMENTS ROOM 
VAR LIGHT: BOOL; END_VAR 
 
METHOD PUBLIC DAYTIME 
  LIGHT:= FALSE; 
END_METHOD 
 
METHOD PUBLIC NIGHTTIME 
  LIGHT:= TRUE; 
END_METHOD 
END_CLASS 
 
FUNCTION_BLOCK ROOM_CTRL 
  VAR_INPUT  
 RM: ROOM;    // Interface ROOM as type of a variable 
  END_VAR 
 
  VAR_EXTERNAL 
 Actual_TOD: TOD;   // Global time definition 
  END_VAR 
 
  IF (RM = NULL)    // Important: test valid reference! 
  THEN RETURN; 
  END_IF; 
  IF Actual_TOD >= TOD#20:15 OR  
 Actual_TOD <= TOD#06:00  
  THEN RM.NIGHTTIME();   // Call method of RM (dynamic binding) to  
      // either LIGHTROOM.NIGHTTIME 
      // or LIGHT2ROOM.NIGHTTIME) 
  ELSE RM.DAYTIME(); 
  END_IF; 
END_FUNCTION_BLOCK */

/////////////////////////////////////////////////////////
// Polecenia do testowania tworzenia drzew składniowych:
/////////////////////////////////////////////////////////
// >>dotnet-script parse.csx
// >>dot -Tpng tree.dot -o tree.png
