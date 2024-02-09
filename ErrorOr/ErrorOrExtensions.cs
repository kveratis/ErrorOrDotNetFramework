using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErrorOr
{
    public static class ErrorOrExtensions
    {
        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> Else<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<List<Error>, TValue> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Else(onError);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> Else<TValue>(this Task<ErrorOr<TValue>> errorOr, TValue onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Else(onError);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> ElseAsync<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<List<Error>, Task<TValue>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> ElseAsync<TValue>(this Task<ErrorOr<TValue>> errorOr, Task<TValue> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> Else<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<List<Error>, Error> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Else(onError);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> Else<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<List<Error>, List<Error>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Else(onError);
        }

        /// <summary>
        /// If the state is error, the provided <paramref name="error"/> is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="error">The error to return.</param>
        /// <returns>The given <paramref name="error"/>.</returns>
        public static async Task<ErrorOr<TValue>> Else<TValue>(this Task<ErrorOr<TValue>> errorOr, Error error)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Else(error);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> ElseAsync<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<List<Error>, Task<Error>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> ElseAsync<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<List<Error>, Task<List<Error>>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state is error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onError">The function to execute if the state is error.</param>
        /// <returns>The result from calling <paramref name="onError"/> if state is error; otherwise the original value.</returns>
        public static async Task<ErrorOr<TValue>> ElseAsync<TValue>(this Task<ErrorOr<TValue>> errorOr, Task<Error> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ElseAsync(onError).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the appropriate function based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// If the state is an error, the provided function <paramref name="onError"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onError"/> and <paramref name="onValue"/> functions.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <param name="onError">The function to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public static async Task<TNextValue> Match<TValue, TNextValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Match(onValue, onError);
        }

        /// <summary>
        /// Asynchronously executes the appropriate function based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// If the state is an error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onError"/> and <paramref name="onValue"/> functions.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <param name="onError">The function to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public static async Task<TNextValue> MatchAsync<TValue, TNextValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, Task<TNextValue>> onValue, Func<List<Error>, Task<TNextValue>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.MatchAsync(onValue, onError);
        }

        /// <summary>
        /// Executes the appropriate function based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// If the state is an error, the provided function <paramref name="onError"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onError"/> and <paramref name="onValue"/> functions.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <param name="onError">The function to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public static async Task<TNextValue> MatchFirst<TValue, TNextValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, TNextValue> onValue, Func<Error, TNextValue> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.MatchFirst(onValue, onError);
        }

        /// <summary>
        /// Asynchronously executes the appropriate function based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// If the state is an error, the provided function <paramref name="onError"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onError"/> and <paramref name="onValue"/> functions.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <param name="onError">The function to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public static async Task<TNextValue> MatchFirstAsync<TValue, TNextValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, Task<TNextValue>> onValue, Func<Error, Task<TNextValue>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.MatchFirstAsync(onValue, onError);
        }

        /// <summary>
        /// Executes the appropriate action based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is an error, the provided action <paramref name="onError"/> is executed.
        /// If the state is a value, the provided action <paramref name="onValue"/> is executed.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The action to execute if the state is a value.</param>
        /// <param name="onError">The action to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public static async Task Switch<TValue>(this Task<ErrorOr<TValue>> errorOr, Action<TValue> onValue, Action<List<Error>> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            result.Switch(onValue, onError);
        }

        /// <summary>
        /// Executes the appropriate action based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is an error, the provided action <paramref name="onError"/> is executed.
        /// If the state is a value, the provided action <paramref name="onValue"/> is executed.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The action to execute if the state is a value.</param>
        /// <param name="onError">The action to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public static async Task SwitchAsync<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, Task> onValue, Func<List<Error>, Task> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            await result.SwitchAsync(onValue, onError);
        }

        /// <summary>
        /// Executes the appropriate action based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is an error, the provided action <paramref name="onError"/> is executed.
        /// If the state is a value, the provided action <paramref name="onValue"/> is executed.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The action to execute if the state is a value.</param>
        /// <param name="onError">The action to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public static async Task SwitchFirst<TValue>(this Task<ErrorOr<TValue>> errorOr, Action<TValue> onValue, Action<Error> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            result.SwitchFirst(onValue, onError);
        }

        /// <summary>
        /// Executes the appropriate action based on the state of the <see cref="ErrorOr{TValue}"/>.
        /// If the state is an error, the provided action <paramref name="onError"/> is executed.
        /// If the state is a value, the provided action <paramref name="onValue"/> is executed.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The action to execute if the state is a value.</param>
        /// <param name="onError">The action to execute if the state is an error.</param>
        /// <returns>The result of the executed function.</returns>
        public static async Task SwitchFirstAsync<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, Task> onValue, Func<Error, Task> onError)
        {
            var result = await errorOr.ConfigureAwait(false);

            await result.SwitchFirstAsync(onValue, onError);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onValue"/> function.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original errors.</returns>
        public static async Task<ErrorOr<TNextValue>> Then<TValue, TNextValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, ErrorOr<TNextValue>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Then(onValue);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided function <paramref name="onValue"/> is executed and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onValue"/> function.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original errors.</returns>
        public static async Task<ErrorOr<TNextValue>> Then<TValue, TNextValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, TNextValue> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Then(onValue);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided <paramref name="action"/> is invoked.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="action">The action to execute if the state is a value.</param>
        /// <returns>The original <paramref name="errorOr"/>.</returns>
        public static async Task<ErrorOr<TValue>> Then<TValue>(this Task<ErrorOr<TValue>> errorOr, Action<TValue> action)
        {
            var result = await errorOr.ConfigureAwait(false);

            return result.Then(action);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onValue"/> function.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original errors.</returns>
        public static async Task<ErrorOr<TNextValue>> ThenAsync<TValue, TNextValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, Task<ErrorOr<TNextValue>>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ThenAsync(onValue).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided function <paramref name="onValue"/> is executed asynchronously and its result is returned.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <typeparam name="TNextValue">The type of the result from invoking the <paramref name="onValue"/> function.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="onValue">The function to execute if the state is a value.</param>
        /// <returns>The result from calling <paramref name="onValue"/> if state is value; otherwise the original errors.</returns>
        public static async Task<ErrorOr<TNextValue>> ThenAsync<TValue, TNextValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, Task<TNextValue>> onValue)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ThenAsync(onValue).ConfigureAwait(false);
        }

        /// <summary>
        /// If the state of <paramref name="errorOr"/> is a value, the provided <paramref name="action"/> is executed asynchronously.
        /// </summary>
        /// <typeparam name="TValue">The type of the underlying value in the <paramref name="errorOr"/>.</typeparam>
        /// <param name="errorOr">The <see cref="ErrorOr"/> instance.</param>
        /// <param name="action">The action to execute if the state is a value.</param>
        /// <returns>The original <paramref name="errorOr"/>.</returns>
        public static async Task<ErrorOr<TValue>> ThenAsync<TValue>(this Task<ErrorOr<TValue>> errorOr, Func<TValue, Task> action)
        {
            var result = await errorOr.ConfigureAwait(false);

            return await result.ThenAsync(action).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates an <see cref="ErrorOr{TValue}"/> instance with the given <paramref name="value"/>.
        /// </summary>
        public static ErrorOr<TValue> ToErrorOr<TValue>(this TValue value)
        {
            return value;
        }

        /// <summary>
        /// Creates an <see cref="ErrorOr{TValue}"/> instance with the given <paramref name="error"/>.
        /// </summary>
        public static ErrorOr<TValue> ToErrorOr<TValue>(this Error error)
        {
            return error;
        }

        /// <summary>
        /// Creates an <see cref="ErrorOr{TValue}"/> instance with the given <paramref name="error"/>.
        /// </summary>
        public static ErrorOr<TValue> ToErrorOr<TValue>(this List<Error> error)
        {
            return error;
        }
    }
}
