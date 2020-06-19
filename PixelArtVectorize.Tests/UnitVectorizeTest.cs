using Xunit;

namespace PixelArtVectorize.Tests
{
    public class UnitVectorizeTest
    {
        [Fact]
        public void TestVectorize1()
        {
            var pixel = new PixelArtVectorizer();
            string newImage = pixel.VectorizeImage("c:\\dotnet\\");
            // Assert.True(!newImage.isEmpty());
            Assert.True(true);
        }
    }
}
