using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;


namespace randomrgb
{
    internal class Program
    {

        static Color SetColor(int R, int G, int B)
        {
            // Color requires the pixels to be Rgb24 (eg 8 bit), Rgb24 requres the data to be i bytes (to fit the 0-255).
            Color colInt = new Color(new Rgba32((byte)R, (byte)G, (byte)B));


            return colInt;
        }
        static int[] GenerateInts(int number)
        {
            int[] ints = new int[number];

            var rand = new Random();

            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = rand.Next(0, 256);

            }



            return ints;
        }

        static void Main(string[] args)
        {



            // When we do it for real: 2046*1364*3 = 8380416
            // make image X,y

            // size of the image: 
            int xSizeInt = 1600;
            int ySizeInt = 1600;


            // make the actual image
            Image<Rgba32> image = new Image<Rgba32>(xSizeInt, ySizeInt);
            Console.WriteLine("image created");

            // GenerateInts()
            int[] intArr = GenerateInts(xSizeInt * ySizeInt * 3);
            Console.WriteLine("Ints created");
            
            
            // read the random data into pixels

            // We increment this value at each position (x,y) by 3, then take the value at (arrayplacement, arrayplacement+1 and arrayplacement+2)
            // and put into the color value
            int arrayplacement = 0;
            
            // go through the image
            for (int y = 0; y < ySizeInt; y++)
            {
                for (int x = 0; x < xSizeInt; x++)
                {
                    // set the actual pixel values

                    image[x, y] = SetColor(intArr[arrayplacement], intArr[arrayplacement + 1], intArr[arrayplacement + 2]);
                    arrayplacement = arrayplacement+3;


                }
            }
            Console.WriteLine("random data written to image");

            // we now have an image size filled with random pixel data
            // save the image

            image.SaveAsPng("random.png");
            Console.WriteLine("File written");


            using (Image imageRS = Image.Load("random.png"))
            {
                int width = imageRS.Width / 2;
                int height = imageRS.Height / 2;
                imageRS.Mutate(x => x.Resize(width, height, KnownResamplers.Box));
                //imageRS.Mutate(x => x.Resize(width*16, height*16));

                imageRS.SaveAsPng("RandomHalfsize.png");
            }



        }
    }
}


