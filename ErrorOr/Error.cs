using System;
using System.Collections.Generic;
using System.Text;

namespace ErrorOr
{
    /// <summary>
    /// Error types.
    /// </summary>
    public enum ErrorType
    {
        Failure,
        Unexpected,
        Validation,
        Conflict,
        NotFound,
        Unauthorized
    }

    /// <summary>
    /// Represents an error.
    /// </summary>
    /// <remarks>
    /// Back ported from "readonly record struct" in .NET 8. The type is reconstructed
    /// into equivalent code for .NET 4.8 as the compiler generates code that is compatible
    /// but not available in previous versions of C#.
    /// </remarks>
    public readonly struct Error : IEquatable<Error>
    {
        /// <summary>
        /// Gets the unique error code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the error description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the error type.
        /// </summary>
        public ErrorType Type { get; }

        /// <summary>
        /// Gets the numeric value of the type.
        /// </summary>
        public int NumericType { get; }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        public Dictionary<string, object> Metadata { get; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Error");
            stringBuilder.Append(" { ");
            if (PrintMembers(stringBuilder))
            {
                stringBuilder.Append(' ');
            }
            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }

        public static bool operator !=(Error left, Error right)
        {
            return !(left == right);
        }

        public static bool operator ==(Error left, Error right)
        {
            return left.Equals(right);
        }

        public override int GetHashCode()
        {
            return (((EqualityComparer<string>.Default.GetHashCode(Code) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description)) * -1521134295 + EqualityComparer<ErrorType>.Default.GetHashCode(Type)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(NumericType)) * -1521134295 + EqualityComparer<Dictionary<string, object>>.Default.GetHashCode(Metadata);
        }

        public override bool Equals(object obj)
        {
            return obj is Error error && Equals(error);
        }

        public bool Equals(Error other)
        {
            return EqualityComparer<string>.Default.Equals(Code, other.Code) && EqualityComparer<string>.Default.Equals(Description, other.Description) && EqualityComparer<ErrorType>.Default.Equals(Type, other.Type) && EqualityComparer<int>.Default.Equals(NumericType, other.NumericType) && EqualityComparer<Dictionary<string, object>>.Default.Equals(Metadata, other.Metadata);
        }

        /// <summary>
        /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Failure"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        public static Error Failure(
            string code = "General.Failure",
            string description = "A failure has occurred.",
            Dictionary<string, object> metadata = null) =>
            new Error(code, description, ErrorType.Failure, metadata);

        /// <summary>
        /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Unexpected"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        public static Error Unexpected(
            string code = "General.Unexpected",
            string description = "An unexpected error has occurred.",
            Dictionary<string, object> metadata = null) =>
                new Error(code, description, ErrorType.Unexpected, metadata);

        /// <summary>
        /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Validation"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        public static Error Validation(
            string code = "General.Validation",
            string description = "A validation error has occurred.",
            Dictionary<string, object> metadata = null) =>
                new Error(code, description, ErrorType.Validation, metadata);

        /// <summary>
        /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Conflict"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        public static Error Conflict(
            string code = "General.Conflict",
            string description = "A conflict error has occurred.",
            Dictionary<string, object> metadata = null) =>
                new Error(code, description, ErrorType.Conflict, metadata);

        /// <summary>
        /// Creates an <see cref="Error"/> of type <see cref="ErrorType.NotFound"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        public static Error NotFound(
            string code = "General.NotFound",
            string description = "A 'Not Found' error has occurred.",
            Dictionary<string, object> metadata = null) =>
                new Error(code, description, ErrorType.NotFound, metadata);

        /// <summary>
        /// Creates an <see cref="Error"/> of type <see cref="ErrorType.Unauthorized"/> from a code and description.
        /// </summary>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        public static Error Unauthorized(
            string code = "General.Unauthorized",
            string description = "An 'Unauthorized' error has occurred.",
            Dictionary<string, object> metadata = null) =>
                new Error(code, description, ErrorType.Unauthorized, metadata);

        /// <summary>
        /// Creates an <see cref="Error"/> with the given numeric <paramref name="type"/>,
        /// <paramref name="code"/>, and <paramref name="description"/>.
        /// </summary>
        /// <param name="type">An integer value which represents the type of error that occurred.</param>
        /// <param name="code">The unique error code.</param>
        /// <param name="description">The error description.</param>
        /// <param name="metadata">A dictionary which provides optional space for information.</param>
        public static Error Custom(
            int type,
            string code,
            string description,
            Dictionary<string, object> metadata = null) =>
                new Error(code, description, (ErrorType)type, metadata);

        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append("Code = ");
            builder.Append((object)Code);
            builder.Append(", Description = ");
            builder.Append((object)Description);
            builder.Append(", Type = ");
            builder.Append(Type.ToString());
            builder.Append(", NumericType = ");
            builder.Append(NumericType.ToString());
            builder.Append(", Metadata = ");
            builder.Append(Metadata);
            return true;
        }

        private Error(string code, string description, ErrorType type, Dictionary<string, object> metadata = null)
        {
            Code = code;
            Description = description;
            Type = type;
            NumericType = (int)type;
            Metadata = metadata;
        }
    }
}
