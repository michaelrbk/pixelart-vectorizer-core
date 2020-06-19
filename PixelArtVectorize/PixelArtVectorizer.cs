using System;
using System.Drawing;

namespace PixelArtVectorize
{
    public class PixelArtVectorizer
    {
        static int Main(string[] args)
        {
            /*
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a image path argument.");
                return 1;
            }
            Console.WriteLine("Image path:" + args[0]);
            */
            VectorizeImage("c:\\dotnet\\PixelArtVectorize\\pixelart-vectorizer-core\\PixelArtVectorize\\Resources\\mario8bit.png");
            return 0;
        }
        public static string VectorizeImage(string imagePath)
        {

            //Read Image
            Bitmap image = new Bitmap(imagePath);
            Vectorize vector = new Vectorize();
            String c = "";
            c = vector.ImageToGraph(image);
            c = vector.SolveAmbiguities();
            c = vector.ReshapePixelCell();
            c = vector.DrawNewGraphEdges();
            c = vector.CreateNewCurves();
            System.Diagnostics.Process.Start(vector.CreateShapes());
            return "terminou";
        }
    }
}
