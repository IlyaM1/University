using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\"a 'b' 'c' d\"", 0, "a 'b' 'c' d", 13)]
        [TestCase("\"def g h, 0", 0, "def g h, 0", 11)]
        [TestCase("'\"1\" \"2\" \"3\"'", 0, "\"1\" \"2\" \"3\"", 13)]
        [TestCase("'x y'", 0, "x y", 5)]
        [TestCase("\"a \\\"c\\\"\"", 0, "a \"c\"", 9)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var outputLine = new StringBuilder();
            var usingQuote = '0';
            for (int i = startIndex; i < line.Length; i++)
            {
                if (usingQuote == '0' && (line[i] == '"' || line[i] == '\''))
                {
                    usingQuote = line[i];
                    continue;
                }

                if (line[i] == line[startIndex] && line[i - 1] != '\\')
                {
                    return new Token(outputLine.ToString(), startIndex, i - startIndex + 1);
                }

                if (line[i] != '\\' || line[i - 1] == '\\')
                    outputLine.Append(line[i]);
            }
            return new Token(line.Substring(startIndex + 1,
                line.Length - startIndex - 1), startIndex, line.Length);
        }
    }
}