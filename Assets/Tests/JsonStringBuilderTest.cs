using System;
using System.Collections.Generic;
using NUnit.Framework;
using Plugins.GameBoost.Core;

public class JsonStringBuilderTest
{
    private static string CallBuilder(Action<JsonSerializer.JsonStringBuilder> builderAction)
    {
        var builder = new JsonSerializer.JsonStringBuilder();
        builderAction.Invoke(builder);
        return builder.ToString();
    }

    private static void TestAny<T>(T obj, string expectedString)
    {
        var result = CallBuilder(builder => builder.WriteAny(obj));
        Assert.AreEqual(result, expectedString);
    }


    [Test]
    public void TestInteger()
    {
        int testNumber = 42;
        TestAny(testNumber, "42");
    }

    [Test]
    public void TestFloatIntegral()
    {
        float testNumber = 42f;
        TestAny(testNumber, "42");
    }

    [Test]
    public void TestFloatBinaryFraction()
    {
        float testNumber = 42.5f;
        TestAny(testNumber, "42.5");
    }

    [Test]
    public void TestFloatNonBinaryFraction()
    {
        float testNumber = 42.3f;
        TestAny(testNumber, "42.2999992");
    }

    [Test]
    public void TestWriteNull()
    {
        var result = CallBuilder(builder => builder.WriteObject(null, false));
        Assert.AreEqual(result, "null");
    }

    [Test]
    public void TestWriteListDoSortDoesNotChangeOrder()
    {
        var testList = new List<int>{ 5, 4, 3, 2, 1 };
        var expectedString = "[5,4,3,2,1]";
        var trueResult = CallBuilder(builder => builder.WriteList(testList, true));
        var falseResult = CallBuilder(builder => builder.WriteList(testList, false));
        Assert.AreEqual(expectedString, trueResult);
        Assert.AreEqual(expectedString, falseResult);
    }

    [Test]
    public void TestWriteDictionaryDoSrotAffectsOrder()
    {
        var testDictionary = new Dictionary<string, int> {{"Akey", 42}, {"Ckey", 43}, {"Bkey", 44}};
        var expectedSortedString = "{\"Akey\":42,\"Bkey\":44,\"Ckey\":43}";
        var trueResult = CallBuilder(builder => builder.WriteDictionary(testDictionary, true));
        var falseResult = CallBuilder(builder => builder.WriteDictionary(testDictionary, false));
        Assert.AreEqual(expectedSortedString, trueResult);
        Assert.AreNotEqual(trueResult, falseResult);
    }
}
