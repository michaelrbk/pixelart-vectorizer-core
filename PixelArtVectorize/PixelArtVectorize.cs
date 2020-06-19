using System;

namespace PixelArtVectorize
{
    class PixelArtVectorize
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a image path argument.");
                return 1;
            }
            Console.WriteLine("Image path:" + args[0]);
            VectorizeImage(args[0]);
            return 0;
        }
        public static string VectorizeImage(string imagePath)
        {

            return "new image";
        }
    }
}
