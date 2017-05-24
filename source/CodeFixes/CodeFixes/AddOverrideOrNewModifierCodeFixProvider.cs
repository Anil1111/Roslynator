﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp.Comparers;

namespace Roslynator.CSharp.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddOverrideOrNewModifierCodeFixProvider))]
    [Shared]
    public class AddOverrideOrNewModifierCodeFixProvider : BaseCodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(CSharpErrorCodes.MemberHidesInheritedMember); }
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            if (!Settings.IsAnyCodeFixEnabled(
                CodeFixIdentifiers.AddOverrideModifier,
                CodeFixIdentifiers.AddNewModifier))
            {
                return;
            }

            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            MemberDeclarationSyntax memberDeclaration = root
                .FindNode(context.Span, getInnermostNodeForTie: true)?
                .FirstAncestorOrSelf<MemberDeclarationSyntax>();

            Debug.Assert(memberDeclaration != null, $"{nameof(memberDeclaration)} is null");

            if (memberDeclaration == null)
                return;

            foreach (Diagnostic diagnostic in context.Diagnostics)
            {
                switch (diagnostic.Id)
                {
                    case CSharpErrorCodes.MemberHidesInheritedMember:
                        {
                            if (Settings.IsCodeFixEnabled(CodeFixIdentifiers.AddOverrideModifier))
                            {
                                CodeAction codeAction = CodeAction.Create(
                                    "Add 'override' modifier",
                                    cancellationToken =>
                                    {
                                        SyntaxTokenList newModifiers = memberDeclaration.GetModifiers().InsertModifier(SyntaxKind.OverrideKeyword, ModifierComparer.Instance);

                                        MemberDeclarationSyntax newNode = memberDeclaration.WithModifiers(newModifiers);

                                        return context.Document.ReplaceNodeAsync(memberDeclaration, newNode, context.CancellationToken);
                                    },
                                    CodeFixIdentifiers.AddOverrideModifier + EquivalenceKeySuffix);

                                context.RegisterCodeFix(codeAction, diagnostic);
                            }

                            if (Settings.IsCodeFixEnabled(CodeFixIdentifiers.AddNewModifier))
                            {
                                CodeAction codeAction = CodeAction.Create(
                                    "Add 'new' modifier",
                                    cancellationToken =>
                                    {
                                        SyntaxTokenList newModifiers = memberDeclaration.GetModifiers().InsertModifier(SyntaxKind.NewKeyword, ModifierComparer.Instance);

                                        MemberDeclarationSyntax newNode = memberDeclaration.WithModifiers(newModifiers);

                                        return context.Document.ReplaceNodeAsync(memberDeclaration, newNode, context.CancellationToken);
                                    },
                                    CodeFixIdentifiers.AddNewModifier + EquivalenceKeySuffix);

                                context.RegisterCodeFix(codeAction, diagnostic);
                            }

                            break;
                        }
                }
            }
        }
    }
}
