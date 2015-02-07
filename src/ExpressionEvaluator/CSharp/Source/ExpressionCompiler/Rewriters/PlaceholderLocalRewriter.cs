// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Symbols;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.CSharp.ExpressionEvaluator
{
    internal sealed class PlaceholderLocalRewriter : BoundTreeRewriter
    {
        internal static BoundNode Rewrite(CSharpCompilation compilation, EENamedTypeSymbol container, HashSet<LocalSymbol> declaredLocals, BoundNode node)
        {
            var rewriter = new PlaceholderLocalRewriter(compilation, container, declaredLocals);
            return rewriter.Visit(node);
        }

        private readonly CSharpCompilation _compilation;
        private readonly EENamedTypeSymbol _container;
        private readonly HashSet<LocalSymbol> _declaredLocals;

        private PlaceholderLocalRewriter(CSharpCompilation compilation, EENamedTypeSymbol container, HashSet<LocalSymbol> declaredLocals)
        {
            _compilation = compilation;
            _container = container;
            _declaredLocals = declaredLocals;
        }

        public override BoundNode VisitLocal(BoundLocal node)
        {
            var result = RewriteLocal(node);
            Debug.Assert(result.Type == node.Type);
            return result;
        }

        private BoundExpression RewriteLocal(BoundLocal node)
        {
            var local = node.LocalSymbol;
            var placeholder = local as PlaceholderLocalSymbol;
            if ((object)placeholder != null)
            {
                return placeholder.RewriteLocal(_compilation, _container, node.Syntax);
            }
            if (_declaredLocals.Contains(local))
            {
                return ObjectIdLocalSymbol.RewriteLocal(_compilation, _container, node.Syntax, local);
            }
            return node;
        }
    }
}
