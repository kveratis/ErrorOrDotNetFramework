﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorOr
{
    /// <summary>
    /// A discriminated union of errors or a value.
    /// </summary>
    /// <remarks>
    /// Back ported from "readonly record struct" in .NET 8. The type is reconstructed
    /// into equivalent code for .NET 4.8 as the compiler generates code that is compatible
    /// but not available in previous versions of C#.
    /// </remarks>
    public readonly struct ErrorOr<TValue> : IErrorOr<TValue>, IEquatable<ErrorOr<TValue>>
    {
        private readonly TValue _value;
        private readonly List<Error> _errors;

        private static readonly Error NoFirstError = Error.Unexpected(
            code: "ErrorOr.NoFirstError",
            description: "First error cannot be retrieved from a successful ErrorOr.");

        private static readonly Error NoErrors = Error.Unexpected(
            code: "ErrorOr.NoErrors",
            description: "Error list cannot be retrieved from a successful ErrorOr.");

        /// <summary>
        /// Gets a value indicating whether the state is error.
        /// </summary>
        public bool IsError { get; }

        /// <summary>
        /// Gets the list of errors. If the state is not error, the list will contain a single error representing the state.
        /// </summary>
        public List<Error> Errors => IsError ? _errors : new List<Error> { NoErrors };

        /// <summary>
        /// Gets the list of errors. If the state is not error, the list will be empty.
        /// </summary>
        public List<Error> ErrorsOrEmptyList => IsError ? _errors : new List<Error>();

        /// <summary>
        /// Creates an <see cref="ErrorOr{TValue}"/> from a list of errors.
        /// </summary>
        public static ErrorOr<TValue> From(List<Error> errors)
        {
            return errors;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public TValue Value => _value;

        /// <summary>
        /// Gets the first error.
        /// </summary>
        public Error FirstError => !IsError ? NoFirstError : _errors[0];

        private ErrorOr(Error error)
        {
            _value = default;
            _errors = new List<Error> { error };
            IsError = true;
        }

        private ErrorOr(List<Error> errors)
        {
            _value = default;
            _errors = errors;
            IsError = true;
        }

        private ErrorOr(TValue value)
        {
            _value = value;
            _errors = null;
            IsError = false;
        }

        /// <summary>
        /// Creates an <see cref="ErrorOr{TValue}"/> from a value.
        /// </summary>
        public static implicit operator ErrorOr<TValue>(TValue value)
        {
            return new ErrorOr<TValue>(value);
        }

        /// <summary>
        /// Creates an <see cref="ErrorOr{TValue}"/> from an error.
        /// </summary>
        public static implicit operator ErrorOr<TValue>(Error error)
        {
            return new ErrorOr<TValue>(error);
        }

        /// <summary>
        /// Creates an <see cref="ErrorOr{TValue}"/> from a list of errors.
        /// </summary>
        public static implicit operator ErrorOr<TValue>(List<Error> errors)
        {
            return new ErrorOr<TValue>(errors);
        }

        /// <summary>
        /// Creates an <see cref="ErrorOr{TValue}"/> from a list of errors.
        /// </summary>
        public static implicit operator ErrorOr<TValue>(Error[] errors)
        {
            return new ErrorOr<TValue>(errors.ToList());
        }

        /// <summary>
        /// Executes the appropriate action based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is an error, the provided action <paramref name="onError"/> is executed.
        /// If the state is a value, the provided action <paramref name="onValue"/> is executed.
        /// </summary>
        /// <param name="onValue">The action to execute if the state is a value.</param>
        /// <param name="onError">The action to execute if the state is an error.</param>
        public void Switch(Action<TValue> onValue, Action<List<Error>> onError)
        {
            if (IsError)
            {
                onError(Errors);
                return;
            }

            onValue(Value);
        }

        /// <summary>
        /// Asynchronously executes the appropriate action based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is an error, the provided action <paramref name="onError"/> is executed asynchronously.
        /// If the state is a value, the provided action <paramref name="onValue"/> is executed asynchronously.
        /// </summary>
        /// <param name="onValue">The asynchronous action to execute if the state is a value.</param>
        /// <param name="onError">The asynchronous action to execute if the state is an error.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SwitchAsync(Func<TValue, Task> onValue, Func<List<Error>, Task> onError)
        {
            if (IsError)
            {
                await onError(Errors).ConfigureAwait(false);
                return;
            }

            await onValue(Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the appropriate action based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is an error, the provided action <paramref name="onFirstError"/> is executed using the first error as input.
        /// If the state is a value, the provided action <paramref name="onValue"/> is executed.
        /// </summary>
        /// <param name="onValue">The action to execute if the state is a value.</param>
        /// <param name="onFirstError">The action to execute with the first error if the state is an error.</param>
        public void SwitchFirst(Action<TValue> onValue, Action<Error> onFirstError)
        {
            if (IsError)
            {
                onFirstError(FirstError);
                return;
            }

            onValue(Value);
        }

        /// <summary>
        /// Asynchronously executes the appropriate action based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is an error, the provided action <paramref name="onFirstError"/> is executed asynchronously using the first error as input.
        /// If the state is a value, the provided action <paramref name="onValue"/> is executed asynchronously.
        /// </summary>
        /// <param name="onValue">The asynchronous action to execute if the state is a value.</param>
        /// <param name="onFirstError">The asynchronous action to execute with the first error if the state is an error.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SwitchFirstAsync(Func<TValue, Task> onValue, Func<Error, Task> onFirstError)
        {
            if (IsError)
            {
                await onFirstError(FirstError).ConfigureAwait(false);
                return;
            }

            await onValue(Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the appropriate function based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// If the state is an error, the provided function <paramref name="onError"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TNextValue">The type of the result.</typeparam>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <param name="onError">The function to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public TNextValue Match<TNextValue>(Func<TValue, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
        {
            if (IsError)
            {
                return onError(Errors);
            }

            return onValue(Value);
        }

        /// <summary>
        /// Asynchronously executes the appropriate function based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// If the state is an error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TNextValue">The type of the result.</typeparam>
        /// <param name="onValue">The asynchronous function to execute if the state is a value.</param>
        /// <param name="onError">The asynchronous function to execute if the state is an error.</param>
        /// <returns>A task representing the asynchronous operation that yields the result of the executed function.</returns>
        public async Task<TNextValue> MatchAsync<TNextValue>(Func<TValue, Task<TNextValue>> onValue, Func<List<Error>, Task<TNextValue>> onError)
        {
            if (IsError)
            {
                return await onError(Errors).ConfigureAwait(false);
            }

            return await onValue(Value).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the appropriate function based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// If the state is an error, the provided function <paramref name="onFirstError"/> is executed using the first error, and its result is returned.
        /// </summary>
        /// <typeparam name="TNextValue">The type of the result.</typeparam>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <param name="onFirstError">The function to execute with the first error if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public TNextValue MatchFirst<TNextValue>(Func<TValue, TNextValue> onValue, Func<Error, TNextValue> onFirstError)
        {
            if (IsError)
            {
                return onFirstError(FirstError);
            }

            return onValue(Value);
        }

        /// <summary>
        /// Asynchronously executes the appropriate function based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// If the state is an error, the provided function <paramref name="onFirstError"/> is executed asynchronously using the first error, and its result is returned.
        /// </summary>
        /// <typeparam name="TNextValue">The type of the result.</typeparam>
        /// <param name="onValue">The asynchronous function to execute if the state is a value.</param>
        /// <param name="onFirstError">The asynchronous function to execute with the first error if the state is an error.</param>
        /// <returns>A task representing the asynchronous operation that yields the result of the executed function.</returns>
        public async Task<TNextValue> MatchFirstAsync<TNextValue>(Func<TValue, Task<TNextValue>> onValue, Func<Error, Task<TNextValue>> onFirstError)
        {
            if (IsError)
            {
                return await onFirstError(FirstError).ConfigureAwait(false);
            }

            return await onValue(Value).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TNextValue">The type of the result.</typeparam>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original <see cref="Errors"/>.</returns>
        public ErrorOr<TNextValue> Then<TNextValue>(Func<TValue, ErrorOr<TNextValue>> onValue)
        {
            if (IsError)
            {
                return Errors;
            }

            return onValue(Value);
        }

        /// <summary>
        /// If the state is a value, the provided <paramref name="action"/> is invoked.
        /// </summary>
        /// <param name="action">The action to execute if the state is a value.</param>
        /// <returns>The original <see cref="ErrorOr"/> instance.</returns>
        public ErrorOr<TValue> Then(Action<TValue> action)
        {
            if (IsError)
            {
                return Errors;
            }

            action(Value);

            return this;
        }

        /// <summary>
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TNextValue">The type of the result.</typeparam>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original <see cref="Errors"/>.</returns>
        public ErrorOr<TNextValue> Then<TNextValue>(Func<TValue, TNextValue> onValue)
        {
            if (IsError)
            {
                return Errors;
            }

            return onValue(Value);
        }

        /// <summary>
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TNextValue">The type of the result.</typeparam>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original <see cref="Errors"/>.</returns>
        public async Task<ErrorOr<TNextValue>> ThenAsync<TNextValue>(Func<TValue, Task<ErrorOr<TNextValue>>> onValue)
        {
            if (IsError)
            {
                return Errors;
            }

            return await onValue(Value).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is a value, the provided <paramref name="action"/> is invoked asynchronously.
        /// </summary>
        /// <param name="action">The action to execute if the state is a value.</param>
        /// <returns>The original <see cref="ErrorOr"/> instance.</returns>
        public async Task<ErrorOr<TValue>> ThenAsync(Func<TValue, Task> action)
        {
            if (IsError)
            {
                return Errors;
            }

            await action(Value).ConfigureAwait(false);

            return this;
        }

        /// <summary>
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TNextValue">The type of the result.</typeparam>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original <see cref="Errors"/>.</returns>
        public async Task<ErrorOr<TNextValue>> ThenAsync<TNextValue>(Func<TValue, Task<TNextValue>> onValue)
        {
            if (IsError)
            {
                return Errors;
            }

            return await onValue(Value).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed and its result is returned.
        /// </summary>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original <see cref="Value"/>.</returns>
        public ErrorOr<TValue> Else(Func<List<Error>, Error> onError)
        {
            if (!IsError)
            {
                return Value;
            }

            return onError(Errors);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed and its result is returned.
        /// </summary>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original <see cref="Value"/>.</returns>
        public ErrorOr<TValue> Else(Func<List<Error>, List<Error>> onError)
        {
            if (!IsError)
            {
                return Value;
            }

            return onError(Errors);
        }

        /// <summary>
        /// If the state is error, the provided <paramref name="error"/> is returned.
        /// </summary>
        /// <param name="error">The error to return.</param>
        /// <returns>The given <paramref name="error"/>.</returns>
        public ErrorOr<TValue> Else(Error error)
        {
            if (!IsError)
            {
                return Value;
            }

            return error;
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed and its result is returned.
        /// </summary>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original <see cref="Value"/>.</returns>
        public ErrorOr<TValue> Else(Func<List<Error>, TValue> onError)
        {
            if (!IsError)
            {
                return Value;
            }

            return onError(Errors);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed and its result is returned.
        /// </summary>
        /// <param name="onError">The value to return if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original <see cref="Value"/>.</returns>
        public ErrorOr<TValue> Else(TValue onError)
        {
            if (!IsError)
            {
                return Value;
            }

            return onError;
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original <see cref="Value"/>.</returns>
        public async Task<ErrorOr<TValue>> ElseAsync(Func<List<Error>, Task<TValue>> onError)
        {
            if (!IsError)
            {
                return Value;
            }

            return await onError(Errors).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original <see cref="Value"/>.</returns>
        public async Task<ErrorOr<TValue>> ElseAsync(Func<List<Error>, Task<Error>> onError)
        {
            if (!IsError)
            {
                return Value;
            }

            return await onError(Errors).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original <see cref="Value"/>.</returns>
        public async Task<ErrorOr<TValue>> ElseAsync(Func<List<Error>, Task<List<Error>>> onError)
        {
            if (!IsError)
            {
                return Value;
            }

            return await onError(Errors).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided <paramref name="error"/> is awaited and returned.
        /// </summary>
        /// <param name="error">The error to return if the state is error.</param>
        /// <returns>The result from awaiting the given <paramref name="error"/>.</returns>
        public async Task<ErrorOr<TValue>> ElseAsync(Task<Error> error)
        {
            if (!IsError)
            {
                return Value;
            }

            return await error.ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original <see cref="Value"/>.</returns>
        public async Task<ErrorOr<TValue>> ElseAsync(Task<TValue> onError)
        {
            if (!IsError)
            {
                return Value;
            }

            return await onError.ConfigureAwait(false);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("ErrorOr");
            stringBuilder.Append(" { ");
            if (PrintMembers(stringBuilder))
            {
                stringBuilder.Append(' ');
            }
            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }

        public static bool operator !=(ErrorOr<TValue> left, ErrorOr<TValue> right)
        {
            return !(left == right);
        }

        public static bool operator ==(ErrorOr<TValue> left, ErrorOr<TValue> right)
        {
            return left.Equals(right);
        }

        public override int GetHashCode()
        {
            return (EqualityComparer<TValue>.Default.GetHashCode(_value) * -1521134295 + EqualityComparer<List<Error>>.Default.GetHashCode(_errors)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(IsError);
        }

        public override bool Equals(object obj)
        {
            return obj is ErrorOr<TValue> or && Equals(or);
        }

        public bool Equals(ErrorOr<TValue> other)
        {
            return EqualityComparer<TValue>.Default.Equals(_value, other._value) && EqualityComparer<List<Error>>.Default.Equals(_errors, other._errors) && EqualityComparer<bool>.Default.Equals(IsError, other.IsError);
        }

        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append("IsError = ");
            builder.Append(IsError.ToString());
            builder.Append(", Errors = ");
            builder.Append(Errors);
            builder.Append(", ErrorsOrEmptyList = ");
            builder.Append(ErrorsOrEmptyList);
            builder.Append(", Value = ");
            builder.Append(Value);
            builder.Append(", FirstError = ");
            builder.Append(FirstError.ToString());
            return true;
        }
    }
}
