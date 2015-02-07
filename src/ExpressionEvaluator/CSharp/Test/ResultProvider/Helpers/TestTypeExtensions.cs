// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis.ExpressionEvaluator;

namespace Microsoft.CodeAnalysis.CSharp.ExpressionEvaluator
{
    internal static class TestTypeExtensions
    {
        public static string GetTypeName(this Type type, bool escapeKeywordIdentifiers = false)
        {
            return CSharpFormatter.Instance.GetTypeName((TypeImpl)type, escapeKeywordIdentifiers);
        }
    }
}
