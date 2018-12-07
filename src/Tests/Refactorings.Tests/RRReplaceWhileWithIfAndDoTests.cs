﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Xunit;

namespace Roslynator.CSharp.Refactorings.Tests
{
    public class RRReplaceWhileWithIfAndDoTests : AbstractCSharpCodeRefactoringVerifier
    {
        public override string RefactoringId { get; } = RefactoringIdentifiers.ReplaceWhileWithIfAndDo;

        [Fact, Trait(Traits.Refactoring, RefactoringIdentifiers.ReplaceWhileWithIfAndDo)]
        public async Task Test()
        {
            await VerifyRefactoringAsync(@"
class C
{
    void M()
    {
        bool f = false;

        [||]while (f)
        {
            M();
        }
    }
}
", @"
class C
{
    void M()
    {
        bool f = false;

        if (f)
        {
            do
            {
                M();
            }
            while (f);
        }
    }
}
", equivalenceKey: RefactoringId);
        }
    }
}