using System.Text;
using NUnit.Framework;
using Plugins.GameBoost.Core;

public class JsonStringWriterTest
{
    private static string CallWriter(string source)
    {
        var builder = new StringBuilder();
        JsonSerializer.JsonStringWriter.WriteString(builder, source);
        return builder.ToString();
    }

    private static void TestEscapes(char charToEscape)
    {
        var source = $"{charToEscape}";
        var expectedResult = $"\"\\{source}\"";
        var result = CallWriter(source);
        Assert.AreEqual(result, expectedResult);
    }

    private static void TestPrintsEscaped(char controlChar, char expectedPrint)
    {
        var source = $"{controlChar}";
        var expectedResult = $"\"\\{expectedPrint}\"";
        var result = CallWriter(source);
        Assert.AreEqual(result, expectedResult);
    }


    [Test]
    public void TestAddsQuotes()
    {
        TestEscapes('"');
    }

    [Test]
    public void TestEscapesBackslash()
    {
        TestEscapes('\\');
    }

    [Test]
    public void TestEscapesSlash()
    {
        TestEscapes('/');
    }

    [Test]
    public void TestPrintsEscapedBackspace()
    {
        TestPrintsEscaped('\b', 'b');
    }

    [Test]
    public void TestPrintsEscapedFormFeed()
    {
        TestPrintsEscaped('\f', 'f');
    }

    [Test]
    public void TestPrintsEscapedNewLine()
    {
        TestPrintsEscaped('\n', 'n');
    }

    [Test]
    public void TestPrintsEscapedCarriageReturn()
    {
        TestPrintsEscaped('\r', 'r');
    }

    [Test]
    public void TestPrintsEscapedTab()
    {
        TestPrintsEscaped('\t', 't');
    }
}
