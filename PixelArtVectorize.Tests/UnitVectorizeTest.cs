using Xunit;
using Xunit.Abstractions;
namespace PixelArtVectorize.Tests
{
    public class UnitVectorizeTest
    {
        private readonly ITestOutputHelper _output;

        public UnitVectorizeTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestVectorize1()
        {
            PixelArtVectorizer pixel = new PixelArtVectorizer();
            //_output.WriteLine(Path.GetRelativePath(".","c:\\dotnet\\PixelArtVectorize\\pixelart-vectorizer-core\\PixelArtVectorize\\Resources\\mario8bit.png"));
            _output.WriteLine(pixel.VectorizeImage("..\\..\\..\\..\\PixelArtVectorize\\Resources\\mario8bit.png"));
            Assert.True(true);
        }
    }
}
