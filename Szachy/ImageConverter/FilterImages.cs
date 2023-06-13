using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szachy.ImageConverter
{
   public static class FilterImages
    { 
        public static Bitmap ConvertImageTransparency(string imagePath)
        {
            // Wczytaj obrazek
            Bitmap bitmap = new Bitmap(imagePath);

            // Iteruj przez każdy piksel obrazka
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);

                    // Sprawdź, czy piksel ma kolor biały
                    if (pixelColor.R > 70 && pixelColor.G > 70 && pixelColor.B > 70)
                    {
                        // Jeśli tak, ustaw przezroczystość na 0
                        bitmap.SetPixel(x, y, Color.FromArgb(0, pixelColor.R, pixelColor.G, pixelColor.B));
                    }
                    else
                    {
                        // Jeśli nie, ustaw przezroczystość na 255 (brak przezroczystości)
                        bitmap.SetPixel(x, y, Color.FromArgb(255, pixelColor.R, pixelColor.G, pixelColor.B));
                    }
                }
            }
            // Zapisz zmodyfikowany obrazek z przezroczystością
            return bitmap;
        }
    }

}
