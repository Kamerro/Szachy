using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Szachy.Figury;
using Szachy.Grid;
using Szachy.ImageConverter;
namespace Szachy.Factories
{
    internal class PieceFactory
    {
        public PieceFactory()
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
        public Image PionekC;
        public Image PionekB;
        public Image BiskupC;
        public Image BiskupB;
        public Image WiezaC;
        public Image WiezaB;
        public Image KonC;
        public Image KonB;
        public Image KrolC;
        public Image KrolB;
        public Image KrolowaC;
        public Image KrolowaB;

        internal void MakePiece(string type,BetterPB pictureBox)
        {
            AFigury figura;
            bool isBlack;
            Image img;
            switch (type){
                
                case "Pionek":
                    isBlack = pictureBox.y == 6 ? true : false;
                    img = pictureBox.y == 6 ? PionekC : PionekB;
                    // bool isOnBlack = (j + i) % 2 == 0 ? false : true;
                    figura = new Pionek(pictureBox.x, pictureBox.y, img, false, isBlack);
                    pictureBox.Image = img;
                    pictureBox.aFigura = figura;
                    pictureBox.aFigura.siatka = Siatka.initializeGrid();
                break;


                case "Biskup":
                    isBlack = pictureBox.y == 7 ? true : false;
                    img = pictureBox.y == 7 ? BiskupC : BiskupB;
                    figura = new Biskup(pictureBox.x, pictureBox.y, img, false, isBlack);
                    pictureBox.Image = img;
                    pictureBox.aFigura = figura;
                    pictureBox.aFigura.siatka = Siatka.initializeGrid();

                    break;

                case "Wieza":
                    isBlack = pictureBox.y == 7 ? true : false;
                    img = pictureBox.y == 7 ? WiezaC : WiezaB;
                    figura = new Wieza(pictureBox.x, pictureBox.y, img, false, isBlack);
                    pictureBox.Image = img;
                    pictureBox.aFigura = figura;
                    pictureBox.aFigura.siatka = Siatka.initializeGrid();
                    break;

                case "Kon":
                    isBlack = pictureBox.y == 7 ? true : false;
                    img = pictureBox.y == 7 ? KonC : KonB;
                    figura = new Kon(pictureBox.x, pictureBox.y, img, false, isBlack);
                    pictureBox.Image = img;
                    pictureBox.aFigura = figura;
                    pictureBox.aFigura.siatka = Siatka.initializeGrid();
                    break;

                case "Krol":
                    isBlack = pictureBox.y == 7 ? true : false;
                    img = pictureBox.y == 7 ? KrolC : KrolB;
                    figura = new Krol(pictureBox.x, pictureBox.y, img, false, isBlack);
                    pictureBox.Image = img;
                    pictureBox.aFigura = figura;
                    pictureBox.aFigura.siatka = Siatka.initializeGrid();
                    break;

                case "Krolowa":
                    isBlack = pictureBox.y == 7 ? true : false;
                    img = pictureBox.y == 7 ? KrolowaC : KrolowaB;
                    figura = new Krolowa(pictureBox.x, pictureBox.y, img, false, isBlack);
                    pictureBox.Image = img;
                    pictureBox.aFigura = figura;
                    pictureBox.aFigura.siatka = Siatka.initializeGrid();
                    break;

                default: break;
            }
          
        }
    }
}
