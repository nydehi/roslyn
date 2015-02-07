﻿Imports System.Collections.Immutable
Imports Microsoft.CodeAnalysis.VisualBasic.Symbols
Imports Roslyn.Utilities

Namespace Microsoft.CodeAnalysis.VisualBasic.ExpressionEvaluator
    Friend NotInheritable Class EELocalConstantSymbol
        Inherits EELocalSymbolBase

        Private ReadOnly _name As String
        Private ReadOnly _constantValue As ConstantValue

        Public Sub New(
            method As MethodSymbol,
            name As String,
            type As TypeSymbol,
            constantValue As ConstantValue)

            MyBase.New(method, type)

            Debug.Assert(name IsNot Nothing)
            Debug.Assert(method IsNot Nothing)
            Debug.Assert(type IsNot Nothing)
            Debug.Assert(constantValue IsNot Nothing)

            _name = name
            _constantValue = constantValue
        End Sub

        Friend Overrides ReadOnly Property DeclarationKind As LocalDeclarationKind
            Get
                Return LocalDeclarationKind.Constant
            End Get
        End Property

        Friend Overrides Function ToOtherMethod(method As MethodSymbol, typeMap As TypeSubstitution) As EELocalSymbolBase
            Dim type = typeMap.SubstituteType(Me.Type)
            Return New EELocalConstantSymbol(method, _name, type, _constantValue)
        End Function

        Public Overrides ReadOnly Property Name As String
            Get
                Return _name
            End Get
        End Property

        Friend Overrides ReadOnly Property IdentifierToken As SyntaxToken
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides ReadOnly Property DeclaringSyntaxReferences As ImmutableArray(Of SyntaxReference)
            Get
                Return ImmutableArray(Of SyntaxReference).Empty
            End Get
        End Property

        Friend Overrides ReadOnly Property IdentifierLocation As Location
            Get
                Throw ExceptionUtilities.Unreachable
            End Get
        End Property

        Public Overrides ReadOnly Property Locations As ImmutableArray(Of Location)
            Get
                Return NoLocations
            End Get
        End Property

        Friend Overrides Function GetConstantValue(binder As Binder) As ConstantValue
            Return _constantValue
        End Function

    End Class
End Namespace

