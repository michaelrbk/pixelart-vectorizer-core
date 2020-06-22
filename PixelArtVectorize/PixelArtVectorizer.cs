using System;
using System.Drawing;

namespace PixelArtVectorize
{
    public class PixelArtVectorizer
    {
        static int Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a image path argument.");
                return 1;
            }
            Console.WriteLine("Image path:" + args[0]);

            PixelArtVectorizer pixel = new PixelArtVectorizer();
            pixel.VectorizeImage(args[0]);
            return 0;
        }
        public string VectorizeImage(string imagePath)
        {

            //Read Image
            Bitmap image = new Bitmap(imagePath);
            Vectorize vector = new Vectorize();
            _ = vector.ImageToGraph(image);
            _ = vector.SolveAmbiguities();
            _ = vector.ReshapePixelCell();
            _ = vector.DrawNewGraphEdges();
            _ = vector.CreateNewCurves();

            return vector.CreateShapes();
        }
    }
}
