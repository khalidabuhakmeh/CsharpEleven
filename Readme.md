# C# 11 What's New Features

This is a project to show all the new C# 11 features coming in .NET 7 that are
mentioned in the [Microsoft Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-11#list-patterns).

The samples found in [Program.cs](CsharpEleven/Program.cs) are runnable using [JetBrains Rider 2022.3+](https://jetbrains.com/rider).

You can give things a try. 

1. File-scoped types
2. Generic Math support
3. Auto-default structs
4. Pattern matching Span<char> on a constant string
5. Extended nameof scope
6. Numeric IntPtr
7. Utf-8 string literals
8. Required Members
9. ref fields and scoped ref
10. Raw string literals
11. Improved method group conversion to delegate
12. Generic Attributes
13. Newlines in string interpolation expressions
14. List Patterns
15. Warning Wave 7 (lot's of Roslyn-based warnings about code issues. Not shown in this project.)

## Opinions

Most of the features in C# 11 seem to be focused on low-level optimization
and most folks will likely use a handful of these features, rather than "ALL OF THEM". 
Although, they will benefit from these features, as these are used internally by the .NET team to
optimize hot path scenarios. We all win! 

My favorite features that you'll likely use are:

- #10: Raw string literals (be sure to try it with Rider's language inject 👨🏻‍🍳😘)
- #11: Improved method group conversion to delegate. (You're likely already using it now. So free perf!)
- #12: Generic Attributes. Heck to the Yeah!
- #8: Required Members (less boilerplate is always good)
- #14: List Patterns (This is a "maybe", and LINQ is also a good option)

In general, this is a good iterative update to the C# language but nothing as dramatic as LINQ, pattern matching, async/await or records. That's likely a good thing for many developers still trying to catch up.