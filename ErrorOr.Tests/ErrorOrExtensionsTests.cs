using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ErrorOr.Tests
{
    public sealed class ErrorOrExtensionsTests
    {
        private sealed class Person
        {
            public Person(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }

        [Fact]
        public void CallingElseWithValueFunc_WhenIsSuccess_ShouldNotInvokeElseFunc()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else(errors => $"Error count: {errors.Count}");

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo(errorOrString.Value);
        }

        [Fact]
        public void CallingElseWithValueFunc_WhenIsError_ShouldReturnElseValue()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else(errors => $"Error count: {errors.Count}");

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo("Error count: 1");
        }

        [Fact]
        public void CallingElseWithValue_WhenIsSuccess_ShouldNotReturnElseValue()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else("oh no");

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo(errorOrString.Value);
        }

        [Fact]
        public void CallingElseWithValue_WhenIsError_ShouldReturnElseValue()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else("oh no");

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo("oh no");
        }

        [Fact]
        public void CallingElseWithError_WhenIsError_ShouldReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else(Error.Unexpected());

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public void CallingElseWithError_WhenIsSuccess_ShouldNotReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else(Error.Unexpected());

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(errorOrString.Value);
        }

        [Fact]
        public void CallingElseWithErrorsFunc_WhenIsError_ShouldReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else(errors => Error.Unexpected());

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public void CallingElseWithErrorsFunc_WhenIsSuccess_ShouldNotReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else(errors => Error.Unexpected());

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(errorOrString.Value);
        }

        [Fact]
        public void CallingElseWithErrorsFunc_WhenIsError_ShouldReturnElseErrors()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else(errors => new List<Error> { Error.Unexpected() });

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public void CallingElseWithErrorsFunc_WhenIsSuccess_ShouldNotReturnElseErrors()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => ConvertToString(num))
                .Else(errors => new List<Error> { Error.Unexpected() });

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(errorOrString.Value);
        }

        [Fact]
        public async Task CallingElseWithValueAfterThenAsync_WhenIsError_ShouldReturnElseValue()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .Then(str => ConvertToInt(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .Else("oh no");

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo("oh no");
        }

        [Fact]
        public async Task CallingElseWithValueFuncAfterThenAsync_WhenIsError_ShouldReturnElseValue()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .Then(str => ConvertToInt(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .Else(errors => $"Error count: {errors.Count}");

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo("Error count: 1");
        }

        [Fact]
        public async Task CallingElseWithErrorAfterThenAsync_WhenIsError_ShouldReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .Then(str => ConvertToInt(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .Else(Error.Unexpected());

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public async Task CallingElseWithErrorFuncAfterThenAsync_WhenIsError_ShouldReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .Then(str => ConvertToInt(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .Else(errors => Error.Unexpected());

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public async Task CallingElseWithErrorFuncAfterThenAsync_WhenIsError_ShouldReturnElseErrors()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .Then(str => ConvertToInt(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .Else(errors => new List<Error> { Error.Unexpected() });

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public async Task CallingElseAsyncWithValueFunc_WhenIsSuccess_ShouldNotInvokeElseFunc()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(errors => Task.FromResult($"Error count: {errors.Count}"));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo(errorOrString.Value);
        }

        [Fact]
        public async Task CallingElseAsyncWithValueFunc_WhenIsError_ShouldInvokeElseFunc()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(errors => Task.FromResult($"Error count: {errors.Count}"));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo("Error count: 1");
        }

        [Fact]
        public async Task CallingElseAsyncWithValue_WhenIsSuccess_ShouldNotReturnElseValue()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(Task.FromResult("oh no"));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo(errorOrString.Value);
        }

        [Fact]
        public async Task CallingElseAsyncWithValue_WhenIsError_ShouldReturnElseValue()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(Task.FromResult("oh no"));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo("oh no");
        }

        [Fact]
        public async Task CallingElseAsyncWithError_WhenIsError_ShouldReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(Task.FromResult(Error.Unexpected()));

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public async Task CallingElseAsyncWithError_WhenIsSuccess_ShouldNotReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(Task.FromResult(Error.Unexpected()));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(errorOrString.Value);
        }

        [Fact]
        public async Task CallingElseAsyncWithErrorFunc_WhenIsError_ShouldReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(errors => Task.FromResult(Error.Unexpected()));

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public async Task CallingElseAsyncWithErrorFunc_WhenIsSuccess_ShouldNotReturnElseError()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(errors => Task.FromResult(Error.Unexpected()));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(errorOrString.Value);
        }

        [Fact]
        public async Task CallingElseAsyncWithErrorFunc_WhenIsError_ShouldReturnElseErrors()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(errors => Task.FromResult(new List<Error> { Error.Unexpected() }));

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        }

        [Fact]
        public async Task CallingElseAsyncWithErrorFunc_WhenIsSuccess_ShouldNotReturnElseErrors()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .ElseAsync(errors => Task.FromResult(new List<Error> { Error.Unexpected() }));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(errorOrString.Value);
        }

        [Fact]
        public void CallingMatch_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            string OnValueAction(Person person)
            {
                person.Should().BeEquivalentTo(errorOrPerson.Value);
                return "Nice";
            }

            string OnErrorsAction(IReadOnlyList<Error> _) => throw new Exception("Should not be called");

            // Act
            Func<string> action = () => errorOrPerson.Match(
                OnValueAction,
                OnErrorsAction);

            // Assert
            action.Should().NotThrow().Subject.Should().Be("Nice");
        }

        [Fact]
        public void CallingMatch_WhenIsError_ShouldExecuteOnErrorAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            string OnValueAction(Person _) => throw new Exception("Should not be called");

            string OnErrorsAction(IReadOnlyList<Error> errors)
            {
                errors.Should().BeEquivalentTo(errorOrPerson.Errors);
                return "Nice";
            }

            // Act
            Func<string> action = () => errorOrPerson.Match(
                OnValueAction,
                OnErrorsAction);

            // Assert
            action.Should().NotThrow().Subject.Should().Be("Nice");
        }

        [Fact]
        public void CallingMatchFirst_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            string OnValueAction(Person person)
            {
                person.Should().BeEquivalentTo(errorOrPerson.Value);
                return "Nice";
            }

            string OnFirstErrorAction(Error _) => throw new Exception("Should not be called");

            // Act
            Func<string> action = () => errorOrPerson.MatchFirst(
                OnValueAction,
                OnFirstErrorAction);

            // Assert
            action.Should().NotThrow().Subject.Should().Be("Nice");
        }

        [Fact]
        public void CallingMatchFirst_WhenIsError_ShouldExecuteOnFirstErrorAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            string OnValueAction(Person _) => throw new Exception("Should not be called");
            string OnFirstErrorAction(Error errors)
            {
                errors.Should().BeEquivalentTo(errorOrPerson.Errors[0])
                    .And.BeEquivalentTo(errorOrPerson.FirstError);

                return "Nice";
            }

            // Act
            Func<string> action = () => errorOrPerson.MatchFirst(
                OnValueAction,
                OnFirstErrorAction);

            // Assert
            action.Should().NotThrow().Subject.Should().Be("Nice");
        }

        [Fact]
        public async Task CallingMatchFirstAfterThenAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            string OnValueAction(Person person)
            {
                person.Should().BeEquivalentTo(errorOrPerson.Value);
                return "Nice";
            }

            string OnFirstErrorAction(Error _) => throw new Exception("Should not be called");

            // Act
            Func<Task<string>> action = () => errorOrPerson
                .ThenAsync(person => Task.FromResult(person))
                .MatchFirst(OnValueAction, OnFirstErrorAction);

            // Assert
            (await action.Should().NotThrowAsync()).Subject.Should().Be("Nice");
        }

        [Fact]
        public async Task CallingMatchAfterThenAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            string OnValueAction(Person person)
            {
                person.Should().BeEquivalentTo(errorOrPerson.Value);
                return "Nice";
            }

            string OnErrorsAction(IReadOnlyList<Error> _) => throw new Exception("Should not be called");

            // Act
            Func<Task<string>> action = () => errorOrPerson
                .ThenAsync(person => Task.FromResult(person))
                .Match(OnValueAction, OnErrorsAction);

            // Assert
            (await action.Should().NotThrowAsync()).Subject.Should().Be("Nice");
        }

        [Fact]
        public async Task CallingMatchAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            Task<string> OnValueAction(Person person)
            {
                person.Should().BeEquivalentTo(errorOrPerson.Value);
                return Task.FromResult("Nice");
            }

            Task<string> OnErrorsAction(IReadOnlyList<Error> _) => throw new Exception("Should not be called");

            // Act
            Func<Task<string>> action = async () => await errorOrPerson.MatchAsync(
                OnValueAction,
                OnErrorsAction);

            // Assert
            (await action.Should().NotThrowAsync()).Subject.Should().Be("Nice");
        }

        [Fact]
        public async Task CallingMatchAsync_WhenIsError_ShouldExecuteOnErrorAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            Task<string> OnValueAction(Person _) => throw new Exception("Should not be called");

            Task<string> OnErrorsAction(IReadOnlyList<Error> errors)
            {
                errors.Should().BeEquivalentTo(errorOrPerson.Errors);
                return Task.FromResult("Nice");
            }

            // Act
            Func<Task<string>> action = async () => await errorOrPerson.MatchAsync(
                OnValueAction,
                OnErrorsAction);

            // Assert
            (await action.Should().NotThrowAsync()).Subject.Should().Be("Nice");
        }

        [Fact]
        public async Task CallingMatchFirstAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            Task<string> OnValueAction(Person person)
            {
                person.Should().BeEquivalentTo(errorOrPerson.Value);
                return Task.FromResult("Nice");
            }

            Task<string> OnFirstErrorAction(Error _) => throw new Exception("Should not be called");

            // Act
            Func<Task<string>> action = async () => await errorOrPerson.MatchFirstAsync(
                OnValueAction,
                OnFirstErrorAction);

            // Assert
            (await action.Should().NotThrowAsync()).Subject.Should().Be("Nice");
        }

        [Fact]
        public async Task CallingMatchFirstAsync_WhenIsError_ShouldExecuteOnFirstErrorAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            Task<string> OnValueAction(Person _) => throw new Exception("Should not be called");
            Task<string> OnFirstErrorAction(Error errors)
            {
                errors.Should().BeEquivalentTo(errorOrPerson.Errors[0])
                    .And.BeEquivalentTo(errorOrPerson.FirstError);

                return Task.FromResult("Nice");
            }

            // Act
            Func<Task<string>> action = async () => await errorOrPerson.MatchFirstAsync(
                OnValueAction,
                OnFirstErrorAction);

            // Assert
            (await action.Should().NotThrowAsync()).Subject.Should().Be("Nice");
        }

        [Fact]
        public async Task CallingMatchFirstAsyncAfterThenAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            Task<string> OnValueAction(Person _) => throw new Exception("Should not be called");
            Task<string> OnFirstErrorAction(Error errors)
            {
                errors.Should().BeEquivalentTo(errorOrPerson.Errors[0])
                    .And.BeEquivalentTo(errorOrPerson.FirstError);

                return Task.FromResult("Nice");
            }

            // Act
            Func<Task<string>> action = () => errorOrPerson
                .ThenAsync(person => Task.FromResult(person))
                .MatchFirstAsync(OnValueAction, OnFirstErrorAction);

            // Assert
            (await action.Should().NotThrowAsync()).Subject.Should().Be("Nice");
        }

        [Fact]
        public async Task CallingMatchAsyncAfterThenAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            Task<string> OnValueAction(Person _) => throw new Exception("Should not be called");

            Task<string> OnErrorsAction(IReadOnlyList<Error> errors)
            {
                errors.Should().BeEquivalentTo(errorOrPerson.Errors);
                return Task.FromResult("Nice");
            }

            // Act
            Func<Task<string>> action = () => errorOrPerson
                .ThenAsync(person => Task.FromResult(person))
                .MatchAsync(OnValueAction, OnErrorsAction);

            // Assert
            (await action.Should().NotThrowAsync()).Subject.Should().Be("Nice");
        }

        [Fact]
        public void CallingSwitch_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            void OnValueAction(Person person) => person.Should().BeEquivalentTo(errorOrPerson.Value);
            void OnErrorsAction(IReadOnlyList<Error> _) => throw new Exception("Should not be called");

            // Act
            Action action = () => errorOrPerson.Switch(
                OnValueAction,
                OnErrorsAction);

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void CallingSwitch_WhenIsError_ShouldExecuteOnErrorAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            void OnValueAction(Person _) => throw new Exception("Should not be called");
            void OnErrorsAction(IReadOnlyList<Error> errors) => errors.Should().BeEquivalentTo(errorOrPerson.Errors);

            // Act
            Action action = () => errorOrPerson.Switch(
                OnValueAction,
                OnErrorsAction);

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void CallingSwitchFirst_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            void OnValueAction(Person person) => person.Should().BeEquivalentTo(errorOrPerson.Value);
            void OnFirstErrorAction(Error _) => throw new Exception("Should not be called");

            // Act
            Action action = () => errorOrPerson.SwitchFirst(
                OnValueAction,
                OnFirstErrorAction);

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void CallingSwitchFirst_WhenIsError_ShouldExecuteOnFirstErrorAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            void OnValueAction(Person _) => throw new Exception("Should not be called");
            void OnFirstErrorAction(Error errors)
                => errors.Should().BeEquivalentTo(errorOrPerson.Errors[0])
                    .And.BeEquivalentTo(errorOrPerson.FirstError);

            // Act
            Action action = () => errorOrPerson.SwitchFirst(
                OnValueAction,
                OnFirstErrorAction);

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public async Task CallingSwitchFirstAfterThenAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            void OnValueAction(Person person) => person.Should().BeEquivalentTo(errorOrPerson.Value);
            void OnFirstErrorAction(Error _) => throw new Exception("Should not be called");

            // Act
            Func<Task> action = () => errorOrPerson
                .ThenAsync(person => Task.FromResult(person))
                .SwitchFirst(OnValueAction, OnFirstErrorAction);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task CallingSwitchAfterThenAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            void OnValueAction(Person person) => person.Should().BeEquivalentTo(errorOrPerson.Value);
            void OnErrorsAction(IReadOnlyList<Error> _) => throw new Exception("Should not be called");

            // Act
            Func<Task> action = () => errorOrPerson
                .ThenAsync(person => Task.FromResult(person))
                .Switch(OnValueAction, OnErrorsAction);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task CallingSwitchAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            Task OnValueAction(Person person) => Task.FromResult(person.Should().BeEquivalentTo(errorOrPerson.Value));
            Task OnErrorsAction(IReadOnlyList<Error> _) => throw new Exception("Should not be called");

            // Act
            Func<Task> action = async () => await errorOrPerson.SwitchAsync(
                OnValueAction,
                OnErrorsAction);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task CallingSwitchAsync_WhenIsError_ShouldExecuteOnErrorAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            Task OnValueAction(Person _) => throw new Exception("Should not be called");
            Task OnErrorsAction(IReadOnlyList<Error> errors) => Task.FromResult(errors.Should().BeEquivalentTo(errorOrPerson.Errors));

            // Act
            Func<Task> action = async () => await errorOrPerson.SwitchAsync(
                OnValueAction,
                OnErrorsAction);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task CallingSwitchFirstAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            Task OnValueAction(Person person) => Task.FromResult(person.Should().BeEquivalentTo(errorOrPerson.Value));
            Task OnFirstErrorAction(Error _) => throw new Exception("Should not be called");

            // Act
            Func<Task> action = async () => await errorOrPerson.SwitchFirstAsync(
                OnValueAction,
                OnFirstErrorAction);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task CallingSwitchFirstAsync_WhenIsError_ShouldExecuteOnFirstErrorAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new List<Error> { Error.Validation(), Error.Conflict() };
            Task OnValueAction(Person _) => throw new Exception("Should not be called");
            Task OnFirstErrorAction(Error errors)
                => Task.FromResult(errors.Should().BeEquivalentTo(errorOrPerson.Errors[0])
                    .And.BeEquivalentTo(errorOrPerson.FirstError));

            // Act
            Func<Task> action = async () => await errorOrPerson.SwitchFirstAsync(
                OnValueAction,
                OnFirstErrorAction);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task CallingSwitchFirstAsyncAfterThenAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            Task OnValueAction(Person person) => Task.FromResult(person.Should().BeEquivalentTo(errorOrPerson.Value));
            Task OnFirstErrorAction(Error _) => throw new Exception("Should not be called");

            // Act
            Func<Task> action = async () => await errorOrPerson
                .ThenAsync(person => Task.FromResult(person))
                .SwitchFirstAsync(
                    OnValueAction,
                    OnFirstErrorAction);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task CallingSwitchAsyncAfterThenAsync_WhenIsSuccess_ShouldExecuteOnValueAction()
        {
            // Arrange
            ErrorOr<Person> errorOrPerson = new Person("John Doe");
            Task OnValueAction(Person person) => Task.FromResult(person.Should().BeEquivalentTo(errorOrPerson.Value));
            Task OnErrorsAction(IReadOnlyList<Error> _) => throw new Exception("Should not be called");

            // Act
            Func<Task> action = async () => await errorOrPerson
                .ThenAsync(person => Task.FromResult(person))
                .SwitchAsync(OnValueAction, OnErrorsAction);

            // Assert
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public void CallingThen_WhenIsSuccess_ShouldInvokeGivenFunc()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => num * 2)
                .Then(num => ConvertToString(num));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo("10");
        }

        [Fact]
        public void CallingThen_WhenIsSuccess_ShouldInvokeGivenAction()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<int> result = errorOrString
                .Then(str => { _ = 5; })
                .Then(str => ConvertToInt(str))
                .Then(str => { _ = 5; });

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(5);
        }

        [Fact]
        public void CallingThen_WhenIsError_ShouldReturnErrors()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = errorOrString
                .Then(str => ConvertToInt(str))
                .Then(num => num * 2)
                .Then(str => { _ = 5; })
                .Then(num => ConvertToString(num));

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().BeEquivalentTo(errorOrString.FirstError);
        }

        [Fact]
        public async Task CallingThenAfterThenAsync_WhenIsSuccess_ShouldInvokeGivenFunc()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .Then(num => num * 2)
                .ThenAsync(num => ConvertToStringAsync(num))
                .Then(str => ConvertToInt(str))
                .ThenAsync(num => ConvertToStringAsync(num))
                .Then(num => { _ = 5; });

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be("10");
        }

        [Fact]
        public async Task CallingThenAfterThenAsync_WhenIsError_ShouldReturnErrors()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .Then(num => ConvertToString(num));

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().BeEquivalentTo(errorOrString.FirstError);
        }

        [Fact]
        public async Task CallingThenAsync_WhenIsSuccess_ShouldInvokeNextThen()
        {
            // Arrange
            ErrorOr<string> errorOrString = "5";

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => Task.FromResult(num * 2))
                .ThenAsync(num => Task.Run(() => { _ = 5; }))
                .ThenAsync(num => ConvertToStringAsync(num));

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().BeEquivalentTo("10");
        }

        [Fact]
        public async Task CallingThenAsync_WhenIsError_ShouldReturnErrors()
        {
            // Arrange
            ErrorOr<string> errorOrString = Error.NotFound();

            // Act
            ErrorOr<string> result = await errorOrString
                .ThenAsync(str => ConvertToIntAsync(str))
                .ThenAsync(num => Task.FromResult(num * 2))
                .ThenAsync(num => Task.Run(() => { _ = 5; }))
                .ThenAsync(num => ConvertToStringAsync(num));

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().BeEquivalentTo(errorOrString.FirstError);
        }

        [Fact]
        public void ValueToErrorOr_WhenAccessingValue_ShouldReturnValue()
        {
            // Arrange
            int value = 5;

            // Act
            ErrorOr<int> result = value.ToErrorOr();

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().Be(value);
        }

        [Fact]
        public void ErrorToErrorOr_WhenAccessingFirstError_ShouldReturnSameError()
        {
            // Arrange
            Error error = Error.Unauthorized();

            // Act
            ErrorOr<int> result = error.ToErrorOr<int>();

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(error);
        }

        [Fact]
        public void ListOfErrorsToErrorOr_WhenAccessingErrors_ShouldReturnSameErrors()
        {
            // Arrange
            List<Error> errors = new List<Error> { Error.Unauthorized(), Error.Validation() };

            // Act
            ErrorOr<int> result = errors.ToErrorOr<int>();

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().BeEquivalentTo(errors);
        }

        private static ErrorOr<string> ConvertToString(int num) => num.ToString();

        private static ErrorOr<int> ConvertToInt(string str) => int.Parse(str);

        private static Task<ErrorOr<int>> ConvertToIntAsync(string str) => Task.FromResult(ErrorOrFactory.From(int.Parse(str)));

        private static Task<ErrorOr<string>> ConvertToStringAsync(int num) => Task.FromResult(ErrorOrFactory.From(num.ToString()));
    }
}
