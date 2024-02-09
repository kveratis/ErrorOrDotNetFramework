using System;

namespace ErrorOr
{
    /// <summary>
    /// Generic Success result
    /// </summary>
    /// <remarks>
    /// Back ported from "readonly record struct" in .NET 8. The type is reconstructed
    /// into equivalent code for .NET 4.8 as the compiler generates code that is compatible
    /// but not available in previous versions of C#.
    /// </remarks>
    public readonly struct Success : IEquatable<Success>
    {
        public override string ToString()
        {
            return "Success";
        }

        public static bool operator !=(Success left, Success right)
        {
            return !(left == right);
        }

        public static bool operator ==(Success left, Success right)
        {
            return left.Equals(right);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Success success && Equals(success);
        }

        public bool Equals(Success other)
        {
            return true;
        }
    }

    /// <summary>
    /// Generic Created result
    /// </summary>
    /// <remarks>
    /// Back ported from "readonly record struct" in .NET 8. The type is reconstructed
    /// into equivalent code for .NET 4.8 as the compiler generates code that is compatible
    /// but not available in previous versions of C#.
    /// </remarks>
    public readonly struct Created : IEquatable<Created>
    {
        public override string ToString()
        {
            return "Created";
        }

        public static bool operator !=(Created left, Created right)
        {
            return !(left == right);
        }

        public static bool operator ==(Created left, Created right)
        {
            return left.Equals(right);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Created created && Equals(created);
        }

        public bool Equals(Created other)
        {
            return true;
        }
    }

    /// <summary>
    /// Generic Deleted result
    /// </summary>
    /// <remarks>
    /// Back ported from "readonly record struct" in .NET 8. The type is reconstructed
    /// into equivalent code for .NET 4.8 as the compiler generates code that is compatible
    /// but not available in previous versions of C#.
    /// </remarks>
    public readonly struct Deleted : IEquatable<Deleted>
    {
        public override string ToString()
        {
            return "Deleted";
        }

        public static bool operator !=(Deleted left, Deleted right)
        {
            return !(left == right);
        }

        public static bool operator ==(Deleted left, Deleted right)
        {
            return left.Equals(right);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Deleted deleted && Equals(deleted);
        }

        public bool Equals(Deleted other)
        {
            return true;
        }
    }

    /// <summary>
    /// Generic Updated result
    /// </summary>
    /// <remarks>
    /// Back ported from "readonly record struct" in .NET 8. The type is reconstructed
    /// into equivalent code for .NET 4.8 as the compiler generates code that is compatible
    /// but not available in previous versions of C#.
    /// </remarks>
    public readonly struct Updated : IEquatable<Updated>
    {
        public override string ToString()
        {
            return "Updated";
        }

        public static bool operator !=(Updated left, Updated right)
        {
            return !(left == right);
        }

        public static bool operator ==(Updated left, Updated right)
        {
            return left.Equals(right);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is Updated updated && Equals(updated);
        }

        public bool Equals(Updated other)
        {
            return true;
        }
    }

    /// <summary>
    /// Commonly used generic results for <see cref="ErrorOr{TValue}"/>
    /// </summary>
    public static class Result
    {
        public static Success Success => default;

        public static Created Created => default;

        public static Deleted Deleted => default;

        public static Updated Updated => default;
    }
}
