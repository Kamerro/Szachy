using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szachy.ImageConverter;

namespace Szachy.Converted_Photos
{
    static class ConvertedPhotos
    {

        static public Image PionekC;
        static public Image PionekB;
        static public Image BiskupC;
        static public Image BiskupB;
        static public Image WiezaC;
        static public Image WiezaB;
        static public Image KonC;
        static public Image KonB;
        static public Image KrolC;
        static public Image KrolB;
        static public Image KrolowaC;
        static public Image KrolowaB;

        public static void PushPhotos()
        {
            PionekC = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/PionekC.png");
            PionekB = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/PionekB.png");
            BiskupC = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/BiskupC.png");
            BiskupB = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/BiskupB.png");
            WiezaC = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/WiezaC.png");
            WiezaB = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/WiezaB.png");
            KonC = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/KonC.png");
            KonB = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/KonB.png");
            KrolC = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/KrolC.png");
            KrolB = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/KrolB.png");
            KrolowaC = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/KrolowaC.png");
            KrolowaB = FilterImages.ConvertImageTransparency($"{AppDomain.CurrentDomain.BaseDirectory}/Zdjecia/KrolowaB.png");
        }
    }

}
