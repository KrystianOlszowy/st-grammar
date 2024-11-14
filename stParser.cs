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
		SAYS=1, WORD=2, TEXT=3, WHITESPACE=4, NEWLINE=5;
	public const int
		RULE_chat = 0, RULE_line = 1, RULE_name = 2, RULE_opinion = 3;
	public static readonly string[] ruleNames = {
		"chat", "line", "name", "opinion"
	};

	private static readonly string[] _LiteralNames = {
	};
	private static readonly string[] _SymbolicNames = {
		null, "SAYS", "WORD", "TEXT", "WHITESPACE", "NEWLINE"
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

	public partial class ChatContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public LineContext[] line() {
			return GetRuleContexts<LineContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public LineContext line(int i) {
			return GetRuleContext<LineContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(stParser.Eof, 0); }
		public ChatContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_chat; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstVisitor<TResult> typedVisitor = visitor as IstVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitChat(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ChatContext chat() {
		ChatContext _localctx = new ChatContext(Context, State);
		EnterRule(_localctx, 0, RULE_chat);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 8;
			line();
			State = 9;
			line();
			State = 10;
			Match(Eof);
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

	public partial class LineContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public NameContext name() {
			return GetRuleContext<NameContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SAYS() { return GetToken(stParser.SAYS, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public OpinionContext opinion() {
			return GetRuleContext<OpinionContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode NEWLINE() { return GetToken(stParser.NEWLINE, 0); }
		public LineContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_line; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstVisitor<TResult> typedVisitor = visitor as IstVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLine(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public LineContext line() {
		LineContext _localctx = new LineContext(Context, State);
		EnterRule(_localctx, 2, RULE_line);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 12;
			name();
			State = 13;
			Match(SAYS);
			State = 14;
			opinion();
			State = 15;
			Match(NEWLINE);
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

	public partial class NameContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode WORD() { return GetToken(stParser.WORD, 0); }
		public NameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_name; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstVisitor<TResult> typedVisitor = visitor as IstVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitName(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public NameContext name() {
		NameContext _localctx = new NameContext(Context, State);
		EnterRule(_localctx, 4, RULE_name);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 17;
			Match(WORD);
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

	public partial class OpinionContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode TEXT() { return GetToken(stParser.TEXT, 0); }
		public OpinionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_opinion; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstVisitor<TResult> typedVisitor = visitor as IstVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitOpinion(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public OpinionContext opinion() {
		OpinionContext _localctx = new OpinionContext(Context, State);
		EnterRule(_localctx, 6, RULE_opinion);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 19;
			Match(TEXT);
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
		4,1,5,22,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1,
		1,1,1,1,2,1,2,1,3,1,3,1,3,0,0,4,0,2,4,6,0,0,17,0,8,1,0,0,0,2,12,1,0,0,
		0,4,17,1,0,0,0,6,19,1,0,0,0,8,9,3,2,1,0,9,10,3,2,1,0,10,11,5,0,0,1,11,
		1,1,0,0,0,12,13,3,4,2,0,13,14,5,1,0,0,14,15,3,6,3,0,15,16,5,5,0,0,16,3,
		1,0,0,0,17,18,5,2,0,0,18,5,1,0,0,0,19,20,5,3,0,0,20,7,1,0,0,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
