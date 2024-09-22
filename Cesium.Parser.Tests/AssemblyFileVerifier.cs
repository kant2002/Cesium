using System.Reflection;
using Cesium.TestFramework;
[assembly: UsesVerify]

namespace Cesium.Parser.Tests;

public class AssemblyFileVerifier
{
    [Fact]
    public void AssemblyHasNoUnusedTestFiles() =>
        TestFileVerification.VerifyAllTestsFromAssembly(Assembly.GetExecutingAssembly());
}
