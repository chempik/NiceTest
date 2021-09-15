using System;
using Xunit;
using System.IO.Abstractions.TestingHelpers;
using NiceTest;
namespace xUnitTest
{
    public class SubstringTest
    {
        [Fact]
        public void CaseSensitiveTest()
        {
            string file = @"C:\Target\test.txt";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"C:\Target");
            mockFileSystem.AddFile(@"C:\Target\test.txt", new MockFileData(
                @"Hihinbgfdgfdgfd
                  gfdgfdgHI"));

            var setting = new Setting(true, false, false);
            var sut = new Substring(mockFileSystem);

            var result = sut.Check(file, setting, "hi");
            Assert.Equal(3, result.NumberOfResults);
            Assert.Equal(1, result.LineNumber[0]);
            Assert.Equal(2, result.LineNumber[1]);
        }

        [Fact]
        public void FullWorldTest()
        {
            string file = @"C:\Target\test.txt";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"C:\Target");
            mockFileSystem.AddFile(@"C:\Target\test.txt", new MockFileData(
                @" hifdsfdsfdsfdsfdsfdsfsd
                  hinbgfdgfdgfd
                  gfdgfdghihi hi hi"));

            var setting = new Setting(false, true, false);
            var sut = new Substring(mockFileSystem);

            var result = sut.Check(file, setting, "hi");
            Assert.Equal(2, result.NumberOfResults);
            Assert.Equal(3, result.LineNumber[0]);
        }

        [Fact]
        public void IgnoreSpaceTest()
        {
            string file = @"C:\Target\test.txt";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"C:\Target");
            mockFileSystem.AddFile(@"C:\Target\test.txt", new MockFileData(
                @"h i  hinbgfdgfdgfd h
                 i fdsfsdf"));

            var setting = new Setting(false, false, true);
            var sut = new Substring(mockFileSystem);

            var result = sut.Check(file, setting, "hi");
            Assert.Equal(3, result.NumberOfResults);
            Assert.Equal(1, result.LineNumber[0]);
            Assert.Single(result.LineNumber);
        }

        [Fact]
        public void IgnoreSpaceTestAndFullWord ()
        {
            string file = @"C:\Target\test.txt";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"C:\Target");
            mockFileSystem.AddFile(@"C:\Target\test.txt", new MockFileData(
                @"h i hihi hi h
                 i fdsfsdf"));

            var setting = new Setting(false, true, true);
            var sut = new Substring(mockFileSystem);

            var result = sut.Check(file, setting, "hi");
            Assert.Equal(3, result.NumberOfResults);
            Assert.Equal(1, result.LineNumber[0]);
            Assert.Single(result.LineNumber);
        }

        [Fact]
        public void SpecialCharactersTest()
        {
            string file = @"C:\Target\test.txt";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"C:\Target");
            mockFileSystem.AddFile(@"C:\Target\test.txt", new MockFileData(
                @"kekekekek
                    ekk
                    dgle* ff"));

            var setting = new Setting(false, false, false);
            var sut = new Substring(mockFileSystem);

            var result = sut.Check(file, setting, "*");
            Assert.Equal(1, result.NumberOfResults);
            Assert.Equal(3, result.LineNumber[0]);
            Assert.Single(result.LineNumber);
        }

        [Fact]
        public void SpecialCharactersAndFullWordTest()
        {
            string file = @"C:\Target\test.txt";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"C:\Target");
            mockFileSystem.AddFile(@"C:\Target\test.txt", new MockFileData(
                @"kekekekek
                    ekk
                    dgle * ff"));

            var setting = new Setting(false, true, false);
            var sut = new Substring(mockFileSystem);

            var result = sut.Check(file, setting, "*");
            Assert.Equal(1, result.NumberOfResults);
            Assert.Equal(3, result.LineNumber[0]);
            Assert.Single(result.LineNumber);
        }

        [Fact]
        public void SpecialCharactersAndIgnoreSpace()
        {
            string file = @"C:\Target\test.txt";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"C:\Target");
            mockFileSystem.AddFile(@"C:\Target\test.txt", new MockFileData(
                @"kekekekek
                    ekk
                    dgle * * * ff"));

            var setting = new Setting(false, false, true);
            var sut = new Substring(mockFileSystem);

            var result = sut.Check(file, setting, "***");
            Assert.Equal(1, result.NumberOfResults);
            Assert.Equal(3, result.LineNumber[0]);
            Assert.Single(result.LineNumber);
        }

        [Fact]
        public void SpecialCharactersAtFirstStringTest()
        {
            string file = @"C:\Target\test.txt";
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"C:\Target");
            mockFileSystem.AddFile(@"C:\Target\test.txt", new MockFileData(
                @"kekekekek
                    ekk
                    * * * ff"));

            var setting = new Setting(false, false, true);
            var sut = new Substring(mockFileSystem);

            var result = sut.Check(file, setting, "***");
            Assert.Equal(1, result.NumberOfResults);
            Assert.Equal(3, result.LineNumber[0]);
            Assert.Single(result.LineNumber);
        }
    }
}
