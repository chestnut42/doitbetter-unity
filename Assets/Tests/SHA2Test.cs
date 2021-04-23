using NUnit.Framework;
using Plugins.GameBoost.Core;

public class SHA2Test
{
    [Test]
    public void TestSha2()
    {
        var source = "{\"key1\":400}";
        var expectedBytes = new byte[]
        {
            0x9c, 0x9d, 0xe1, 0x80, 0xaa, 0x78, 0xb1, 0x5a,
            0x5e, 0x38, 0x32, 0x99, 0x74, 0xf0, 0xcb, 0x5f,
            0x82, 0xf0, 0x88, 0x53, 0x08, 0xa5, 0x9d, 0x09,
            0x24, 0xf7, 0xab, 0x0e, 0x23, 0x5c, 0xce, 0xfb
        };

        var result = new SHA2Function().Calculate(source);
        Assert.AreEqual(expectedBytes, result);
    }
}