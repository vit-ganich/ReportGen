using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GetResultsCI
{
    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        [Description("Testing the parsing of the full path to a file, located in a network folder")]
        public void TestParseFromNetworkFolder()
        {
            string[] splittedFullPathToFile = { "", "", "computer-33", "Test Results",
                                                "CI", "10_26_2018", "CI Group_Name_1_CLTQACLIENT123",
                                                "60.55-Feature Name_mssql_Chrome_B7.7.1000.1-27-21-0-0.trx" };
            int length = 8;
            string resultString1 = "CLTQACLIENT123,CI Group_Name_1,60.55-Feature Name_mssql_Chrome,B7.7.1000.1,21,0,0,PASSED,-\n";
            string resultString2 = "CI Group_Name_1";
            
            string[] expectedResult = { resultString1, resultString2 };
            string[] actualresult = Parser.Parse(splittedFullPathToFile, length);

            CollectionAssert.AreEqual(actualresult, expectedResult, "Incorrect parsing ");
        }

        [TestMethod]
        [Description("Testing the parsing of the full path to a file, located in a local folder")]
        public void TestParseFromLocalFolder()
        {
            string[] splittedFullPathToFile = { "C:", "Test Results", "CI",
                                                "10_26_2018", "CI Group_1_CLTQACLIENT123",
                                                "60.55-Feature Name_mssql_Chrome_B7.7.1000.1-27-21-0-0.trx" };
            int length = 6;
            string resultString1 = "CLTQACLIENT123,CI Group_1,60.55-Feature Name_mssql_Chrome,B7.7.1000.1,21,0,0,PASSED,-\n";
            string resultString2 = "CI Group_1";

            string[] expectedResult = { resultString1, resultString2 };
            string[] actualresult = Parser.Parse(splittedFullPathToFile, length);

            CollectionAssert.AreEqual(actualresult, expectedResult, "Incorrect parsing ");
        }
    }
}
