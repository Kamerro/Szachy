using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Szachy.Figury;
using Szachy.Grid;
using Szachy.Converted_Photos;
using static Szachy.Converted_Photos.ConvertedPhotos;
namespace Szachy.Factories
{
    internal class PieceFactory
    {
        public PieceFactory()
        {
          
        }
        internal void MakePiece(string type,BetterPB pictureBox)
        {
            AFigury figura;
            bool isBlack;
            Image img;
            switch (type){
                //Tu też by można do tych stałych przyrównywać z FabrykiPol, ale ten enum jakoś mi bardziej pasuje, bo wtedy pionek musi byc public
                case FabrykaPol.pionek:
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
