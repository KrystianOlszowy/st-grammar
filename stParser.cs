//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:/Users/kryst/Desktop/Praca magisterska/st-grammar/st.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public partial class stParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		PROGRAM=1, END_PROGRAM=2, VAR=3, VAR_INPUT=4, VAR_OUTPUT=5, VAR_IN_OUT=6, 
		VAR_EXTERNAL=7, VAR_GLOBAL=8, VAR_ACCESS=9, VAR_TEMP=10, VAR_CONFIG=11, 
		RETAIN=12, NOT_RETAIN=13, CONSTANT=14, AT=15, END_VAR=16, BOOL=17, SINT=18, 
		INT=19, DINT=20, LINT=21, USINT=22, UINT=23, UDINT=24, ULINT=25, REAL=26, 
		LREAL=27, TIME=28, DATE=29, TIME_OF_DAY=30, DATE_AND_TIME=31, STRING=32, 
		BYTE=33, WORD=34, DWORD=35, LWORD=36, WSTRING=37, ANY=38, ANY_DERIVED=39, 
		ANY_ELEMENTARY=40, ANY_MAGNITUDE=41, ANY_NUM=42, ANY_REAL=43, ANY_INT=44, 
		ANY_BIT=45, ANY_STRING=46, ANY_DATE=47, TYPE=48, END_TYPE=49, ARRAY=50, 
		OF=51, STRUCT=52, VAR_LOCATION_PREFIX=53, VAR_SIZE_PREFIX=54, FUNCTION=55, 
		END_FUNCTION=56, FUNCTION_BLOCK=57, END_FUNCTION_BLOCK=58, RETURN=59, 
		IF=60, ELSIF=61, ELSE=62, THEN=63, END_IF=64, CASE=65, END_CASE=66, FOR=67, 
		BY=68, DO=69, EXIT=70, END_FOR=71, WHILE=72, END_WHILE=73, REPEAT=74, 
		UNTIL=75, END_REPEAT=76, CONFIGURATION=77, END_CONFIGURATION=78, RESOURCE=79, 
		ON=80, END_RESOURCE=81, TASK=82, WITH=83, SINGLE=84, INTERVAL=85, PRIORITY=86, 
		STEP=87, INITIAL_STEP=88, END_STEP=89, TRANSITION=90, FROM=91, TO=92, 
		END_TRANSITION=93, ACTION=94, END_ACTION=95, COLON=96, SEMICOLON=97, DOT=98, 
		COMMA=99, BRACKET_OPEN=100, BRACKET_CLOSE=101, SQUARE_BRACKET_OPEN=102, 
		SQUARE_BRACKET_CLOSE=103, CURLY_BRACKET_OPEN=104, CURLY_BRACKET_CLOSE=105, 
		HASH=106, CARET=107, PERCENT=108, ASSIGN_OPERATOR=109, ANONYMOUS_FN_OPERATOR=110, 
		ADD_OPERATOR=111, SUBTRACT_OPERATOR=112, MULTIPLY_OPERATOR=113, DIVISION_OPERATOR=114, 
		MODULO_OPERATOR=115, EXPONENT_OPERATOR=116, EQUAL_OPERATOR=117, LESS_THAN_OPERATOR=118, 
		LESS_THAN_EQUAL_OPERATOR=119, GREATER_THAN_OPERATOR=120, GREATER_THAN_EQUAL_OPERATOR=121, 
		NOT_EQUAL_OPERATOR=122, AND_OPERATOR=123, OR_OPERATOR=124, XOR_OPERATOR=125, 
		NEGATION_OPERATOR=126, LINE_COMMENT=127, COMMENT=128, WHITESPACE=129;
	public const int
		RULE_program = 0;
	public static readonly string[] ruleNames = {
		"program"
	};

	private static readonly string[] _LiteralNames = {
		null, "'PROGRAM'", "'END_PROGRAM'", "'VAR'", "'VAR_INPUT'", "'VAR_OUTPUT'", 
		"'VAR_IN_OUT'", "'VAR_EXTERNAL'", "'VAR_GLOBAL'", "'VAR_ACCESS'", "'VAR_TEMP'", 
		"'VAR_CONFIG'", "'RETAIN'", "'NON_RETAIN'", "'CONSTANT'", "'AT'", "'END_VAR'", 
		"'BOOL'", "'SINT'", "'INT'", "'DINT'", "'LINT'", "'USINT'", "'UINT'", 
		"'UDINT'", "'ULINT'", "'REAL'", "'LREAL'", "'TIME'", "'DATE'", null, null, 
		"'STRING'", "'BYTE'", "'WORD'", "'DWORD'", "'LWORD'", "'WSTRING'", "'ANY'", 
		"'ANY_DERIVED'", "'ANY_ELEMENTARY'", "'ANY_MAGNITUDE'", "'ANY_NUM'", "'ANY_REAL'", 
		"'ANY_INT'", "'ANY_BIT'", "'ANY_STRING'", "'ANY_DATE'", "'TYPE'", "'END_TYPE'", 
		"'ARRAY'", "'OF'", "'STRUCT'", null, null, "'FUNCTION'", "'END_FUNCTION'", 
		"'FUNCTION_BLOCK'", "'END_FUNCTION_BLOCK'", "'RETURN'", "'IF'", "'ELSIF'", 
		"'ELSE'", "'THEN'", "'END_IF'", "'CASE'", "'END_CASE'", "'FOR'", "'BY'", 
		"'DO'", "'EXIT'", "'END_FOR'", "'WHILE'", "'END_WHILE'", "'REPEAT'", "'UNTIL'", 
		"'END_REPEAT'", "'CONFIGURATION'", "'END_CONFIGURATION'", "'RESOURCE'", 
		"'ON'", "'END_RESOURCE'", "'TASK'", "'WITH'", "'SINGLE'", "'INTERVAL'", 
		"'PRIORITY'", "'STEP'", "'INITIAL_STEP'", "'END_STEP'", "'TRANSITION'", 
		"'FROM'", "'TO'", "'END_TRANSITION'", "'ACTION'", "'END_ACTION'", "':'", 
		"';'", "'.'", "','", "'('", "')'", "'['", "']'", "'{'", "'}'", "'#'", 
		"'^'", "'%'", "':='", "'=>'", "'+'", "'-'", "'*'", "'/'", "'MOD'", "'**'", 
		"'='", "'<'", "'<='", "'>'", "'>='", "'<>'", null, "'OR'", "'XOR'", "'NOT'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "PROGRAM", "END_PROGRAM", "VAR", "VAR_INPUT", "VAR_OUTPUT", "VAR_IN_OUT", 
		"VAR_EXTERNAL", "VAR_GLOBAL", "VAR_ACCESS", "VAR_TEMP", "VAR_CONFIG", 
		"RETAIN", "NOT_RETAIN", "CONSTANT", "AT", "END_VAR", "BOOL", "SINT", "INT", 
		"DINT", "LINT", "USINT", "UINT", "UDINT", "ULINT", "REAL", "LREAL", "TIME", 
		"DATE", "TIME_OF_DAY", "DATE_AND_TIME", "STRING", "BYTE", "WORD", "DWORD", 
		"LWORD", "WSTRING", "ANY", "ANY_DERIVED", "ANY_ELEMENTARY", "ANY_MAGNITUDE", 
		"ANY_NUM", "ANY_REAL", "ANY_INT", "ANY_BIT", "ANY_STRING", "ANY_DATE", 
		"TYPE", "END_TYPE", "ARRAY", "OF", "STRUCT", "VAR_LOCATION_PREFIX", "VAR_SIZE_PREFIX", 
		"FUNCTION", "END_FUNCTION", "FUNCTION_BLOCK", "END_FUNCTION_BLOCK", "RETURN", 
		"IF", "ELSIF", "ELSE", "THEN", "END_IF", "CASE", "END_CASE", "FOR", "BY", 
		"DO", "EXIT", "END_FOR", "WHILE", "END_WHILE", "REPEAT", "UNTIL", "END_REPEAT", 
		"CONFIGURATION", "END_CONFIGURATION", "RESOURCE", "ON", "END_RESOURCE", 
		"TASK", "WITH", "SINGLE", "INTERVAL", "PRIORITY", "STEP", "INITIAL_STEP", 
		"END_STEP", "TRANSITION", "FROM", "TO", "END_TRANSITION", "ACTION", "END_ACTION", 
		"COLON", "SEMICOLON", "DOT", "COMMA", "BRACKET_OPEN", "BRACKET_CLOSE", 
		"SQUARE_BRACKET_OPEN", "SQUARE_BRACKET_CLOSE", "CURLY_BRACKET_OPEN", "CURLY_BRACKET_CLOSE", 
		"HASH", "CARET", "PERCENT", "ASSIGN_OPERATOR", "ANONYMOUS_FN_OPERATOR", 
		"ADD_OPERATOR", "SUBTRACT_OPERATOR", "MULTIPLY_OPERATOR", "DIVISION_OPERATOR", 
		"MODULO_OPERATOR", "EXPONENT_OPERATOR", "EQUAL_OPERATOR", "LESS_THAN_OPERATOR", 
		"LESS_THAN_EQUAL_OPERATOR", "GREATER_THAN_OPERATOR", "GREATER_THAN_EQUAL_OPERATOR", 
		"NOT_EQUAL_OPERATOR", "AND_OPERATOR", "OR_OPERATOR", "XOR_OPERATOR", "NEGATION_OPERATOR", 
		"LINE_COMMENT", "COMMENT", "WHITESPACE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "st.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static stParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public stParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public stParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class ProgramContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode PROGRAM() { return GetToken(stParser.PROGRAM, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode END_PROGRAM() { return GetToken(stParser.END_PROGRAM, 0); }
		public ProgramContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_program; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstVisitor<TResult> typedVisitor = visitor as IstVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProgram(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgramContext program() {
		ProgramContext _localctx = new ProgramContext(Context, State);
		EnterRule(_localctx, 0, RULE_program);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 2;
			Match(PROGRAM);
			State = 6;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,0,Context);
			while ( _alt!=1 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1+1 ) {
					{
					{
					State = 3;
					MatchWildcard();
					}
					} 
				}
				State = 8;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,0,Context);
			}
			State = 9;
			Match(END_PROGRAM);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static int[] _serializedATN = {
		4,1,129,12,2,0,7,0,1,0,1,0,5,0,5,8,0,10,0,12,0,8,9,0,1,0,1,0,1,0,1,6,0,
		1,0,0,0,11,0,2,1,0,0,0,2,6,5,1,0,0,3,5,9,0,0,0,4,3,1,0,0,0,5,8,1,0,0,0,
		6,7,1,0,0,0,6,4,1,0,0,0,7,9,1,0,0,0,8,6,1,0,0,0,9,10,5,2,0,0,10,1,1,0,
		0,0,1,6
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
