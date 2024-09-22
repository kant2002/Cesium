#nullable enable
global using Assert = Xunit.Assert;
//global using TheoryAttribute = Xunit.TheoryAttribute;
global using TheoryAttribute = global::Microsoft.VisualStudio.TestTools.UnitTesting.DataTestMethodAttribute;
//global using FactAttribute = Xunit.FactAttribute;
global using FactAttribute = global::Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;
global using InlineDataAttribute = Xunit.InlineDataAttribute;
global using MemberDataAttribute = Xunit.MemberDataAttribute;
using System.Diagnostics.CodeAnalysis;

namespace Xunit.Abstractions
{

    public interface ITestOutputHelper
    {
        void WriteLine(string message);
    }

}

namespace Xunit
{
    public static class Assert
    {
        public static void True([DoesNotReturnIf(false)] bool condition)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(condition);
        }
        public static void True([DoesNotReturnIf(false)] bool condition, string? message)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(condition, message);
        }
        public static void False([DoesNotReturnIf(true)] bool condition)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(condition);
        }
        public static void False([DoesNotReturnIf(true)] bool condition, string? message)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(condition, message);
        }
        public static void NotNull(object? value)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(value);
        }
        public static void StartsWith(string? value, string? substring)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert.StartsWith(value, substring);
        }
        public static void Contains(string? substring, string? value)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert.Contains(value, substring);
        }
        public static void Equal<T>(T? expected, T? value)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, value);
        }
        public static T Single<T>(IEnumerable<T> value)
        {
            T result = default!;
            int count = 0;
            foreach (var item in value)
            {
                result = item;
                count++;
                if (count == 2)
                {
                    global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Not a single value");
                }
            }

            if (count == 0)
            {
                global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail("Collection empty");
            }

            return result;
        }
        public static void Empty<T>(IEnumerable<T> value)
        {
            global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, value.Count());
        }
        public static Task<T> ThrowsAsync<T>(Func<Task> action)
            where T: Exception
        {
            return global::Microsoft.VisualStudio.TestTools.UnitTesting.Assert.ThrowsExceptionAsync<T>(action);
        }
    }

    //[AttributeUsage(AttributeTargets.Method)]
    public class TheoryAttribute : global::Microsoft.VisualStudio.TestTools.UnitTesting.DataTestMethodAttribute
    {

    }

    //[AttributeUsage(AttributeTargets.Method)]
    public class FactAttribute : global::Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute
    {
        public string? Skip { get; set; }

        public override global::Microsoft.VisualStudio.TestTools.UnitTesting.TestResult[] Execute(global::Microsoft.VisualStudio.TestTools.UnitTesting.ITestMethod testMethod)
        {
            if (Skip != null)
            {
                return new[]
                {
                    new global::Microsoft.VisualStudio.TestTools.UnitTesting.TestResult()
                    {
                        Outcome = Microsoft.VisualStudio.TestTools.UnitTesting.UnitTestOutcome.Inconclusive,
                    }
                };
            }

            return base.Execute(testMethod);
        }
    }

    //[AttributeUsage(AttributeTargets.Method)]
    public class InlineDataAttribute : global::Microsoft.VisualStudio.TestTools.UnitTesting.DataRowAttribute
    {
        public InlineDataAttribute(params object?[]? data)
            : base(data) { }
    }
    public class MemberDataAttribute : global::Microsoft.VisualStudio.TestTools.UnitTesting.DataRowAttribute
    {
        public MemberDataAttribute(string member)
        {
            MemberName = member;
        }

        public string MemberName { get; }

        public override bool Match(object? obj)
        {
            throw new global::Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException("MemberDataAttribute is not supported");
        }
    }
}

namespace Xunit.Sdk
{
    public class XunitException : global::Microsoft.VisualStudio.TestTools.UnitTesting.UnitTestAssertException
    {
        public XunitException()
        {
        }
        public XunitException(string msg)
        : base(msg)
        {
        }
        public XunitException(string msg, Exception? ex)
        : base(msg, ex!)
        {
        }
    }
}
