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

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="stParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IstVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] stParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.namespaceName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNamespaceName([NotNull] stParser.NamespaceNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.dataTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDataTypeDeclaration([NotNull] stParser.DataTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.typeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeDeclaration([NotNull] stParser.TypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.simpleTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleTypeDeclaration([NotNull] stParser.SimpleTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.simpleSpecInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleSpecInit([NotNull] stParser.SimpleSpecInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.simpleSpec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleSpec([NotNull] stParser.SimpleSpecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.elementTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElementTypeName([NotNull] stParser.ElementTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.numericTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericTypeName([NotNull] stParser.NumericTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.subrangeTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubrangeTypeDeclaration([NotNull] stParser.SubrangeTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.subrangeSpecInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubrangeSpecInit([NotNull] stParser.SubrangeSpecInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.subrangeSpec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubrangeSpec([NotNull] stParser.SubrangeSpecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.subrange"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubrange([NotNull] stParser.SubrangeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.enumTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumTypeDeclaration([NotNull] stParser.EnumTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.namedSpecInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNamedSpecInit([NotNull] stParser.NamedSpecInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.enumSpecInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumSpecInit([NotNull] stParser.EnumSpecInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.enumValueSpec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumValueSpec([NotNull] stParser.EnumValueSpecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.enumValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumValue([NotNull] stParser.EnumValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.arrayTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayTypeDeclaration([NotNull] stParser.ArrayTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.arraySpecInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArraySpecInit([NotNull] stParser.ArraySpecInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.arraySpec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArraySpec([NotNull] stParser.ArraySpecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.dataTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDataTypeAccess([NotNull] stParser.DataTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.arrayInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayInit([NotNull] stParser.ArrayInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.arrayElementInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayElementInit([NotNull] stParser.ArrayElementInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.arrayElementInitValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayElementInitValue([NotNull] stParser.ArrayElementInitValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructTypeDeclaration([NotNull] stParser.StructTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structSpec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructSpec([NotNull] stParser.StructSpecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structSpecInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructSpecInit([NotNull] stParser.StructSpecInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructDeclaration([NotNull] stParser.StructDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structElementDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructElementDeclaration([NotNull] stParser.StructElementDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.locatedAt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLocatedAt([NotNull] stParser.LocatedAtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.multibitPartAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultibitPartAccess([NotNull] stParser.MultibitPartAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structElementName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructElementName([NotNull] stParser.StructElementNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructInit([NotNull] stParser.StructInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structElementInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructElementInit([NotNull] stParser.StructElementInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.stringTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringTypeDeclaration([NotNull] stParser.StringTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefTypeDeclaration([NotNull] stParser.RefTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refSpecInit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefSpecInit([NotNull] stParser.RefSpecInitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refSpec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefSpec([NotNull] stParser.RefSpecContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefTypeName([NotNull] stParser.RefTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefTypeAccess([NotNull] stParser.RefTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.ref_Name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRef_Name([NotNull] stParser.Ref_NameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefValue([NotNull] stParser.RefValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refAddress"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefAddress([NotNull] stParser.RefAddressContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefAssign([NotNull] stParser.RefAssignContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.refDereference"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRefDereference([NotNull] stParser.RefDereferenceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.derivedTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDerivedTypeAccess([NotNull] stParser.DerivedTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.stringTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringTypeAccess([NotNull] stParser.StringTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.stringTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringTypeName([NotNull] stParser.StringTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.singleElementTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSingleElementTypeAccess([NotNull] stParser.SingleElementTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.simpleTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleTypeAccess([NotNull] stParser.SimpleTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.simpleTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleTypeName([NotNull] stParser.SimpleTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.subrangeTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubrangeTypeAccess([NotNull] stParser.SubrangeTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.subrangeTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubrangeTypeName([NotNull] stParser.SubrangeTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.enumTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumTypeAccess([NotNull] stParser.EnumTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.enumTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumTypeName([NotNull] stParser.EnumTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.arrayTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayTypeAccess([NotNull] stParser.ArrayTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.arrayTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayTypeName([NotNull] stParser.ArrayTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structTypeAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructTypeAccess([NotNull] stParser.StructTypeAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.structTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructTypeName([NotNull] stParser.StructTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.constExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstExpression([NotNull] stParser.ConstExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] stParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.literalValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralValue([NotNull] stParser.LiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.numericLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericLiteral([NotNull] stParser.NumericLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.intLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntLiteral([NotNull] stParser.IntLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.intLiteralValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntLiteralValue([NotNull] stParser.IntLiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.intTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIntTypeName([NotNull] stParser.IntTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.unsignedIntTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnsignedIntTypeName([NotNull] stParser.UnsignedIntTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.signedIntTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSignedIntTypeName([NotNull] stParser.SignedIntTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.multibitsLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultibitsLiteral([NotNull] stParser.MultibitsLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.multibitsLiteralValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultibitsLiteralValue([NotNull] stParser.MultibitsLiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.multibitsTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultibitsTypeName([NotNull] stParser.MultibitsTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.realLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRealLiteral([NotNull] stParser.RealLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.realLiteralValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRealLiteralValue([NotNull] stParser.RealLiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.realTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRealTypeName([NotNull] stParser.RealTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.boolLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolLiteral([NotNull] stParser.BoolLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.boolLiteralValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolLiteralValue([NotNull] stParser.BoolLiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.boolTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBoolTypeName([NotNull] stParser.BoolTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.charLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCharLiteral([NotNull] stParser.CharLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.charString"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCharString([NotNull] stParser.CharStringContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.charTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCharTypeName([NotNull] stParser.CharTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.timeLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimeLiteral([NotNull] stParser.TimeLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.durationLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDurationLiteral([NotNull] stParser.DurationLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.durationLiteralValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDurationLiteralValue([NotNull] stParser.DurationLiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.durationTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDurationTypeName([NotNull] stParser.DurationTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.timeOfDayLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimeOfDayLiteral([NotNull] stParser.TimeOfDayLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.timeOfDayLiteralValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimeOfDayLiteralValue([NotNull] stParser.TimeOfDayLiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.timeOfDayTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTimeOfDayTypeName([NotNull] stParser.TimeOfDayTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.dateLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDateLiteral([NotNull] stParser.DateLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.dateLiteralValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDateLiteralValue([NotNull] stParser.DateLiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.dateTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDateTypeName([NotNull] stParser.DateTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.dateAndTimeLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDateAndTimeLiteral([NotNull] stParser.DateAndTimeLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.dateAndTimeLiteralValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDateAndTimeLiteralValue([NotNull] stParser.DateAndTimeLiteralValueContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.dateAndTimeTypeName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDateAndTimeTypeName([NotNull] stParser.DateAndTimeTypeNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stParser.directVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDirectVariable([NotNull] stParser.DirectVariableContext context);
}
