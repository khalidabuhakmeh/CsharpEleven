using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;
using System.Text;

/*
    # What's new in C# 11?
    
    1. File-scoped types
    2. Generic Math support
    3. Auto-default structs
    4. Pattern match Span<char> on a constant string
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
*/

Output.WriteLine("Hello, World!");

// 1. File-scoped types : "file" keyword
// File-scoped access restricts a top-level type's
// scope and visibility to the file in which it's declared.
file class JetBrains
{
    public bool IsAwesome { get; } = true;
}

public static class FileScopedClasses
{
    public static void Execute()
    {
        // I can only new this class up 
        // in this file. Attempting to create
        // this type in a new file will fail.
        var jb = new JetBrains();
        
        Output.WriteLine($"JetBrains is Awesome? {jb.IsAwesome}");
    }
} 

// 2. Generic Math Support
// using static member interfaces you can
// now constrain generic methods to the INumber interface
// making it easier to build generic math functions and
// reducing your codebase's size
public static class GenericMath
{
    private static T Add<T>(T left, T right)
        where T : INumber<T>
    {
        return left + right;
    }

    public static void Execute()
    {
        Output.WriteLine(Add(1f, 1f));
    }
}

// 3. Auto-default structs
// The C# 11 compiler ensures that all fields of a struct type
// are initialized to their default value as
// part of executing a constructor.
public static class StructWithoutNew
{
    public struct Coords
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public static void Execute()
    {
        // Property values set to the default
        // similar to class behaviors
        Coords p = new();
        Output.WriteLine($"({p.X}, {p.Y})"); // output: (0, 0)
    }
}

// 4. Pattern match Span<char> on a constant string
public static class PatternMatchingConstant
{
    public static void Execute()
    {
        //                                   1111111
        //                         012345 78 0123456
        ReadOnlySpan<char> name = "Khalid is Awesome";

        var result = name[10..17] is "Awesome";
        Output.WriteLine($"Is Khalid Awesome? {result}");
    }
}

// 5. Extended nameof scope
// Type parameter names and parameter names are now
// in scope when used in a nameof expression
public static class ExtendedNameOfScope
{
    [Display(Name = nameof(myFavoritePerson))]
    public static void Hello(string myFavoritePerson)
    {
        Output.WriteLine($"Hello, {myFavoritePerson}");
    }

    public static void Execute()
    {
        var method = typeof(ExtendedNameOfScope).GetMethod(nameof(Hello), BindingFlags.Static | BindingFlags.Public);
        var attribute = method!.GetCustomAttribute<DisplayAttribute>();

        Output.WriteLine($"The attribute name is \"{attribute!.Name}\"");
    }
}

// 6. Numeric IntPtr
// The nint and nuint types now alias
// System.IntPtr and System.UIntPtr, respectively.
public static class NumericIntPtr
{
    public static void Execute()
    {
        // cmd+click "nint" to go to IntPtr
        nint one = 1;
        // cmd+click "nuint" to go to UIntPtr
        nuint two = 2;
    }
}

// 7. Utf-8 string literals
// using the "u8" suffix on strings converts a string constant to 
// a ReadOnlySpan<byte> that can be more easily used in streaming scenarios
public static class Utf8StringLiterals
{
    public static void Execute()
    {
        // easily encode strings to UTF8 bytes
        ReadOnlySpan<byte> utf8 = "<h1>Hello, World</h1>"u8;
        var html = Encoding.UTF8.GetString(utf8);

        Output.WriteLine(html);
    }
}

// 8. Required Members
public static class RequiredMembers
{
    public class Previous
    {
        public string Name { get; }

        public Previous(string name)
        {
            Name = name;
        }
    }

    public class Updated
    {
        public required string Name { get; set; }
    }

    public static void Execute()
    {
        var prev = new Previous("Khalid");
        var updated = new Updated
        {
            // comment this line to get an error
            Name = "Khalid"
        };

        Output.WriteLine(updated.Name);
    }
}

// 9. ref fields and scoped ref
public static class RefFieldsAndScopedRef
{
    public readonly ref struct Example
    {
        public Example(ref byte[] bytes)
            => _bytes = bytes;

        // private fields can now use the ref keyword
        private readonly ref byte[] _bytes;
        public byte[] GetBytes() => _bytes is null ? Array.Empty<byte>() : _bytes;
    }
}

// 10. Raw String literals
public static class RawStringLiterals
{
    public static void Execute()
    {
        // language=html
        var html = """
        <html lang="en">
            <body>
                <main>
                    <h1>Hello, World!</h1>
                    <p>This is a Raw String Literal in C#</p>
                </main>
            </body>
        </html>
        """;
//👆 This whitespace before the """ will be trimmed

        Output.WriteLine(html);
    }
}

// 11. Improved method group conversion to delegate ✨Performance ✨
public static class MethodGroupConversion
{
    public static void Execute()
    {
        var list = new List<int> { 1, 2, 3, 4, 5 };

        var write = (int num) => Output.WriteLine(num);
        
        // Previously the method group would 
        // create a new delegate on every iteration
        // causing unnecessary allocations, this is no longer the case.
        // This could cause issues in code hot paths.
        // In short: method groups are more performant, yay! 👍
        list.ForEach(write);
    }
}

// 12. Generic Attributes
public static class GenericAttributes
{
    public class MyAttribute<T> : Attribute
    {
        public string Kind => nameof(T);
    }
    
    [MyAttribute<string>()]
    public class MyClass {}

    public static void Execute()
    {
        var attr = typeof(MyClass).GetCustomAttribute<MyAttribute<string>>();
        Output.WriteLine(attr!.Kind);
    }
}

// 13. Newlines in string interpolation expressions
public static class NewlinesInStringInterpolation
{
    public static void Execute()
    {
        var test = $"{
            (1/2) * 2
            +
            (1/2) * 4
        }";
        
        Output.WriteLine(test);
    }
}

// 14. List Patterns
// You can use the [] in combination with
//   - .. (any number of elements from 0 to *)
//   - _  (any single element)
//   - N  (any exact value)
// to match a collection of elements
public static class ListPatterns 
{
    public static void Execute()
    {
        var odd = new List<int> { 1, 3, 5 };
        var fib = new[] { 1, 1, 2, 3, 5 };
        
        Output.WriteLine(odd is [1, .., 3, _]); // true
        Output.WriteLine(fib is [1, .., 3, _]); // true

        Output.WriteLine(odd is [1, _, 5, ..]); // true
        Output.WriteLine(fib is [1, _, 5, ..]); // false
    }
}

public static class Output
{
    private static bool FirstRun { get; set; } = true;
    
    public static void WriteLine(object value)
    {
        if (FirstRun)
        {
            Console.Clear();
            FirstRun = false;
        }
        
        Console.WriteLine(value);
    }
}