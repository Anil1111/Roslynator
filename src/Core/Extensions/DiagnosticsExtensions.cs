﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator
{
    /// <summary>
    /// A set of extension methods for <see cref="SymbolAnalysisContext"/>, <see cref="SyntaxNodeAnalysisContext"/> and <see cref="SyntaxTreeAnalysisContext"/>.
    /// </summary>
    public static class DiagnosticsExtensions
    {
        #region SymbolAnalysisContext
        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="ISymbol"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="node"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SymbolAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxNode node,
            params object[] messageArgs)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: node.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="ISymbol"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="token"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SymbolAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxToken token,
            params object[] messageArgs)
        {
            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: token.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="ISymbol"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="trivia"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SymbolAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxTrivia trivia,
            params object[] messageArgs)
        {
            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: trivia.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="ISymbol"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor">A <see cref="DiagnosticDescriptor"/> describing the diagnostic.</param>
        /// <param name="location"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SymbolAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="ISymbol"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="additionalLocations"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SymbolAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            IEnumerable<Location> additionalLocations,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                additionalLocations: additionalLocations,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="ISymbol"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="properties"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SymbolAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            ImmutableDictionary<string, string> properties,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                properties: properties,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="ISymbol"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="additionalLocations"></param>
        /// <param name="properties"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SymbolAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            IEnumerable<Location> additionalLocations,
            ImmutableDictionary<string, string> properties,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                additionalLocations: additionalLocations,
                properties: properties,
                messageArgs: messageArgs));
        }
        #endregion SymbolAnalysisContext

        #region SyntaxNodeAnalysisContext
        internal static void ReportDiagnosticIfNotSuppressed(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxNode node,
            params object[] messageArgs)
        {
            if (context.IsAnalyzerSuppressed(descriptor))
                return;

            ReportDiagnostic(context, descriptor, node, messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="node"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxNode node,
            params object[] messageArgs)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: node.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="token"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxToken token,
            params object[] messageArgs)
        {
            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: token.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="trivia"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxTrivia trivia,
            params object[] messageArgs)
        {
            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: trivia.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="additionalLocations"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            IEnumerable<Location> additionalLocations,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                additionalLocations: additionalLocations,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="properties"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            ImmutableDictionary<string, string> properties,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                properties: properties,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxNode"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="additionalLocations"></param>
        /// <param name="properties"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxNodeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            IEnumerable<Location> additionalLocations,
            ImmutableDictionary<string, string> properties,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                additionalLocations: additionalLocations,
                properties: properties,
                messageArgs: messageArgs));
        }
        #endregion SyntaxNodeAnalysisContext

        #region SyntaxTreeAnalysisContext
        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxTree"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="node"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxTreeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxNode node,
            params object[] messageArgs)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: node.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxTree"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="token"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxTreeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxToken token,
            params object[] messageArgs)
        {
            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: token.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxTree"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="trivia"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxTreeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            SyntaxTrivia trivia,
            params object[] messageArgs)
        {
            ReportDiagnostic(
                context: context,
                descriptor: descriptor,
                location: trivia.GetLocation(),
                messageArgs: messageArgs);
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxTree"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxTreeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxTree"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="additionalLocations"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxTreeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            IEnumerable<Location> additionalLocations,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                additionalLocations: additionalLocations,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxTree"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="properties"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxTreeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            ImmutableDictionary<string, string> properties,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                properties: properties,
                messageArgs: messageArgs));
        }

        /// <summary>
        /// Report a <see cref="Diagnostic"/> about a <see cref="SyntaxTree"/>.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="descriptor"></param>
        /// <param name="location"></param>
        /// <param name="additionalLocations"></param>
        /// <param name="properties"></param>
        /// <param name="messageArgs"></param>
        public static void ReportDiagnostic(
            this SyntaxTreeAnalysisContext context,
            DiagnosticDescriptor descriptor,
            Location location,
            IEnumerable<Location> additionalLocations,
            ImmutableDictionary<string, string> properties,
            params object[] messageArgs)
        {
            context.ReportDiagnostic(Diagnostic.Create(
                descriptor: descriptor,
                location: location,
                additionalLocations: additionalLocations,
                properties: properties,
                messageArgs: messageArgs));
        }
        #endregion SyntaxTreeAnalysisContext

        internal static bool IsAnalyzerSuppressed(this SymbolAnalysisContext context, DiagnosticDescriptor descriptor)
        {
            return context.Compilation.IsAnalyzerSuppressed(descriptor);
        }

        internal static bool IsAnalyzerSuppressed(this SyntaxNodeAnalysisContext context, DiagnosticDescriptor descriptor)
        {
            return context.Compilation.IsAnalyzerSuppressed(descriptor);
        }

#pragma warning disable RS1012
        internal static bool IsAnalyzerSuppressed(this CompilationStartAnalysisContext context, DiagnosticDescriptor descriptor)
        {
            return context.Compilation.IsAnalyzerSuppressed(descriptor);
        }

        internal static bool AreAnalyzersSuppressed(this CompilationStartAnalysisContext context, ImmutableArray<DiagnosticDescriptor> descriptors)
        {
            return context.Compilation.AreAnalyzersSuppressed(descriptors);
        }

        internal static bool AreAnalyzersSuppressed(this CompilationStartAnalysisContext context, DiagnosticDescriptor descriptor1, DiagnosticDescriptor descriptor2)
        {
            return context.Compilation.AreAnalyzersSuppressed(descriptor1, descriptor2);
        }

        internal static bool AreAnalyzersSuppressed(this CompilationStartAnalysisContext context, DiagnosticDescriptor descriptor1, DiagnosticDescriptor descriptor2, DiagnosticDescriptor descriptor3)
        {
            return context.Compilation.AreAnalyzersSuppressed(descriptor1, descriptor2, descriptor3);
        }
#pragma warning restore RS1012
    }
}
