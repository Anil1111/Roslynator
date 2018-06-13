﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp.Analysis.Documentation;

namespace Roslynator.CSharp.Refactorings.DocumentationComment
{
    internal class AddTypeParameterToDocumentationCommentRefactoring : DocumentationCommentRefactoring<TypeParameterSyntax>
    {
        public override XmlElementKind ElementKind
        {
            get { return XmlElementKind.TypeParam; }
        }

        public override bool ShouldBeBefore(XmlElementKind elementKind)
        {
            return elementKind == XmlElementKind.Summary;
        }

        public override SeparatedSyntaxList<TypeParameterSyntax> GetContainingList(TypeParameterSyntax node)
        {
            var typeParameterList = (TypeParameterListSyntax)node.Parent;

            return typeParameterList.Parameters;
        }

        public override string GetName(TypeParameterSyntax node)
        {
            return node.Identifier.ValueText;
        }

        public override ElementInfo<TypeParameterSyntax> CreateInfo(TypeParameterSyntax node, int insertIndex, NewLinePosition newLinePosition)
        {
            return new TypeParamElementInfo(node, insertIndex, newLinePosition);
        }
    }
}